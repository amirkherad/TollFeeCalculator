using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TollFeeCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Provinces",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalSchema: "dbo",
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SingleChargeRules",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: true),
                    PeriodOfTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleChargeRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleChargeRules_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "dbo",
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SingleChargeRules_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalSchema: "dbo",
                        principalTable: "VehicleTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimeAmounts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: true),
                    From = table.Column<TimeSpan>(type: "time", nullable: false),
                    To = table.Column<TimeSpan>(type: "time", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeAmounts_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "dbo",
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeAmounts_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalSchema: "dbo",
                        principalTable: "VehicleTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CreatedOn",
                schema: "dbo",
                table: "Cities",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                schema: "dbo",
                table: "Cities",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId",
                schema: "dbo",
                table: "Cities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId_Name",
                schema: "dbo",
                table: "Cities",
                columns: new[] { "ProvinceId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CreatedOn",
                schema: "dbo",
                table: "Provinces",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_Name",
                schema: "dbo",
                table: "Provinces",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChargeRules_CityId",
                schema: "dbo",
                table: "SingleChargeRules",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChargeRules_CreatedOn",
                schema: "dbo",
                table: "SingleChargeRules",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChargeRules_VehicleTypeId",
                schema: "dbo",
                table: "SingleChargeRules",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeAmounts_CityId",
                schema: "dbo",
                table: "TimeAmounts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeAmounts_CreatedOn",
                schema: "dbo",
                table: "TimeAmounts",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_TimeAmounts_From",
                schema: "dbo",
                table: "TimeAmounts",
                column: "From");

            migrationBuilder.CreateIndex(
                name: "IX_TimeAmounts_To",
                schema: "dbo",
                table: "TimeAmounts",
                column: "To");

            migrationBuilder.CreateIndex(
                name: "IX_TimeAmounts_VehicleTypeId",
                schema: "dbo",
                table: "TimeAmounts",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypes_CreatedOn",
                schema: "dbo",
                table: "VehicleTypes",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypes_Name",
                schema: "dbo",
                table: "VehicleTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SingleChargeRules",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TimeAmounts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "VehicleTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Provinces",
                schema: "dbo");
        }
    }
}
