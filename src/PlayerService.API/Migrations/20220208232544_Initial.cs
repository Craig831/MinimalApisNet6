using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerService.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MlbPlayer",
                columns: table => new
                {
                    MlbPlayerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MlbTeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerFirstName = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerLastName = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerFullName = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerNickName = table.Column<string>(type: "TEXT", nullable: true),
                    PositionId = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MlbPlayer", x => x.MlbPlayerId);
                });

            migrationBuilder.CreateTable(
                name: "MlbTeam",
                columns: table => new
                {
                    MlbTeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamName = table.Column<string>(type: "TEXT", nullable: false),
                    TeamAbbreviation = table.Column<string>(type: "TEXT", nullable: false),
                    TeamCity = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MlbTeam", x => x.MlbTeamId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MlbPlayer");
            migrationBuilder.DropTable(
                name: "MlbTeam");
        }
    }
}
