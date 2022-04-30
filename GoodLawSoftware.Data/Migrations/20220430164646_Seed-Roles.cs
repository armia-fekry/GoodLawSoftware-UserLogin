﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodLawSoftware.Infrastructure.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                            table: "AspNetRoles",
                            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                            values: new object[] { Guid.NewGuid().ToString(), "Admin", "admin".ToUpper(), Guid.NewGuid().ToString() }
                        );

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "User", "User".ToUpper(), Guid.NewGuid().ToString() }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AspNetRoles]");
        }
    }
}
