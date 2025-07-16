using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActiveLifeElderServices.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumbersToAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppointmentDateTime",
                table: "Appointments",
                newName: "AppointmentTime");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalRequirements",
                table: "Appointments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AlternatePhoneNumber",
                table: "Appointments",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CaregiverGenderPreference",
                table: "Appointments",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactAddress",
                table: "Appointments",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactPhoneNumber",
                table: "Appointments",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalRequirements",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AlternatePhoneNumber",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CaregiverGenderPreference",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ContactAddress",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ContactPhoneNumber",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "AppointmentTime",
                table: "Appointments",
                newName: "AppointmentDateTime");
        }
    }
}
