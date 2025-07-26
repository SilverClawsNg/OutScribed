using OutScribed.Application;
using OutScribed.Persistence.EntityConfigurations;
using OutScribed.Infrastructure.ExceptionHandling;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Hangfire.PostgreSql;
using Hangfire;
using OutScribed.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using OutScribed.Infrastructure.ErrorLogging;
using OutScribed.Api;
using OutScribed.Persistence;
using OutScribed.Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Serilog.Exceptions;
using Backend.Api.GeoData;
using OutScribed.Api.Analytics;
using Serilog.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireConnection");

builder.Services.AddDbContext<OutScribedDbContext>(options =>
options.UseNpgsql(connectionString));

builder.Services.AddDbContext<HangfireDbContext>(options => {
    options.UseNpgsql(hangfireConnectionString);
});

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies([typeof(Program).Assembly, typeof(Program).Assembly]); });

builder.Services.AddAutoMapper(typeof(Program));

//Add global exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//Set forwarded headers for NGINX proxy server
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddHangfire(configuration => configuration
   .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
   .UseSimpleAssemblyNameTypeSerializer()
   .UseRecommendedSerializerSettings()
   .UsePostgreSqlStorage(options => options.UseNpgsqlConnection(hangfireConnectionString),
   new PostgreSqlStorageOptions
   {

   }));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

//Gets the Current Http Context
builder.Services.AddHttpContextAccessor();

// Discover controllers from application.
builder.Services.AddControllersWithViews()
    .AddApplicationPart(typeof(ApplicationBaseClass).Assembly)
    .AddControllersAsServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOpenApiDocument();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0",
        Title = "Production API",
        Description = "Outscribed"

    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
    //option.OperationFilter<FileUploadFilter>();
});


//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtAudience = builder.Configuration.GetSection("Jwt:Audience").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

//DI(Dependency Injection) extensions
builder.Services.AddApplication();
builder.Services.AddPersistence();
builder.Services.AddInfrastructure();

builder.Services.AddSingleton(Log.Logger);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSingleton<IWebHostEnvironment>(builder.Environment);

//add cors
//builder.Services.AddCors();

//var AllowCoversFrontend = "_allowCoversFrontend";

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: AllowCoversFrontend,
//                      policy =>
//                      {
//                          policy.WithOrigins("https://outscribed.com")
//                              .AllowAnyMethod()
//                              .AllowAnyHeader();
//                      });
//});

builder.Services.AddSingleton<GeoLocationService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseForwardedHeaders();
    app.UseHsts();


}
else
{
    app.UseDeveloperExceptionPage();
    app.UseForwardedHeaders();
    app.UseHttpsRedirection();
}

//app.UseSwagger();
//app.UseOpenApi();
//app.UseSwaggerUI();

app.UseSwagger(c =>
{
    c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Production API");

});
app.UseOpenApi();

//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "staticImage v1"));
app.UseRouting();


app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseStaticFiles();


// set global cors policy
//app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


//app.UseCors(AllowCoversFrontend);

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<AnalyticsMiddleware>();


app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = [new MyAuthorizationFilter()]
});


app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


var config = new ConfigurationBuilder()
   .AddJsonFile("appsettings.json")
   .Build();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .Enrich.WithThreadId()
    .Enrich.With(new GeneralLogEnricher())
    .Enrich.With(new AnalyticsLogEnricher())
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(Matching.WithProperty("LogType", "General"))
        .WriteTo.PostgreSQL(
            connectionString: config.GetConnectionString("LogConnection"),
            tableName: "GeneralLogs",
            columnOptions: PostgreSqlColumns.General,
            needAutoCreateTable: true))
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(Matching.WithProperty("LogType", "Analytics"))
        .WriteTo.PostgreSQL(
            connectionString: config.GetConnectionString("AnalyticsConnection"),
            tableName: "AnalyticsLogs",
            columnOptions: PostgreSqlColumns.Analytics,
            needAutoCreateTable: true))
    .CreateLogger();

try
{
    Log.Information("Application Starting.");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The Application failed to start.");
}
finally
{
    Log.CloseAndFlush();
}