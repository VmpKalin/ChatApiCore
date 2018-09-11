using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Data.Migrations
{
    public partial class comunityService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comunities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FollowingFromId = table.Column<string>(nullable: true),
                    FollowingToId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UserEntityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comunities_Users_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comunities_UserEntityId",
                table: "Comunities",
                column: "UserEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comunities");
        }
    }
}
