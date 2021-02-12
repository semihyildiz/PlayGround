using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BigDataTechnology.DATA.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherForecast",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CurrentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentTemprature = table.Column<double>(type: "float", nullable: false),
                    HighestDateTimeInThisWeek = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HighestTempratureInThisWeek = table.Column<double>(type: "float", nullable: false),
                    LowestTempratureInThisWeek = table.Column<double>(type: "float", nullable: false),
                    LowestDateTimeInThisWeek = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecast", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecast_Location",
                table: "WeatherForecast",
                column: "Location");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherForecast");
        }
    }
}
