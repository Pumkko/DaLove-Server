using Microsoft.EntityFrameworkCore.Migrations;

namespace DaLove_Server.Data.Migrations
{
    public partial class AddAvatarFileNameToUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarFileName",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarFileName",
                table: "UserProfiles");
        }
    }
}
