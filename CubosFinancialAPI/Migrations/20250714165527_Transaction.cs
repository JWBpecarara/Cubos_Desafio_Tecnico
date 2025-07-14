using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CubosFinancialAPI.Migrations
{
    /// <inheritdoc />
    public partial class Transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternalTransfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginTransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DestinationTransactionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalTransfers_Transactions_DestinationTransactionId",
                        column: x => x.DestinationTransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternalTransfers_Transactions_OriginTransactionId",
                        column: x => x.OriginTransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalTransfers_DestinationTransactionId",
                table: "InternalTransfers",
                column: "DestinationTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalTransfers_OriginTransactionId",
                table: "InternalTransfers",
                column: "OriginTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalTransfers");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
