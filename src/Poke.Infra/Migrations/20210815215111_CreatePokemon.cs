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
                name: "Pokemons",
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
                    table.PrimaryKey("PK_Pokemons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BaseStats",
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
                    table.PrimaryKey("PK_BaseStats", x => x.id);
                    table.ForeignKey(
                        name: "FK_BaseStats_Pokemons_pokemon_id",
                        column: x => x.pokemon_id,
                        principalSchema: "dbo",
                        principalTable: "Pokemons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evolutions",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PkmnEvolutionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolutions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Evolutions_Pokemons_PkmnEvolutionId",
                        column: x => x.PkmnEvolutionId,
                        principalSchema: "dbo",
                        principalTable: "Pokemons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreEvolutions",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PkmnPreEvolutionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreEvolutions", x => x.id);
                    table.ForeignKey(
                        name: "FK_PreEvolutions_Pokemons_PkmnPreEvolutionId",
                        column: x => x.PkmnPreEvolutionId,
                        principalSchema: "dbo",
                        principalTable: "Pokemons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tranings",
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
                    table.PrimaryKey("PK_Tranings", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tranings_Pokemons_pokemon_id",
                        column: x => x.pokemon_id,
                        principalSchema: "dbo",
                        principalTable: "Pokemons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseStats_pokemon_id",
                schema: "dbo",
                table: "BaseStats",
                column: "pokemon_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evolutions_PkmnEvolutionId",
                schema: "dbo",
                table: "Evolutions",
                column: "PkmnEvolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_PreEvolutions_PkmnPreEvolutionId",
                schema: "dbo",
                table: "PreEvolutions",
                column: "PkmnPreEvolutionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tranings_pokemon_id",
                schema: "dbo",
                table: "Tranings",
                column: "pokemon_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseStats",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Evolutions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PreEvolutions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Tranings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Pokemons",
                schema: "dbo");
        }
    }
}
