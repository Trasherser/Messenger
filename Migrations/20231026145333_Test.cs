using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messenger.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id_content",
                table: "chats");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDt",
                table: "contents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id_chats",
                table: "contents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Id_user",
                table: "contents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contents_Id_user",
                table: "contents",
                column: "Id_user");

            migrationBuilder.CreateIndex(
                name: "IX_chats_Id_user",
                table: "chats",
                column: "Id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_chats_users_Id_user",
                table: "chats",
                column: "Id_user",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_contents_users_Id_user",
                table: "contents",
                column: "Id_user",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chats_users_Id_user",
                table: "chats");

            migrationBuilder.DropForeignKey(
                name: "FK_contents_users_Id_user",
                table: "contents");

            migrationBuilder.DropIndex(
                name: "IX_users_Email",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_contents_Id_user",
                table: "contents");

            migrationBuilder.DropIndex(
                name: "IX_chats_Id_user",
                table: "chats");

            migrationBuilder.DropColumn(
                name: "CreateDt",
                table: "contents");

            migrationBuilder.DropColumn(
                name: "Id_chats",
                table: "contents");

            migrationBuilder.DropColumn(
                name: "Id_user",
                table: "contents");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id_content",
                table: "chats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
