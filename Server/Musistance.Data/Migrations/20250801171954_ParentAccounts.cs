using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Musistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class ParentAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Profiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_ParentId",
                table: "Profiles",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Profiles_ParentId",
                table: "Profiles",
                column: "ParentId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Profiles_ParentId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_ParentId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Profiles");
        }
    }
}
