using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    /// <inheritdoc />
    public partial class SeesAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
    table: "Users",
    columns: new[]
    {
        "Id", "Username", "Email", "PasswordHash", "RoleId", "IsActive", "CreatedAt"
    },
    values: new object[]
    {
        1,
        "admin",
        "admin@gmail.com",
        "$2a$11$4/5Cti4gVWoADh4Wjg0OMeMdSwsI1SVPclrHDjNN8rRdHA9IWhObS", // 🔥 paste hashed password
        1, // Admin role
        true,
        DateTime.UtcNow
    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
    table: "Users",
    keyColumn: "Id",
    keyValue: 1);
        }
    }
}
