using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apteka_razor.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class FixFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Pharmacies_PharmacyId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_PharmacyId",
                table: "Drugs",
                column: "PharmacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_Pharmacies_PharmacyId",
                table: "Drugs",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Pharmacies_PharmacyId",
                table: "Employees",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drugs_Pharmacies_PharmacyId",
                table: "Drugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Pharmacies_PharmacyId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Drugs_PharmacyId",
                table: "Drugs");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Pharmacies_PharmacyId",
                table: "Employees",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
