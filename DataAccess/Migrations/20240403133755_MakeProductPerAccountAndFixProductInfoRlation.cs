using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MakeProductPerAccountAndFixProductInfoRlation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Products",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AccountId",
                table: "Products",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_AccountId",
                table: "Products",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_AccountId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_AccountId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Products");
        }
    }
}
