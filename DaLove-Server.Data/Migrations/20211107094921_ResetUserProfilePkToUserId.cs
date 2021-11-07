using Microsoft.EntityFrameworkCore.Migrations;

namespace DaLove_Server.Data.Migrations
{
    public partial class ResetUserProfilePkToUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMemoryUserProfile_UserProfiles_RecipientsUniqueUserName",
                table: "UserMemoryUserProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "RecipientsUniqueUserName",
                table: "UserMemoryUserProfile",
                newName: "RecipientsUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMemoryUserProfile_RecipientsUniqueUserName",
                table: "UserMemoryUserProfile",
                newName: "IX_UserMemoryUserProfile_RecipientsUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UniqueUserName",
                table: "UserProfiles",
                column: "UniqueUserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMemoryUserProfile_UserProfiles_RecipientsUserId",
                table: "UserMemoryUserProfile",
                column: "RecipientsUserId",
                principalTable: "UserProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMemoryUserProfile_UserProfiles_RecipientsUserId",
                table: "UserMemoryUserProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_UniqueUserName",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "RecipientsUserId",
                table: "UserMemoryUserProfile",
                newName: "RecipientsUniqueUserName");

            migrationBuilder.RenameIndex(
                name: "IX_UserMemoryUserProfile_RecipientsUserId",
                table: "UserMemoryUserProfile",
                newName: "IX_UserMemoryUserProfile_RecipientsUniqueUserName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles",
                column: "UniqueUserName");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMemoryUserProfile_UserProfiles_RecipientsUniqueUserName",
                table: "UserMemoryUserProfile",
                column: "RecipientsUniqueUserName",
                principalTable: "UserProfiles",
                principalColumn: "UniqueUserName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
