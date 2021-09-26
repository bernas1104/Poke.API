using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Poke.Infra.Migrations
{
    public partial class CreatePokemon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "pokemon",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "varchar", nullable: true),
                    species = table.Column<string>(type: "varchar", nullable: true),
                    height = table.Column<decimal>(type: "numeric(38,17)", nullable: false),
                    weight = table.Column<decimal>(type: "numeric(38,17)", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    first_type = table.Column<string>(type: "varchar", nullable: false),
                    second_type = table.Column<string>(type: "varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pokemon", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "base_stats",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hitpoints = table.Column<int>(type: "integer", nullable: true),
                    attack = table.Column<int>(type: "integer", nullable: true),
                    defense = table.Column<int>(type: "integer", nullable: true),
                    special_attack = table.Column<int>(type: "integer", nullable: true),
                    special_defense = table.Column<int>(type: "integer", nullable: true),
                    speed = table.Column<int>(type: "integer", nullable: true),
                    PokemonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_base_stats", x => x.id);
                    table.ForeignKey(
                        name: "FK_base_stats_pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalSchema: "dbo",
                        principalTable: "pokemon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "training",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ev_yeld = table.Column<int>(type: "integer", nullable: false),
                    base_friendship = table.Column<int>(type: "integer", nullable: false),
                    growth_rate = table.Column<int>(type: "integer", nullable: false),
                    PokemonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_training", x => x.id);
                    table.ForeignKey(
                        name: "FK_training_pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalSchema: "dbo",
                        principalTable: "pokemon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_base_stats_PokemonId",
                schema: "dbo",
                table: "base_stats",
                column: "PokemonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pokemon_number",
                schema: "dbo",
                table: "pokemon",
                column: "number");

            migrationBuilder.CreateIndex(
                name: "IX_training_PokemonId",
                schema: "dbo",
                table: "training",
                column: "PokemonId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "base_stats",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "training",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "pokemon",
                schema: "dbo");
        }
    }
}
