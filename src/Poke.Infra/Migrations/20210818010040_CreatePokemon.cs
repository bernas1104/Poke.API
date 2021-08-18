using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    number = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    species = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    height = table.Column<decimal>(type: "numeric(38,17)", nullable: false),
                    weight = table.Column<decimal>(type: "numeric(38,17)", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    first_type = table.Column<string>(type: "varchar", maxLength: 10, nullable: false),
                    second_type = table.Column<string>(type: "varchar", maxLength: 10, nullable: true)
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
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hitpoints = table.Column<int>(type: "integer", nullable: false),
                    attack = table.Column<int>(type: "integer", nullable: false),
                    defense = table.Column<int>(type: "integer", nullable: false),
                    special_attack = table.Column<int>(type: "integer", nullable: false),
                    special_defense = table.Column<int>(type: "integer", nullable: false),
                    speed = table.Column<int>(type: "integer", nullable: false),
                    pokemon_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_base_stats", x => x.id);
                    table.ForeignKey(
                        name: "FK_base_stats_pokemon_pokemon_id",
                        column: x => x.pokemon_id,
                        principalSchema: "dbo",
                        principalTable: "pokemon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "traning",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ev_yeld = table.Column<int>(type: "integer", nullable: false),
                    catch_rate = table.Column<decimal>(type: "numeric(38,17)", nullable: false),
                    base_friendship = table.Column<int>(type: "integer", nullable: false),
                    base_experience = table.Column<int>(type: "integer", nullable: false),
                    growth_rate = table.Column<int>(type: "integer", nullable: false),
                    pokemon_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_traning", x => x.id);
                    table.ForeignKey(
                        name: "FK_traning_pokemon_pokemon_id",
                        column: x => x.pokemon_id,
                        principalSchema: "dbo",
                        principalTable: "pokemon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_base_stats_pokemon_id",
                schema: "dbo",
                table: "base_stats",
                column: "pokemon_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pokemon_number",
                schema: "dbo",
                table: "pokemon",
                column: "number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_traning_pokemon_id",
                schema: "dbo",
                table: "traning",
                column: "pokemon_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "base_stats",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "traning",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "pokemon",
                schema: "dbo");
        }
    }
}
