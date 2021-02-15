using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FarmAppV2.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sheep",
                columns: table => new
                {
                    SheepId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SheepName = table.Column<string>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: false),
                    MothersName = table.Column<string>(nullable: true),
                    FathersName = table.Column<string>(nullable: true),
                    Miscarriage = table.Column<bool>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sheep", x => x.SheepId);
                    table.ForeignKey(
                        name: "FK_Sheep_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vaccine",
                columns: table => new
                {
                    VaccineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    VaccineDate = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccine", x => x.VaccineId);
                    table.ForeignKey(
                        name: "FK_Vaccine_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sheep_OwnerId",
                table: "Sheep",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccine_OwnerId",
                table: "Vaccine",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sheep");

            migrationBuilder.DropTable(
                name: "Vaccine");
        }
    }
}
