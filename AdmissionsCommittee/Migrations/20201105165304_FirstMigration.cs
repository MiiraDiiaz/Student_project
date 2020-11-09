using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionsCommittee.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NameCourseId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NameCourses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NationaleAndCountries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationaleAndCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDocumentFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityDocument = table.Column<byte[]>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDocumentFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDocumentFiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    BirthDay = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeDocument = table.Column<string>(nullable: true),
                    SeriesNamberDocument = table.Column<string>(nullable: true),
                    DateIssuanceDicument = table.Column<DateTime>(nullable: false),
                    IssuanceOffice = table.Column<string>(nullable: true),
                    BirthPlace = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    NationalyAndCountry = table.Column<int>(nullable: false),
                    NationaleAndCountryId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDocuments_NationaleAndCountries_NationaleAndCountryId",
                        column: x => x.NationaleAndCountryId,
                        principalTable: "NationaleAndCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDocuments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NameCourseId",
                table: "AspNetUsers",
                column: "NameCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocumentFiles_UserId",
                table: "UserDocumentFiles",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_NationaleAndCountryId",
                table: "UserDocuments",
                column: "NationaleAndCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_UserId",
                table: "UserDocuments",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_NameCourses_NameCourseId",
                table: "AspNetUsers",
                column: "NameCourseId",
                principalTable: "NameCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_NameCourses_NameCourseId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "NameCourses");

            migrationBuilder.DropTable(
                name: "UserDocumentFiles");

            migrationBuilder.DropTable(
                name: "UserDocuments");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "NationaleAndCountries");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NameCourseId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NameCourseId",
                table: "AspNetUsers");
        }
    }
}
