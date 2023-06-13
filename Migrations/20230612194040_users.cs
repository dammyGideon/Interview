using Microsoft.EntityFrameworkCore.Migrations;

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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NoteEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                values: new object[] { 1, "ajayegidolas@gmail.com", "$2a$11$U/gKqwE9MgnbwK6v5Vr4keiQmFSoga1mB88uj97lYDh1og2Vk.pZG" });

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
