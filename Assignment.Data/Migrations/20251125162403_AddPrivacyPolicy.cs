using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPrivacyPolicy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrivacyPolicyAccepted",
                table: "Customers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PrivacyPolicyAcceptedAt",
                table: "Customers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrivacyPolicyAccepted",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PrivacyPolicyAcceptedAt",
                table: "Customers");
        }
    }
}
