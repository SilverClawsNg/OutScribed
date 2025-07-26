using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutScribed.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(56)", maxLength: 56, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: true),
                    Username = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: false),
                    OtpValue = table.Column<int>(type: "integer", nullable: true),
                    OtpDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DoNotResendOtp = table.Column<bool>(type: "boolean", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Views = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    Views = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "character varying(144)", maxLength: 144, nullable: true),
                    Title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Details = table.Column<string>(type: "character varying(32768)", maxLength: 32768, nullable: true),
                    Category = table.Column<string>(type: "character varying(48)", maxLength: 48, nullable: false),
                    Photo = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    Summary = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Country = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(56)", maxLength: 56, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: true),
                    OtpPassword = table.Column<int>(type: "integer", nullable: false),
                    OtpDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DoNotResendOtp = table.Column<bool>(type: "boolean", nullable: false),
                    Verified = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    ThreaderId = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    TaleId = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    Views = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Summary = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Details = table.Column<string>(type: "character varying(32768)", maxLength: 32768, nullable: true),
                    Url = table.Column<string>(type: "character varying(144)", maxLength: 144, nullable: false),
                    Category = table.Column<string>(type: "character varying(48)", maxLength: 48, nullable: false),
                    Photo = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    IsOnline = table.Column<bool>(type: "boolean", nullable: false),
                    Country = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WatchLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    Category = table.Column<string>(type: "character varying(48)", maxLength: 48, nullable: false),
                    Country = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    AdminId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Summary = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    SourceUrl = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    SourceText = table.Column<string>(type: "character varying(28)", maxLength: 28, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsOnline = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountFollowers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    FollowerId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountFollowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountFollowers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActiveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Details = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Type = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ConstructorType = table.Column<int>(type: "integer", nullable: false),
                    HasRead = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Type = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Application = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Text = table.Column<string>(type: "character varying(56)", maxLength: 56, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoginHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActiveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpAddress = table.Column<string>(type: "character varying(28)", maxLength: 28, nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginHistories_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayPhoto = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    Title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Bio = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaleComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    CommentatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Details = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaleComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaleComments_TaleComments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TaleComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaleComments_Tales_TaleId",
                        column: x => x.TaleId,
                        principalTable: "Tales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaleFlags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FlaggerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaleFlags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaleFlags_Tales_TaleId",
                        column: x => x.TaleId,
                        principalTable: "Tales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaleFollowers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    FollowerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaleFollowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaleFollowers_Tales_TaleId",
                        column: x => x.TaleId,
                        principalTable: "Tales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaleHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    TaleId = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    AdminId = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    Reasons = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaleHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaleHistories_Tales_TaleId",
                        column: x => x.TaleId,
                        principalTable: "Tales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaleRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    RaterId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaleRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaleRatings_Tales_TaleId",
                        column: x => x.TaleId,
                        principalTable: "Tales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaleShares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    SharerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaleShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaleShares_Tales_TaleId",
                        column: x => x.TaleId,
                        principalTable: "Tales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaleTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaleTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaleTags_Tales_TaleId",
                        column: x => x.TaleId,
                        principalTable: "Tales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreadAddendums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    ThreadsId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Details = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadAddendums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadAddendums_Threads_ThreadsId",
                        column: x => x.ThreadsId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreadComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    CommentatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThreadsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Details = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadComments_ThreadComments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ThreadComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ThreadComments_Threads_ThreadsId",
                        column: x => x.ThreadsId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreadFlags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FlaggerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThreadsId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadFlags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadFlags_Threads_ThreadsId",
                        column: x => x.ThreadsId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreadFollowers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    FollowerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThreadsId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadFollowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadFollowers_Threads_ThreadsId",
                        column: x => x.ThreadsId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreadRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    RaterId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThreadsId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadRatings_Threads_ThreadsId",
                        column: x => x.ThreadsId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreadShares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    SharerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThreadsId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadShares_Threads_ThreadsId",
                        column: x => x.ThreadsId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreadTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ThreadsId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadTags_Threads_ThreadsId",
                        column: x => x.ThreadsId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinkedTales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    TaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    WatchListId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedTales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkedTales_WatchLists_WatchListId",
                        column: x => x.WatchListId,
                        principalTable: "WatchLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatchListFollowers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    FollowerId = table.Column<Guid>(type: "uuid", nullable: false),
                    WatchListId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchListFollowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchListFollowers_WatchLists_WatchListId",
                        column: x => x.WatchListId,
                        principalTable: "WatchLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaleCommentFlags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FlaggerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaleCommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaleCommentFlags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaleCommentFlags_TaleComments_TaleCommentId",
                        column: x => x.TaleCommentId,
                        principalTable: "TaleComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaleCommentRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    RaterId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaleCommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaleCommentRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaleCommentRatings_TaleComments_TaleCommentId",
                        column: x => x.TaleCommentId,
                        principalTable: "TaleComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreadCommentFlags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FlaggerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThreadsCommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadCommentFlags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadCommentFlags_ThreadComments_ThreadsCommentId",
                        column: x => x.ThreadsCommentId,
                        principalTable: "ThreadComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreadCommentRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 48, nullable: false),
                    RaterId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThreadsCommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadCommentRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadCommentRatings_ThreadComments_ThreadsCommentId",
                        column: x => x.ThreadsCommentId,
                        principalTable: "ThreadComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountFollowers_AccountId_FollowerId",
                table: "AccountFollowers",
                columns: new[] { "AccountId", "FollowerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Username",
                table: "Accounts",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AccountId",
                table: "Activities",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_AccountId",
                table: "Admins",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_AccountId",
                table: "Contacts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkedTales_WatchListId_TaleId",
                table: "LinkedTales",
                columns: new[] { "WatchListId", "TaleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginHistories_AccountId",
                table: "LoginHistories",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_AccountId",
                table: "Profiles",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaleCommentFlags_TaleCommentId_FlaggerId",
                table: "TaleCommentFlags",
                columns: new[] { "TaleCommentId", "FlaggerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaleCommentRatings_TaleCommentId_RaterId",
                table: "TaleCommentRatings",
                columns: new[] { "TaleCommentId", "RaterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaleComments_ParentId",
                table: "TaleComments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaleComments_TaleId",
                table: "TaleComments",
                column: "TaleId");

            migrationBuilder.CreateIndex(
                name: "IX_TaleFlags_TaleId_FlaggerId",
                table: "TaleFlags",
                columns: new[] { "TaleId", "FlaggerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaleFollowers_TaleId_FollowerId",
                table: "TaleFollowers",
                columns: new[] { "TaleId", "FollowerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaleHistories_TaleId",
                table: "TaleHistories",
                column: "TaleId");

            migrationBuilder.CreateIndex(
                name: "IX_TaleRatings_TaleId_RaterId",
                table: "TaleRatings",
                columns: new[] { "TaleId", "RaterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tales_Url",
                table: "Tales",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaleShares_TaleId_SharerId",
                table: "TaleShares",
                columns: new[] { "TaleId", "SharerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaleTags_TaleId",
                table: "TaleTags",
                column: "TaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadAddendums_ThreadsId",
                table: "ThreadAddendums",
                column: "ThreadsId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadCommentFlags_ThreadsCommentId_FlaggerId",
                table: "ThreadCommentFlags",
                columns: new[] { "ThreadsCommentId", "FlaggerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThreadCommentRatings_ThreadsCommentId_RaterId",
                table: "ThreadCommentRatings",
                columns: new[] { "ThreadsCommentId", "RaterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThreadComments_ParentId",
                table: "ThreadComments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadComments_ThreadsId",
                table: "ThreadComments",
                column: "ThreadsId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadFlags_ThreadsId_FlaggerId",
                table: "ThreadFlags",
                columns: new[] { "ThreadsId", "FlaggerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThreadFollowers_ThreadsId_FollowerId",
                table: "ThreadFollowers",
                columns: new[] { "ThreadsId", "FollowerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThreadRatings_ThreadsId_RaterId",
                table: "ThreadRatings",
                columns: new[] { "ThreadsId", "RaterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Threads_Url",
                table: "Threads",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThreadShares_ThreadsId_SharerId",
                table: "ThreadShares",
                columns: new[] { "ThreadsId", "SharerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThreadTags_ThreadsId",
                table: "ThreadTags",
                column: "ThreadsId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchListFollowers_WatchListId_FollowerId",
                table: "WatchListFollowers",
                columns: new[] { "WatchListId", "FollowerId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountFollowers");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "LinkedTales");

            migrationBuilder.DropTable(
                name: "LoginHistories");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "TaleCommentFlags");

            migrationBuilder.DropTable(
                name: "TaleCommentRatings");

            migrationBuilder.DropTable(
                name: "TaleFlags");

            migrationBuilder.DropTable(
                name: "TaleFollowers");

            migrationBuilder.DropTable(
                name: "TaleHistories");

            migrationBuilder.DropTable(
                name: "TaleRatings");

            migrationBuilder.DropTable(
                name: "TaleShares");

            migrationBuilder.DropTable(
                name: "TaleTags");

            migrationBuilder.DropTable(
                name: "TempUsers");

            migrationBuilder.DropTable(
                name: "ThreadAddendums");

            migrationBuilder.DropTable(
                name: "ThreadCommentFlags");

            migrationBuilder.DropTable(
                name: "ThreadCommentRatings");

            migrationBuilder.DropTable(
                name: "ThreadFlags");

            migrationBuilder.DropTable(
                name: "ThreadFollowers");

            migrationBuilder.DropTable(
                name: "ThreadRatings");

            migrationBuilder.DropTable(
                name: "ThreadShares");

            migrationBuilder.DropTable(
                name: "ThreadTags");

            migrationBuilder.DropTable(
                name: "WatchListFollowers");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "TaleComments");

            migrationBuilder.DropTable(
                name: "ThreadComments");

            migrationBuilder.DropTable(
                name: "WatchLists");

            migrationBuilder.DropTable(
                name: "Tales");

            migrationBuilder.DropTable(
                name: "Threads");
        }
    }
}
