using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantApp.Data.Migrations
{
    public partial class Plants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Plants",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastWateredOn",
                table: "Plants",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Plants",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WateringPeriod",
                table: "Plants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Plants_UserId",
                table: "Plants",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_AspNetUsers_UserId",
                table: "Plants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_AspNetUsers_UserId",
                table: "Plants");

            migrationBuilder.DropIndex(
                name: "IX_Plants_UserId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "LastWateredOn",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "WateringPeriod",
                table: "Plants");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Plants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);
        }
    }
}
