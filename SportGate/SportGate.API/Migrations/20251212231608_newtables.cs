using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportGate.API.Migrations
{
    /// <inheritdoc />
    public partial class newtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "EntryTypePrices",
                newName: "BaseFee");

            migrationBuilder.AddColumn<bool>(
                name: "AllowMultiplePeople",
                table: "EntryTypePrices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "EntryTypePrices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresBaseFee",
                table: "EntryTypePrices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PersonCategoryPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCategoryPrices", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonCategoryPrices");

            migrationBuilder.DropColumn(
                name: "AllowMultiplePeople",
                table: "EntryTypePrices");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EntryTypePrices");

            migrationBuilder.DropColumn(
                name: "RequiresBaseFee",
                table: "EntryTypePrices");

            migrationBuilder.RenameColumn(
                name: "BaseFee",
                table: "EntryTypePrices",
                newName: "Price");
        }
    }
}
