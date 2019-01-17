using Microsoft.EntityFrameworkCore.Migrations;

namespace Inkett.Infrastructure.Migrations.Inkett
{
    public partial class ProfileAlbumRealtion_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Profiles_ProfileId",
                table: "Albums");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Profiles_ProfileId",
                table: "Albums",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Profiles_ProfileId",
                table: "Albums");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Profiles_ProfileId",
                table: "Albums",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
