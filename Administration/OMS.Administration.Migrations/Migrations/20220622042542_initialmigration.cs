using Microsoft.EntityFrameworkCore.Migrations;

namespace OMS.Administration.Migrations.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TaxIdenfier = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OfficeAddress_Street = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OfficeAddress_City = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    OfficeAddress_State = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OfficeAddress_Country = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    OfficeAddress_ZipCode = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    IsOwner = table.Column<bool>(type: "boolean", nullable: false),
                    OrganizationId = table.Column<string>(type: "character varying(50)", nullable: true),
                    ContactAddress_Street = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ContactAddress_City = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ContactAddress_State = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ContactAddress_Country = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ContactAddress_ZipCode = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_OrganizationId",
                table: "Contacts",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
