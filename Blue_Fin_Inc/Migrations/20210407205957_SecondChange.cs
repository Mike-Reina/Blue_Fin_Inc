using Microsoft.EntityFrameworkCore.Migrations;

namespace Blue_Fin_Inc.Migrations
{
    public partial class SecondChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Livestocks",
                type: "nvarchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Livestocks");
        }
    }
}
