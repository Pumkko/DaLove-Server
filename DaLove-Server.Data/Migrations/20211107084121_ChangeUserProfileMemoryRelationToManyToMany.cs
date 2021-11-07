using Microsoft.EntityFrameworkCore.Migrations;

namespace DaLove_Server.Data.Migrations
{
    public partial class ChangeUserProfileMemoryRelationToManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Memories_UserMemoryId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_UserMemoryId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "UserMemoryId",
                table: "UserProfiles");

            migrationBuilder.CreateTable(
                name: "UserMemoryUserProfile",
                columns: table => new
                {
                    MemoriesId = table.Column<int>(type: "int", nullable: false),
                    RecipientsUniqueUserName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMemoryUserProfile", x => new { x.MemoriesId, x.RecipientsUniqueUserName });
                    table.ForeignKey(
                        name: "FK_UserMemoryUserProfile_Memories_MemoriesId",
                        column: x => x.MemoriesId,
                        principalTable: "Memories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMemoryUserProfile_UserProfiles_RecipientsUniqueUserName",
                        column: x => x.RecipientsUniqueUserName,
                        principalTable: "UserProfiles",
                        principalColumn: "UniqueUserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMemoryUserProfile_RecipientsUniqueUserName",
                table: "UserMemoryUserProfile",
                column: "RecipientsUniqueUserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMemoryUserProfile");

            migrationBuilder.AddColumn<int>(
                name: "UserMemoryId",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserMemoryId",
                table: "UserProfiles",
                column: "UserMemoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Memories_UserMemoryId",
                table: "UserProfiles",
                column: "UserMemoryId",
                principalTable: "Memories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
