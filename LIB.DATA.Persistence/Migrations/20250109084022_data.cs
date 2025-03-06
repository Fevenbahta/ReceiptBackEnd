using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LIB.API.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reciepts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecieptNo = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CustomerName = table.Column<string>(type: "text", nullable: false),
                    Account = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    ServiceFee = table.Column<decimal>(type: "numeric", nullable: false),
                    Vat = table.Column<decimal>(type: "numeric", nullable: false),
                    Branch = table.Column<string>(type: "text", nullable: false),
                    ServiceType = table.Column<string>(type: "text", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    TinNo = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    PhoneNo = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reciepts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UpdateLog",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Operation = table.Column<string>(type: "text", nullable: false),
                    TableName = table.Column<string>(type: "text", nullable: false),
                    RecordId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedFields = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateLog", x => x.LogId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Branch = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    BranchCode = table.Column<string>(type: "text", nullable: false),
                    UpdatedDate = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reciepts");

            migrationBuilder.DropTable(
                name: "UpdateLog");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
