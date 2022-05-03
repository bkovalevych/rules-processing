using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RulesExercise.Infrastructure.Migrations
{
    public partial class SeedWithTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "Message", "Subject", "Type" },
                values: new object[] { 1, "\r\n<div>\r\n	<h4>Hi there from {{ Name }} project</h4>\r\n	<ul>\r\n		<li>Id : {{ Id }};</li>\r\n        <li>Project Changed;</li>\r\n	</ul> \r\n</div>", "Changes from {{Name}} project", "Smtp" });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "Message", "Subject", "Type" },
                values: new object[] { 2, "\r\n<div>\r\n	<h4>Project {{ Name }} was Deleted</h4>\r\n	<ul>\r\n		<li>Id : {{ Id }};</li>\r\n        <li>Project Deleted;</li>\r\n	</ul> \r\n</div>", "{{ Name }} project was deleted", "Smtp" });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "Message", "Subject", "Type" },
                values: new object[] { 3, "Description: {{ Description }}", "{{ Name }} project with id  {{ Id }} was changed", "Telegram" });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "Message", "Subject", "Type" },
                values: new object[] { 4, "Description: {{ Description }}\n Id: {{ Id }}", "{{ Name }} project was deleted", "Telegram" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
