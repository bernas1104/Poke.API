using Microsoft.EntityFrameworkCore.Migrations;

namespace Poke.Infra.Migrations
{
    public partial class PokeItemsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_evolution_item_HeldItemId",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.RenameColumn(
                name: "HeldItemId",
                schema: "dbo",
                table: "evolution",
                newName: "held_item_id");

            migrationBuilder.RenameIndex(
                name: "IX_evolution_HeldItemId",
                schema: "dbo",
                table: "evolution",
                newName: "IX_evolution_held_item_id");

            migrationBuilder.AddForeignKey(
                name: "FK_evolution_item_held_item_id",
                schema: "dbo",
                table: "evolution",
                column: "held_item_id",
                principalSchema: "dbo",
                principalTable: "item",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_evolution_item_held_item_id",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.RenameColumn(
                name: "held_item_id",
                schema: "dbo",
                table: "evolution",
                newName: "HeldItemId");

            migrationBuilder.RenameIndex(
                name: "IX_evolution_held_item_id",
                schema: "dbo",
                table: "evolution",
                newName: "IX_evolution_HeldItemId");

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
    }
}
