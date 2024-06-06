using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManagementApi.Database.Migrations
{
    /// <inheritdoc />
    public partial class UserDBUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_PlacesOfResidence_PlaceOfResidenceId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_PlaceOfResidenceId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PlaceOfResidenceId",
                table: "People");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "PlacesOfResidence",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PlacesOfResidence_PersonId",
                table: "PlacesOfResidence",
                column: "PersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlacesOfResidence_People_PersonId",
                table: "PlacesOfResidence",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlacesOfResidence_People_PersonId",
                table: "PlacesOfResidence");

            migrationBuilder.DropIndex(
                name: "IX_PlacesOfResidence_PersonId",
                table: "PlacesOfResidence");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "PlacesOfResidence");

            migrationBuilder.AddColumn<Guid>(
                name: "PlaceOfResidenceId",
                table: "People",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_People_PlaceOfResidenceId",
                table: "People",
                column: "PlaceOfResidenceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_People_PlacesOfResidence_PlaceOfResidenceId",
                table: "People",
                column: "PlaceOfResidenceId",
                principalTable: "PlacesOfResidence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
