using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignTrack.Migrations
{
    public partial class tableSignIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StuNo",
                table: "Students",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SignIns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Sign = table.Column<DateTime>(nullable: false),
                    StudentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignIns_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SignIns_StudentId",
                table: "SignIns",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignIns");

            migrationBuilder.DropColumn(
                name: "StuNo",
                table: "Students");
        }
    }
}
