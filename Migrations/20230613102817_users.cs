using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace interview.Migrations
{
    /// <inheritdoc />
    public partial class users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NoteEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Note = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteEntities_UsersEntities_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UsersEntities",
                columns: new[] { "Id", "Email", "Password" },
                values: new object[] { 1, "ajayegidolas@gmail.com", "$2a$11$JjTg/VGfHAdE6yx3Y/mnaewFexAhQqs9QIG277usBpl4jgx0LEdoe" });

            migrationBuilder.CreateIndex(
                name: "IX_NoteEntities_UserId",
                table: "NoteEntities",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteEntities");

            migrationBuilder.DropTable(
                name: "UsersEntities");
        }
    }
}
