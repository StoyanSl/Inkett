using Microsoft.EntityFrameworkCore.Migrations;

namespace Inkett.Infrastructure.Migrations.Inkett
{
    public partial class PropertiesNamesChange_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TattooPictureUri",
                table: "Tattoos",
                newName: "PictureUri");

            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "Profiles",
                newName: "ProfilePictureUri");

            migrationBuilder.RenameColumn(
                name: "ProfileName",
                table: "Profiles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ProfileDescription",
                table: "Profiles",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CoverPicture",
                table: "Profiles",
                newName: "CoverPictureUri");

            migrationBuilder.RenameColumn(
                name: "AlbumPictureUri",
                table: "Albums",
                newName: "PictureUri");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureUri",
                table: "Tattoos",
                newName: "TattooPictureUri");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureUri",
                table: "Profiles",
                newName: "ProfilePicture");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Profiles",
                newName: "ProfileName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Profiles",
                newName: "ProfileDescription");

            migrationBuilder.RenameColumn(
                name: "CoverPictureUri",
                table: "Profiles",
                newName: "CoverPicture");

            migrationBuilder.RenameColumn(
                name: "PictureUri",
                table: "Albums",
                newName: "AlbumPictureUri");
        }
    }
}
