using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesAndPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert Roles
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
            { 1, "Admin" },
            { 2, "User" }
                });

            // Insert Permissions
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
            { 1, "ViewDashboard" },
            { 2, "EditUser" },
            { 3, "DeleteUser" }
                });

            // Insert RolePermissions (Admin → all)
            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" },
                values: new object[,]
                {
            { 1, 1 },
            { 1, 2 },
            { 1, 3 },

            { 2, 1 } // User → only ViewDashboard
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete RolePermissions
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleId", "PermissionId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleId", "PermissionId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleId", "PermissionId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleId", "PermissionId" },
                keyValues: new object[] { 2, 1 });

            // Delete Permissions
            migrationBuilder.DeleteData("Permissions", "Id", 1);
            migrationBuilder.DeleteData("Permissions", "Id", 2);
            migrationBuilder.DeleteData("Permissions", "Id", 3);

            // Delete Roles
            migrationBuilder.DeleteData("Roles", "Id", 1);
            migrationBuilder.DeleteData("Roles", "Id", 2);

        }
    }
}
