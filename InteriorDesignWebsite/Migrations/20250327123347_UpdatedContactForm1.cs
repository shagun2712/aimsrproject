using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteriorDesignWebsite.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedContactForm1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelectedSpace",
                table: "ContactForms",
                newName: "SpaceToDesign");

            migrationBuilder.RenameColumn(
                name: "PreferredDesignStyle",
                table: "ContactForms",
                newName: "HeardAboutUs");

            migrationBuilder.RenameColumn(
                name: "HowDidYouHear",
                table: "ContactForms",
                newName: "DesignStyle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpaceToDesign",
                table: "ContactForms",
                newName: "SelectedSpace");

            migrationBuilder.RenameColumn(
                name: "HeardAboutUs",
                table: "ContactForms",
                newName: "PreferredDesignStyle");

            migrationBuilder.RenameColumn(
                name: "DesignStyle",
                table: "ContactForms",
                newName: "HowDidYouHear");
        }
    }
}
