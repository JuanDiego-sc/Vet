using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "MedicalAppointments",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAppointments_IdUser",
                table: "MedicalAppointments",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalAppointments_AspNetUsers_IdUser",
                table: "MedicalAppointments",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalAppointments_AspNetUsers_IdUser",
                table: "MedicalAppointments");

            migrationBuilder.DropIndex(
                name: "IX_MedicalAppointments_IdUser",
                table: "MedicalAppointments");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "MedicalAppointments");
        }
    }
}
