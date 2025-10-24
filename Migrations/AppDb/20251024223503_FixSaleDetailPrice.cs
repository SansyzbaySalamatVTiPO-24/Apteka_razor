using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apteka_razor.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class FixSaleDetailPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "SaleDetail");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "SaleDetail",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
