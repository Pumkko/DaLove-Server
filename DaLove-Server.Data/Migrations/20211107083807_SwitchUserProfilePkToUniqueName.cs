using Microsoft.EntityFrameworkCore.Migrations;

namespace DaLove_Server.Data.Migrations
{
    public partial class SwitchUserProfilePkToUniqueName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_UniqueUserName",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "MemoryName",
                table: "Memories",
                newName: "MemoryUniqueName");

            migrationBuilder.AddColumn<int>(
                name: "UserMemoryId",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemoryFriendlyName",
                table: "Memories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles",
                column: "UniqueUserName");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Memories_UserMemoryId",
                table: "UserProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_UserMemoryId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "UserMemoryId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "MemoryFriendlyName",
                table: "Memories");

            migrationBuilder.RenameColumn(
                name: "MemoryUniqueName",
                table: "Memories",
                newName: "MemoryName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UniqueUserName",
                table: "UserProfiles",
                column: "UniqueUserName",
                unique: true);
        }
    }
}
