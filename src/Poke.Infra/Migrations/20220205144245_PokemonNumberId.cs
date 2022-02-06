using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Poke.Infra.Migrations
{
    public partial class PokemonNumberId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_base_stats_pokemon_pokemon_id",
                schema: "dbo",
                table: "base_stats");

            migrationBuilder.DropForeignKey(
                name: "FK_evolution_pokemon_from_id",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropForeignKey(
                name: "FK_evolution_pokemon_to_id",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropForeignKey(
                name: "FK_training_pokemon_pokemon_id",
                schema: "dbo",
                table: "training");

            migrationBuilder.DropIndex(
                name: "IX_training_pokemon_id",
                schema: "dbo",
                table: "training");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pokemon",
                schema: "dbo",
                table: "pokemon");

            migrationBuilder.DropIndex(
                name: "IX_pokemon_number",
                schema: "dbo",
                table: "pokemon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_evolution",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropIndex(
                name: "IX_evolution_from_id",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropIndex(
                name: "IX_evolution_to_id",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropIndex(
                name: "IX_base_stats_pokemon_id",
                schema: "dbo",
                table: "base_stats");

            migrationBuilder.DropColumn(
                name: "pokemon_id",
                schema: "dbo",
                table: "training");

            migrationBuilder.DropColumn(
                name: "to_id",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropColumn(
                name: "from_id",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropColumn(
                name: "pokemon_id",
                schema: "dbo",
                table: "base_stats");

            migrationBuilder.AddColumn<int>(
                name: "pokemon_number",
                schema: "dbo",
                table: "training",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "to_number",
                schema: "dbo",
                table: "evolution",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "from_number",
                schema: "dbo",
                table: "evolution",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pokemon_number",
                schema: "dbo",
                table: "base_stats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_pokemon",
                schema: "dbo",
                table: "pokemon",
                column: "number");

            migrationBuilder.AddPrimaryKey(
                name: "PK_evolution",
                schema: "dbo",
                table: "evolution",
                columns: new[] { "id", "to_number", "from_number" });

            migrationBuilder.CreateIndex(
                name: "IX_training_pokemon_number",
                schema: "dbo",
                table: "training",
                column: "pokemon_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pokemon_id",
                schema: "dbo",
                table: "pokemon",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_evolution_from_number",
                schema: "dbo",
                table: "evolution",
                column: "from_number");

            migrationBuilder.CreateIndex(
                name: "IX_evolution_to_number",
                schema: "dbo",
                table: "evolution",
                column: "to_number");

            migrationBuilder.CreateIndex(
                name: "IX_base_stats_pokemon_number",
                schema: "dbo",
                table: "base_stats",
                column: "pokemon_number",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_base_stats_pokemon_pokemon_number",
                schema: "dbo",
                table: "base_stats",
                column: "pokemon_number",
                principalSchema: "dbo",
                principalTable: "pokemon",
                principalColumn: "number",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_evolution_pokemon_from_number",
                schema: "dbo",
                table: "evolution",
                column: "from_number",
                principalSchema: "dbo",
                principalTable: "pokemon",
                principalColumn: "number",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_evolution_pokemon_to_number",
                schema: "dbo",
                table: "evolution",
                column: "to_number",
                principalSchema: "dbo",
                principalTable: "pokemon",
                principalColumn: "number",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_training_pokemon_pokemon_number",
                schema: "dbo",
                table: "training",
                column: "pokemon_number",
                principalSchema: "dbo",
                principalTable: "pokemon",
                principalColumn: "number",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_base_stats_pokemon_pokemon_number",
                schema: "dbo",
                table: "base_stats");

            migrationBuilder.DropForeignKey(
                name: "FK_evolution_pokemon_from_number",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropForeignKey(
                name: "FK_evolution_pokemon_to_number",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropForeignKey(
                name: "FK_training_pokemon_pokemon_number",
                schema: "dbo",
                table: "training");

            migrationBuilder.DropIndex(
                name: "IX_training_pokemon_number",
                schema: "dbo",
                table: "training");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pokemon",
                schema: "dbo",
                table: "pokemon");

            migrationBuilder.DropIndex(
                name: "IX_pokemon_id",
                schema: "dbo",
                table: "pokemon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_evolution",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropIndex(
                name: "IX_evolution_from_number",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropIndex(
                name: "IX_evolution_to_number",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropIndex(
                name: "IX_base_stats_pokemon_number",
                schema: "dbo",
                table: "base_stats");

            migrationBuilder.DropColumn(
                name: "pokemon_number",
                schema: "dbo",
                table: "training");

            migrationBuilder.DropColumn(
                name: "to_number",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropColumn(
                name: "from_number",
                schema: "dbo",
                table: "evolution");

            migrationBuilder.DropColumn(
                name: "pokemon_number",
                schema: "dbo",
                table: "base_stats");

            migrationBuilder.AddColumn<Guid>(
                name: "pokemon_id",
                schema: "dbo",
                table: "training",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "to_id",
                schema: "dbo",
                table: "evolution",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "from_id",
                schema: "dbo",
                table: "evolution",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "pokemon_id",
                schema: "dbo",
                table: "base_stats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_pokemon",
                schema: "dbo",
                table: "pokemon",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_evolution",
                schema: "dbo",
                table: "evolution",
                columns: new[] { "id", "to_id", "from_id" });

            migrationBuilder.CreateIndex(
                name: "IX_training_pokemon_id",
                schema: "dbo",
                table: "training",
                column: "pokemon_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pokemon_number",
                schema: "dbo",
                table: "pokemon",
                column: "number");

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

            migrationBuilder.CreateIndex(
                name: "IX_base_stats_pokemon_id",
                schema: "dbo",
                table: "base_stats",
                column: "pokemon_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_base_stats_pokemon_pokemon_id",
                schema: "dbo",
                table: "base_stats",
                column: "pokemon_id",
                principalSchema: "dbo",
                principalTable: "pokemon",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_evolution_pokemon_from_id",
                schema: "dbo",
                table: "evolution",
                column: "from_id",
                principalSchema: "dbo",
                principalTable: "pokemon",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_evolution_pokemon_to_id",
                schema: "dbo",
                table: "evolution",
                column: "to_id",
                principalSchema: "dbo",
                principalTable: "pokemon",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_training_pokemon_pokemon_id",
                schema: "dbo",
                table: "training",
                column: "pokemon_id",
                principalSchema: "dbo",
                principalTable: "pokemon",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
