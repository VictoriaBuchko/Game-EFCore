using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Games.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGameModeAndCopiesSold : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Завдання 4: додаємо режим гри
            migrationBuilder.AddColumn<string>(
                name: "GameMode",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            // Завдання 4: додаємо кількість проданих копій
            migrationBuilder.AddColumn<int>(
                name: "CopiesSold",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "GameMode", table: "Games");
            migrationBuilder.DropColumn(name: "CopiesSold", table: "Games");
        }
    }
}
