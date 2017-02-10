using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreProjectsList.Migrations
{
    public partial class NullablecolumnsinTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PercentDone",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HoursSpent",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HoursPredicted",
                table: "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PercentDone",
                table: "Tasks",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "HoursSpent",
                table: "Tasks",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "HoursPredicted",
                table: "Tasks",
                nullable: false);
        }
    }
}
