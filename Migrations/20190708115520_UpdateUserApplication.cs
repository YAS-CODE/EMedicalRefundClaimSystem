using Microsoft.EntityFrameworkCore.Migrations;

namespace EMedicalClaimRefundSystem.Migrations
{
    public partial class UpdateUserApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ERefundUserApplication",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "ERefundUserApplication",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ERefundUserApplication",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "ERefundUserApplication");

            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "ERefundUserApplication");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ERefundUserApplication");
        }
    }
}
