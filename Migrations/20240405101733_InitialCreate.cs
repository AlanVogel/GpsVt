using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpeedSight.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GpsDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<int>(type: "int", nullable: false),
                    Utc = table.Column<int>(type: "int", nullable: false),
                    Match_X = table.Column<double>(type: "float", nullable: false),
                    Match_Y = table.Column<double>(type: "float", nullable: false),
                    Org_X = table.Column<double>(type: "float", nullable: false),
                    Org_Y = table.Column<double>(type: "float", nullable: false),
                    Match_Distance = table.Column<int>(type: "int", nullable: false),
                    Match_H = table.Column<int>(type: "int", nullable: false),
                    Match_Speed = table.Column<int>(type: "int", nullable: false),
                    Org_H = table.Column<int>(type: "int", nullable: false),
                    Org_Speed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpsDatas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GpsDatas");
        }
    }
}
