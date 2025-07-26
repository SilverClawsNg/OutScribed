using Backend.Application;
using Backend.Persistence.EntityConfigurations;
using Backend.Infrastructure.ExceptionHandling;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Hangfire.PostgreSql;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using Backend.Api;
using Backend.Persistence;
using Backend.Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Serilog.Exceptions;
using Backend.Api.Analytics;
using Serilog.Filters;
using Backend.Api.GeoData;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireConnection");

builder.Services.AddDbContext<BackendDbContext>(options =>
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

// Add services to the container.

builder.Services.AddOpenApiDocument();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0",
        Title = "Test API",
        Description = "Atlantis Tales"

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

//serilog
//builder.Services.AddTransient<IErrorLogger, ErrorLogger>();

//add cors
//builder.Services.AddCors();

//kestrel request size
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 52428800;
});

builder.Services.AddSingleton<GeoLocationService>();

//var AllowFrontend = "_allowFrontend";

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: AllowFrontend,
//                      policy =>
//                      {
//                          policy.WithOrigins("https://atlantistales.com");
//                      });
//});

var app = builder.Build();

//Uncomment for Serilog operation
//Serilog.Debugging.SelfLog.Enable(msg => File.AppendAllText("selflog.txt", msg));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API");

    });
    app.UseOpenApi();
}

app.UseRouting();


// set global cors policy
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseExceptionHandler();

app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
    Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
    RequestPath = "/media"
});

//app.UseCors(MyAllowSpecificOrigins);

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