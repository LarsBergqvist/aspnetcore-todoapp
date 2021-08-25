using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class migr1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table => new
                {
                    TodoListEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => x.TodoListEntityId);
                });

            migrationBuilder.CreateTable(
                name: "TodoItemEntity",
                columns: table => new
                {
                    TodoItemEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TodoListEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItemEntity", x => x.TodoItemEntityId);
                    table.ForeignKey(
                        name: "FK_TodoItemEntity_TodoLists_TodoListEntityId",
                        column: x => x.TodoListEntityId,
                        principalTable: "TodoLists",
                        principalColumn: "TodoListEntityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItemEntity_TodoListEntityId",
                table: "TodoItemEntity",
                column: "TodoListEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItemEntity");

            migrationBuilder.DropTable(
                name: "TodoLists");
        }
    }
}
