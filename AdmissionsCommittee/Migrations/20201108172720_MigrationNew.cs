using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionsCommittee.Migrations
{
    public partial class MigrationNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_NameCourses_NameCourseId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDocuments_NationaleAndCountries_NationaleAndCountryId",
                table: "UserDocuments");

            migrationBuilder.DropTable(
                name: "NationaleAndCountries");

            migrationBuilder.DropIndex(
                name: "IX_UserDocuments_NationaleAndCountryId",
                table: "UserDocuments");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NameCourseId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NationaleAndCountryId",
                table: "UserDocuments");

            migrationBuilder.DropColumn(
                name: "NationalyAndCountry",
                table: "UserDocuments");

            migrationBuilder.DropColumn(
                name: "NameCourseId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NationaleAndCountryId",
                table: "UserDocuments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NationalyAndCountry",
                table: "UserDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NameCourseId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NationaleAndCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationaleAndCountries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_NationaleAndCountryId",
                table: "UserDocuments",
                column: "NationaleAndCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NameCourseId",
                table: "AspNetUsers",
                column: "NameCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_NameCourses_NameCourseId",
                table: "AspNetUsers",
                column: "NameCourseId",
                principalTable: "NameCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDocuments_NationaleAndCountries_NationaleAndCountryId",
                table: "UserDocuments",
                column: "NationaleAndCountryId",
                principalTable: "NationaleAndCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
