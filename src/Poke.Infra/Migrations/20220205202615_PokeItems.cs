using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Poke.Infra.Migrations
{
    public partial class PokeItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HeldItemId",
                schema: "dbo",
                table: "evolution",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "item",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    held_item = table.Column<bool>(type: "boolean", nullable: false),
                    item_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_evolution_HeldItemId",
                schema: "dbo",
                table: "evolution",
                column: "HeldItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_evolution_item_HeldItemId",
                schema: "dbo",
                table: "evolution",
                column: "HeldItemId",
                principalSchema: "dbo",
                principalTable: "item",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_evolution_item_HeldItemId",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropTable(
                name: "item",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_evolution_HeldItemId",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropColumn(
                name: "HeldItemId",
                schema: "dbo",
                table: "evolution");
        }
    }
}
