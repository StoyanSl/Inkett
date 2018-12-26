using Microsoft.EntityFrameworkCore.Migrations;

namespace Inkett.Infrastructure.Migrations.Inkett
{
    public partial class TattooRelationsFixed_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tattoos_Albums_AlbumId",
                table: "Tattoos");

            migrationBuilder.DropForeignKey(
                name: "FK_Tattoos_Profiles_ProfileId",
                table: "Tattoos");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "Tattoos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AlbumId",
                table: "Tattoos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Tattoos_Albums_AlbumId",
                table: "Tattoos",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tattoos_Profiles_ProfileId",
                table: "Tattoos",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tattoos_Albums_AlbumId",
                table: "Tattoos");

            migrationBuilder.DropForeignKey(
                name: "FK_Tattoos_Profiles_ProfileId",
                table: "Tattoos");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "Tattoos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AlbumId",
                table: "Tattoos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tattoos_Albums_AlbumId",
                table: "Tattoos",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tattoos_Profiles_ProfileId",
                table: "Tattoos",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
