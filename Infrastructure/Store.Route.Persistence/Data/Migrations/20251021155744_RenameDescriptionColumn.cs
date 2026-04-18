using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Route.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameDescriptionColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Describtion",
                table: "Products",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "Describtion");
        }
    }
}
