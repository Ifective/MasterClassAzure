using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Data.Migrations
{
    public partial class studentemail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_Cources_CourseId",
                table: "Enrolments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cources",
                table: "Cources");

            migrationBuilder.RenameTable(
                name: "Cources",
                newName: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_Courses_CourseId",
                table: "Enrolments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_Courses_CourseId",
                table: "Enrolments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Cources");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cources",
                table: "Cources",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_Cources_CourseId",
                table: "Enrolments",
                column: "CourseId",
                principalTable: "Cources",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
