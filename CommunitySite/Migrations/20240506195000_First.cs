using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunitySite.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SZABOKD");

            migrationBuilder.CreateSequence(
                name: "FELHASZNALO_SEQ",
                schema: "SZABOKD");

            migrationBuilder.CreateTable(
                name: "PERMISSIONS",
                schema: "SZABOKD",
                columns: table => new
                {
                    PERMISSIONID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PERMISSION_NAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C0010560", x => x.PERMISSIONID);
                });

            migrationBuilder.CreateTable(
                name: "SITEUSERS",
                schema: "SZABOKD",
                columns: table => new
                {
                    USERID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PERMISSIONID = table.Column<decimal>(type: "NUMBER(38)", nullable: true, defaultValueSql: "(1)"),
                    USERNAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true),
                    SUR_NAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true),
                    LAST_NAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true),
                    WORKPLACE = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true),
                    SCHOOL = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true),
                    BirthDate = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    USERTECHNICALNAME = table.Column<Guid>(type: "RAW(16)", nullable: false, defaultValueSql: "(sys_guid()) ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C0010562", x => x.USERID);
                    table.ForeignKey(
                        name: "SYS_C0010563",
                        column: x => x.PERMISSIONID,
                        principalSchema: "SZABOKD",
                        principalTable: "PERMISSIONS",
                        principalColumn: "PERMISSIONID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FRIENDS",
                schema: "SZABOKD",
                columns: table => new
                {
                    FRIENDROWID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FRIENDID1 = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    FRIENDID2 = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    FRIEND_START_DATE = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true),
                    IS_FRIEND = table.Column<decimal>(type: "NUMBER(38)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FRIENDS_PK", x => x.FRIENDROWID);
                    table.ForeignKey(
                        name: "SYS_C0010564",
                        column: x => x.FRIENDID1,
                        principalSchema: "SZABOKD",
                        principalTable: "SITEUSERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "SYS_C0010565",
                        column: x => x.FRIENDID2,
                        principalSchema: "SZABOKD",
                        principalTable: "SITEUSERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MESSAGES",
                schema: "SZABOKD",
                columns: table => new
                {
                    MESSAGEID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SENDERID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    RECEIVERID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    MESSAGE_TEXT = table.Column<string>(type: "VARCHAR2(1000)", unicode: false, maxLength: 1000, nullable: true),
                    SEND_DATE = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C0010567", x => x.MESSAGEID);
                    table.ForeignKey(
                        name: "SYS_C0010568",
                        column: x => x.SENDERID,
                        principalSchema: "SZABOKD",
                        principalTable: "SITEUSERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "SYS_C0010569",
                        column: x => x.RECEIVERID,
                        principalSchema: "SZABOKD",
                        principalTable: "SITEUSERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PHOTOS",
                schema: "SZABOKD",
                columns: table => new
                {
                    PHOTOID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USERID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    PHOTO_TYPE = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true),
                    PHOTO_SIZE = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    PHOTO_IN_BYTE = table.Column<byte[]>(type: "BLOB", nullable: true),
                    PHOTO_NAME = table.Column<string>(type: "VARCHAR2(1000)", unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C0010576", x => x.PHOTOID);
                    table.ForeignKey(
                        name: "SYS_C0010577",
                        column: x => x.USERID,
                        principalSchema: "SZABOKD",
                        principalTable: "SITEUSERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SITEGROUPS",
                schema: "SZABOKD",
                columns: table => new
                {
                    GROUPID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    OWNERID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    NAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true),
                    GROUPTECHNICALNAME = table.Column<Guid>(type: "RAW(16)", maxLength: 40, nullable: false, defaultValueSql: "(sys_guid()) ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C0010571", x => x.GROUPID);
                    table.ForeignKey(
                        name: "SYS_C0010572",
                        column: x => x.OWNERID,
                        principalSchema: "SZABOKD",
                        principalTable: "SITEUSERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MANAGEGROUPS",
                schema: "SZABOKD",
                columns: table => new
                {
                    MEMBERROWID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USERID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    GROUPID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    JOIN_DATE = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MANAGEGROUPS_PK", x => x.MEMBERROWID);
                    table.ForeignKey(
                        name: "SYS_C0010573",
                        column: x => x.USERID,
                        principalSchema: "SZABOKD",
                        principalTable: "SITEUSERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "SYS_C0010574",
                        column: x => x.GROUPID,
                        principalSchema: "SZABOKD",
                        principalTable: "SITEGROUPS",
                        principalColumn: "GROUPID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "POSTS",
                schema: "SZABOKD",
                columns: table => new
                {
                    POSTID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USERID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    PHOTOID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    GROUPID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    POST_TEXT = table.Column<string>(type: "VARCHAR2(1000)", unicode: false, maxLength: 1000, nullable: true),
                    POST_DATE = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C0010579", x => x.POSTID);
                    table.ForeignKey(
                        name: "SYS_C0010580",
                        column: x => x.USERID,
                        principalSchema: "SZABOKD",
                        principalTable: "SITEUSERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "SYS_C0010581",
                        column: x => x.PHOTOID,
                        principalSchema: "SZABOKD",
                        principalTable: "PHOTOS",
                        principalColumn: "PHOTOID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "SYS_C0010582",
                        column: x => x.GROUPID,
                        principalSchema: "SZABOKD",
                        principalTable: "SITEGROUPS",
                        principalColumn: "GROUPID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SITECOMMENTS",
                schema: "SZABOKD",
                columns: table => new
                {
                    COMMENTID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    POSTID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    USERID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    COMMENT_TEXT = table.Column<string>(type: "VARCHAR2(1000)", unicode: false, maxLength: 1000, nullable: true),
                    COMMENT_DATE = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C0010584", x => x.COMMENTID);
                    table.ForeignKey(
                        name: "SYS_C0010585",
                        column: x => x.POSTID,
                        principalSchema: "SZABOKD",
                        principalTable: "POSTS",
                        principalColumn: "POSTID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "SYS_C0010586",
                        column: x => x.USERID,
                        principalSchema: "SZABOKD",
                        principalTable: "SITEUSERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INCLUDES",
                schema: "SZABOKD",
                columns: table => new
                {
                    INCLUDEROWID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    POSTID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    COMMENTID = table.Column<decimal>(type: "NUMBER(38)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("INCLUDES_PK", x => x.INCLUDEROWID);
                    table.ForeignKey(
                        name: "SYS_C0010587",
                        column: x => x.POSTID,
                        principalSchema: "SZABOKD",
                        principalTable: "POSTS",
                        principalColumn: "POSTID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "SYS_C0010588",
                        column: x => x.COMMENTID,
                        principalSchema: "SZABOKD",
                        principalTable: "SITECOMMENTS",
                        principalColumn: "COMMENTID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FRIENDS_FRIENDID1",
                schema: "SZABOKD",
                table: "FRIENDS",
                column: "FRIENDID1");

            migrationBuilder.CreateIndex(
                name: "IX_FRIENDS_FRIENDID2",
                schema: "SZABOKD",
                table: "FRIENDS",
                column: "FRIENDID2");

            migrationBuilder.CreateIndex(
                name: "IX_INCLUDES_COMMENTID",
                schema: "SZABOKD",
                table: "INCLUDES",
                column: "COMMENTID");

            migrationBuilder.CreateIndex(
                name: "IX_INCLUDES_POSTID",
                schema: "SZABOKD",
                table: "INCLUDES",
                column: "POSTID");

            migrationBuilder.CreateIndex(
                name: "IX_MANAGEGROUPS_GROUPID",
                schema: "SZABOKD",
                table: "MANAGEGROUPS",
                column: "GROUPID");

            migrationBuilder.CreateIndex(
                name: "IX_MANAGEGROUPS_USERID",
                schema: "SZABOKD",
                table: "MANAGEGROUPS",
                column: "USERID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGES_RECEIVERID",
                schema: "SZABOKD",
                table: "MESSAGES",
                column: "RECEIVERID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGES_SENDERID",
                schema: "SZABOKD",
                table: "MESSAGES",
                column: "SENDERID");

            migrationBuilder.CreateIndex(
                name: "IX_PHOTOS_USERID",
                schema: "SZABOKD",
                table: "PHOTOS",
                column: "USERID");

            migrationBuilder.CreateIndex(
                name: "IX_POSTS_GROUPID",
                schema: "SZABOKD",
                table: "POSTS",
                column: "GROUPID");

            migrationBuilder.CreateIndex(
                name: "IX_POSTS_PHOTOID",
                schema: "SZABOKD",
                table: "POSTS",
                column: "PHOTOID");

            migrationBuilder.CreateIndex(
                name: "IX_POSTS_USERID",
                schema: "SZABOKD",
                table: "POSTS",
                column: "USERID");

            migrationBuilder.CreateIndex(
                name: "IX_SITECOMMENTS_POSTID",
                schema: "SZABOKD",
                table: "SITECOMMENTS",
                column: "POSTID");

            migrationBuilder.CreateIndex(
                name: "IX_SITECOMMENTS_USERID",
                schema: "SZABOKD",
                table: "SITECOMMENTS",
                column: "USERID");

            migrationBuilder.CreateIndex(
                name: "IX_SITEGROUPS_OWNERID",
                schema: "SZABOKD",
                table: "SITEGROUPS",
                column: "OWNERID");

            migrationBuilder.CreateIndex(
                name: "IX_SITEUSERS_PERMISSIONID",
                schema: "SZABOKD",
                table: "SITEUSERS",
                column: "PERMISSIONID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FRIENDS",
                schema: "SZABOKD");

            migrationBuilder.DropTable(
                name: "INCLUDES",
                schema: "SZABOKD");

            migrationBuilder.DropTable(
                name: "MANAGEGROUPS",
                schema: "SZABOKD");

            migrationBuilder.DropTable(
                name: "MESSAGES",
                schema: "SZABOKD");

            migrationBuilder.DropTable(
                name: "SITECOMMENTS",
                schema: "SZABOKD");

            migrationBuilder.DropTable(
                name: "POSTS",
                schema: "SZABOKD");

            migrationBuilder.DropTable(
                name: "PHOTOS",
                schema: "SZABOKD");

            migrationBuilder.DropTable(
                name: "SITEGROUPS",
                schema: "SZABOKD");

            migrationBuilder.DropTable(
                name: "SITEUSERS",
                schema: "SZABOKD");

            migrationBuilder.DropTable(
                name: "PERMISSIONS",
                schema: "SZABOKD");

            migrationBuilder.DropSequence(
                name: "FELHASZNALO_SEQ",
                schema: "SZABOKD");
        }
    }
}
