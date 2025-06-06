using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZooManagement.Migrations
{
    /// <inheritdoc />
    public partial class DBContextSeeding4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "AnimalID", "ArrivedAtZoo", "Classification", "DOB", "Name", "Sex", "Species" },
                values: new object[,]
                {
                    { 9, "20/02/2020", "Mammal", "09/04/2011", "Sarah", "Female", "Elephant" },
                    { 10, "02/10/2021", "Mammal", "05/04/2019", "Sophie", "Female", "Lion" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalID",
                keyValue: 10);
        }
    }
}
