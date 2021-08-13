using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class adressrenamedtoaddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adresse",
                table: "Sites",
                newName: "Addresse");

            migrationBuilder.RenameColumn(
                name: "Adresse",
                table: "Fournisseurs",
                newName: "Addresse");

            migrationBuilder.RenameColumn(
                name: "Adresse",
                table: "Clients",
                newName: "Addresse");

            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "AspNetUsers",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Addresse",
                table: "Sites",
                newName: "Adresse");

            migrationBuilder.RenameColumn(
                name: "Addresse",
                table: "Fournisseurs",
                newName: "Adresse");

            migrationBuilder.RenameColumn(
                name: "Addresse",
                table: "Clients",
                newName: "Adresse");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "AspNetUsers",
                newName: "Adress");
        }
    }
}
