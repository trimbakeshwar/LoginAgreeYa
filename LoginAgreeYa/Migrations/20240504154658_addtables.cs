using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginAgreeYa.Migrations
{
    public partial class addtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginUser = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registration_Email",
                table: "Registration",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registration_LoginUser",
                table: "Registration",
                column: "LoginUser",
                unique: true,
                filter: "[LoginUser] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_PhoneNumber",
                table: "Registration",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registration");
        }
    }
}
