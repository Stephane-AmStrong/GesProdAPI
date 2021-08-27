using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class nometprenom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nom",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "Clients");
        }
    }
}
