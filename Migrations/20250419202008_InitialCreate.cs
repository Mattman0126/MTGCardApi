using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTGCardApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MagicCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CardUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScryfallUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmallImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LargeImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PngImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtCropImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderCropImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManaCost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cmc = table.Column<double>(type: "float", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Power = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Toughness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Colors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorIdentity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Legalities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SetAbbr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SetUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScryfallSetUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RulingsUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrintSearchUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Digital = table.Column<bool>(type: "bit", nullable: false),
                    Rarity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlavorText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtistName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorderColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullArt = table.Column<bool>(type: "bit", nullable: false),
                    Textless = table.Column<bool>(type: "bit", nullable: false),
                    Booster = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagicCards", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MagicCards");
        }
    }
}
