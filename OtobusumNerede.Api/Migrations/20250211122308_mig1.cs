using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtobusumNerede.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Duraklar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurakKodu = table.Column<int>(type: "int", nullable: false),
                    Enlem = table.Column<double>(type: "float", nullable: false),
                    Boylam = table.Column<double>(type: "float", nullable: false),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngelliKullaniminaUygunMu = table.Column<bool>(type: "bit", nullable: false),
                    AkilliMi = table.Column<bool>(type: "bit", nullable: false),
                    IlceAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duraklar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hatlar",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HatAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hatlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtobusRotalari",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    GUZERGAH_A = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GUZERGAH_K = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HAT_ADI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HAT_KODU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DEPAR_NO = table.Column<int>(type: "int", nullable: false),
                    DURUM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UZUNLUK = table.Column<double>(type: "float", nullable: false),
                    HAT_BASI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HAT_SONU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YON = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HAT_ID = table.Column<int>(type: "int", nullable: false),
                    SURE = table.Column<double>(type: "float", nullable: false),
                    RING_MI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    B_NOKTASI = table.Column<int>(type: "int", nullable: false),
                    RouteGeometry = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtobusRotalari", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Duraklar");

            migrationBuilder.DropTable(
                name: "Hatlar");

            migrationBuilder.DropTable(
                name: "OtobusRotalari");
        }
    }
}
