using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PigmaAPI.Migrations
{
    /// <inheritdoc />
    public partial class initMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyAgencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 11, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adress = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    AdditionalAddress = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    PostalCode = table.Column<int>(type: "int", maxLength: 11, nullable: true),
                    VilleID = table.Column<int>(type: "int", maxLength: 11, nullable: true),
                    MeansOfPayment = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Iban = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Bic = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAgencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 11, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyAgencyId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyContact",
                        column: x => x.CompanyAgencyId,
                        principalTable: "CompanyAgencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContacts_CompanyAgencyId",
                table: "CompanyContacts",
                column: "CompanyAgencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyContacts");

            migrationBuilder.DropTable(
                name: "CompanyAgencies");
        }
    }
}
