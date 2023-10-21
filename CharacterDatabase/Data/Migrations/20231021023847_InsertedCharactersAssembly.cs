using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterDatabase.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertedCharactersAssembly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Species = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Speed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Subclass1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Class1Level = table.Column<int>(type: "int", nullable: false),
                    Class2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Subclass2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Class2Level = table.Column<int>(type: "int", nullable: true),
                    Class3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Subclass3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Class3Level = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    HairColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EyeColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Dexterity = table.Column<int>(type: "int", nullable: false),
                    Constitution = table.Column<int>(type: "int", nullable: false),
                    Intelligence = table.Column<int>(type: "int", nullable: false),
                    Wisdom = table.Column<int>(type: "int", nullable: false),
                    Charisma = table.Column<int>(type: "int", nullable: false),
                    CharacterNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserInfo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
