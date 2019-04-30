using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_HospitalDatabase.Migrations
{
    public partial class DoctorAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Visitations",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Visitations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Patients",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Diagnoses",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    DoctorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Specialty = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.DoctorId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visitations_DoctorId",
                table: "Visitations",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitations_Doctor_DoctorId",
                table: "Visitations",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitations_Doctor_DoctorId",
                table: "Visitations");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Visitations_DoctorId",
                table: "Visitations");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Visitations");

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Visitations",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Patients",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Diagnoses",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250);
        }
    }
}
