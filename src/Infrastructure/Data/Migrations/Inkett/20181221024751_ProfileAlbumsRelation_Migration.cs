using Microsoft.EntityFrameworkCore.Migrations;

namespace Inkett.Infrastructure.Migrations.Inkett
{
    public partial class ProfileAlbumsRelation_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Albums_ProfileId",
                table: "Albums",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Profiles_ProfileId",
                table: "Albums",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Profiles_ProfileId",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_ProfileId",
                table: "Albums");
        }
    }
}
