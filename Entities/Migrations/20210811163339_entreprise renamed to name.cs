using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class entrepriserenamedtoname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnterpriseName",
                table: "AspNetUsers",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "EnterpriseName");
        }
    }
}
