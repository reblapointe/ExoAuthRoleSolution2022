using Microsoft.EntityFrameworkCore.Migrations;

namespace ExoAuthRoleSolution2022.Data.Migrations
{
    public partial class CreationModeleVetements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vetement",
                columns: table => new
                {
                    VetementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vetement", x => x.VetementId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vetement");
        }
    }
}
