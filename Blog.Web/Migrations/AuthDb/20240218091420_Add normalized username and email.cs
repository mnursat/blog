using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Addnormalizedusernameandemail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9023e09-d8c7-428b-9387-77f36492d748",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33707a8f-d982-486d-b49d-b6cce667ec5a", "SUPERADMIN@BLOG.COM", "SUPERADMIN@BLOG.COM", "AQAAAAIAAYagAAAAEF7p0IMm0kBN4aupd+gO44EqzP13Dud1WEcIeUYlpCGEtqKhj1ioj7iszS4HmxQXvg==", "6c123a96-0131-4ea4-bb0e-a39d7f24fd97" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9023e09-d8c7-428b-9387-77f36492d748",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1c21ce04-48d0-472d-9f5a-bec894644505", null, null, "AQAAAAIAAYagAAAAEGFu6EGPXcPO0m1iIim3ApsZV+kiG8YOJ5Ekd+M5OdiRtBa7zcxXiDsLsMXAM3u0XQ==", "800af9cb-1011-4f61-bf58-bb8c97f0a93b" });
        }
    }
}
