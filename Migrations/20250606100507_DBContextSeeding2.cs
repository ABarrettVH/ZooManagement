using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZooManagement.Migrations
{
    /// <inheritdoc />
    public partial class DBContextSeeding2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "AnimalID", "ArrivedAtZoo", "Classification", "DOB", "Name", "Sex", "Species" },
                values: new object[,]
                {
                    { 3, "24/06/2022", "Mammal", "12/05/2014", "Harry", "Male", "Panda" },
                    { 4, "14/08/2019", "Bird", "18/11/2017", "Kelly", "Female", "Parrot" },
                    { 5, "17/10/2022", "Amphibian", "23/10/2002", "Bob", "Male", "Frog" },
                    { 6, "19/12/2020", "Reptile", "01/01/2019", "Lisa", "Female", "Python" },
                    { 7, "21/07/2022", "Mammal", "30/08/2021", "Valerie", "Female", "Zebra" },
                    { 8, "06/11/2021", "Fish", "07/12/2019", "Michael", "Male", "Shark" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalID",
                keyValue: 8);
        }
    }
}
