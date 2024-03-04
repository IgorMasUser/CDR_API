using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CDR_API.Migrations
{
    /// <inheritdoc />
    public partial class CallRecord_Id_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CallRecords",
                table: "CallRecords");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CallRecords",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CallRecords",
                table: "CallRecords",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CallRecords",
                table: "CallRecords");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CallRecords");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CallRecords",
                table: "CallRecords",
                column: "CallerId");
        }
    }
}
