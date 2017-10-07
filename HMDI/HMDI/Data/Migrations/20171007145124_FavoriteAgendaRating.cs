using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HMDI.Migrations
{
    public partial class FavoriteAgendaRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "FavoriteAgenda",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasRated",
                table: "FavoriteAgenda",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "FavoriteAgenda");

            migrationBuilder.DropColumn(
                name: "HasRated",
                table: "FavoriteAgenda");
        }
    }
}
