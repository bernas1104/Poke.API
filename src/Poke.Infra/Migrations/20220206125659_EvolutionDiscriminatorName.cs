using Microsoft.EntityFrameworkCore.Migrations;

namespace Poke.Infra.Migrations
{
    public partial class EvolutionDiscriminatorName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                schema: "dbo",
                table: "evolution",
                newName: "discriminator");

            migrationBuilder.AlterColumn<string>(
                name: "discriminator",
                schema: "dbo",
                table: "evolution",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "discriminator",
                schema: "dbo",
                table: "evolution",
                newName: "Discriminator");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                schema: "dbo",
                table: "evolution",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");
        }
    }
}
