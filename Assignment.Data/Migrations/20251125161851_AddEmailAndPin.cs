using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailAndPin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtpCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "OtpExpiry",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "IsVerified",
                table: "Customers",
                newName: "IsPhoneVerified");

            migrationBuilder.AddColumn<bool>(
                name: "BiometricEnabled",
                table: "Customers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EmailOtp",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailOtpExpiry",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailVerified",
                table: "Customers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneOtp",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PhoneOtpExpiry",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Pin",
                table: "Customers",
                type: "TEXT",
                maxLength: 6,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BiometricEnabled",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EmailOtp",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EmailOtpExpiry",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsEmailVerified",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PhoneOtp",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PhoneOtpExpiry",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Pin",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "IsPhoneVerified",
                table: "Customers",
                newName: "IsVerified");

            migrationBuilder.AddColumn<string>(
                name: "OtpCode",
                table: "Customers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpExpiry",
                table: "Customers",
                type: "TEXT",
                nullable: true);
        }
    }
}
