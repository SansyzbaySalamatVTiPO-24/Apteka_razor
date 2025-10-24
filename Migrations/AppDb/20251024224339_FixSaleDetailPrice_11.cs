using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apteka_razor.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class FixSaleDetailPrice_11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Drugs_DrugId",
                table: "SaleDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Sales_SaleId",
                table: "SaleDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleDetails",
                table: "SaleDetail");

            migrationBuilder.RenameTable(
                name: "SaleDetail",
                newName: "SaleDetail");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetails_SaleId",
                table: "SaleDetail",
                newName: "IX_SaleDetail_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetails_DrugId",
                table: "SaleDetail",
                newName: "IX_SaleDetail_DrugId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleDetail",
                table: "SaleDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_Drugs_DrugId",
                table: "SaleDetail",
                column: "DrugId",
                principalTable: "Drugs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_Sales_SaleId",
                table: "SaleDetail",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_Drugs_DrugId",
                table: "SaleDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_Sales_SaleId",
                table: "SaleDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleDetail",
                table: "SaleDetail");

            migrationBuilder.RenameTable(
                name: "SaleDetail",
                newName: "SaleDetail");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetail_SaleId",
                table: "SaleDetail",
                newName: "IX_SaleDetails_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetail_DrugId",
                table: "SaleDetail",
                newName: "IX_SaleDetails_DrugId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleDetails",
                table: "SaleDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Drugs_DrugId",
                table: "SaleDetail",
                column: "DrugId",
                principalTable: "Drugs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Sales_SaleId",
                table: "SaleDetail",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
