using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagement.Migrations
{
    /// <inheritdoc />
    public partial class DBContextSeeding1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Animals",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "AnimalID",
                keyValue: 1,
                columns: new[] { "ArrivedAtZoo", "Classification", "Sex" },
                values: new object[] { "14/06/2022", "Mammal", "Male" });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "AnimalID",
                keyValue: 2,
                columns: new[] { "ArrivedAtZoo", "Classification", "Sex" },
                values: new object[] { "10/10/2010", "Mammals", "Female" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Sex",
                table: "Animals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "AnimalID",
                keyValue: 1,
                columns: new[] { "ArrivedAtZoo", "Classification", "Sex" },
                values: new object[] { null, null, 0 });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "AnimalID",
                keyValue: 2,
                columns: new[] { "ArrivedAtZoo", "Classification", "Sex" },
                values: new object[] { null, null, 0 });
        }
    }
}
