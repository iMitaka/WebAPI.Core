using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JarvisEdge.Data.Migrations
{
    public partial class fixingg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartamentTypeId",
                table: "Properties",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ApartamentTypeId",
                table: "Properties",
                column: "ApartamentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_ApartamentTypes_ApartamentTypeId",
                table: "Properties",
                column: "ApartamentTypeId",
                principalTable: "ApartamentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_ApartamentTypes_ApartamentTypeId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_ApartamentTypeId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ApartamentTypeId",
                table: "Properties");
        }
    }
}
