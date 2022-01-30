using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Poke.Infra.Migrations
{
    public partial class PokemonEvolution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "evolution",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_id = table.Column<Guid>(type: "uuid", nullable: false),
                    to_id = table.Column<Guid>(type: "uuid", nullable: false),
                    evolution_type = table.Column<int>(type: "integer", nullable: false),
                    pokemon_evolution_level = table.Column<int>(type: "integer", nullable: true),
                    evolution_stone = table.Column<int>(type: "integer", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evolution", x => new { x.id, x.to_id, x.from_id });
                    table.ForeignKey(
                        name: "FK_evolution_pokemon_from_id",
                        column: x => x.from_id,
                        principalSchema: "dbo",
                        principalTable: "pokemon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_evolution_pokemon_to_id",
                        column: x => x.to_id,
                        principalSchema: "dbo",
                        principalTable: "pokemon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_evolution_from_id",
                schema: "dbo",
                table: "evolution",
                column: "from_id");

            migrationBuilder.CreateIndex(
                name: "IX_evolution_to_id",
                schema: "dbo",
                table: "evolution",
                column: "to_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "evolution",
                schema: "dbo");
        }
    }
}
