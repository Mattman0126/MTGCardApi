using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTGCardApi.Migrations
{
    /// <inheritdoc />
    public partial class MagicDeck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MagicDeck",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Format = table.Column<int>(type: "int", nullable: false),
                    CommanderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FullyObtained = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagicDeck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MagicDeck_MagicCards_CommanderId",
                        column: x => x.CommanderId,
                        principalTable: "MagicCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeckCard",
                columns: table => new
                {
                    DeckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Obtained = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckCard", x => new { x.DeckId, x.CardId });
                    table.ForeignKey(
                        name: "FK_DeckCard_MagicCards_CardId",
                        column: x => x.CardId,
                        principalTable: "MagicCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeckCard_MagicDeck_DeckId",
                        column: x => x.DeckId,
                        principalTable: "MagicDeck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeckCard_CardId",
                table: "DeckCard",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_MagicDeck_CommanderId",
                table: "MagicDeck",
                column: "CommanderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeckCard");

            migrationBuilder.DropTable(
                name: "MagicDeck");
        }
    }
}
