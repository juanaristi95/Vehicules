using Microsoft.EntityFrameworkCore.Migrations;

namespace Vehicules.API.Migrations
{
    public partial class AddTableProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Procedure",
                table: "Procedure");

            migrationBuilder.RenameTable(
                name: "Procedure",
                newName: "Procedures");

            migrationBuilder.RenameIndex(
                name: "IX_Procedure_Description",
                table: "Procedures",
                newName: "IX_Procedures_Description");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Procedures",
                table: "Procedures",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Procedures",
                table: "Procedures");

            migrationBuilder.RenameTable(
                name: "Procedures",
                newName: "Procedure");

            migrationBuilder.RenameIndex(
                name: "IX_Procedures_Description",
                table: "Procedure",
                newName: "IX_Procedure_Description");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Procedure",
                table: "Procedure",
                column: "Id");
        }
    }
}
