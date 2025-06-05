using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAnimalClas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnimalType",
                table: "Animals",
                newName: "Species");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ArrivedAtZoo",
                table: "Animals",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Classification",
                table: "Animals",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DOB",
                table: "Animals",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "Animals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivedAtZoo",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Classification",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Animals");

            migrationBuilder.RenameColumn(
                name: "Species",
                table: "Animals",
                newName: "AnimalType");
        }
    }
}
