using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiyiStack.Migrations
{
    /// <inheritdoc />
    public partial class InicializarBaseDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CMS.Menu",
                columns: table => new
                {
                    MenuId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeCreation = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateTimeLastModification = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserCreationId = table.Column<long>(type: "bigint", nullable: false),
                    UserLastModificationId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    MenuFatherId = table.Column<long>(type: "bigint", nullable: false),
                    URLPath = table.Column<string>(type: "varchar(2048)", nullable: true),
                    IconURLPath = table.Column<string>(type: "varchar(100)", nullable: true),
                    Order = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMS.Menu", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "CMS.Role",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeCreation = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateTimeLastModification = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserCreationId = table.Column<long>(type: "bigint", nullable: false),
                    UserLastModificationId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMS.Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "CMS.RoleMenu",
                columns: table => new
                {
                    RoleMenuId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeCreation = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateTimeLastModification = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserCreationId = table.Column<long>(type: "bigint", nullable: false),
                    UserLastModificationId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<byte>(type: "tinyint", nullable: false),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMS.RoleMenu", x => x.RoleMenuId);
                });

            migrationBuilder.CreateTable(
                name: "CMS.User",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeCreation = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateTimeLastModification = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserCreationId = table.Column<long>(type: "bigint", nullable: false),
                    UserLastModificationId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<byte>(type: "tinyint", nullable: false),
                    Email = table.Column<string>(type: "varchar(400)", nullable: false),
                    Password = table.Column<string>(type: "varchar(8000)", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMS.User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "System.Failure",
                columns: table => new
                {
                    FailureId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeCreation = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateTimeLastModification = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserCreationId = table.Column<long>(type: "bigint", nullable: false),
                    UserLastModificationId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<byte>(type: "tinyint", nullable: false),
                    Message = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    EmergencyLevel = table.Column<long>(type: "bigint", nullable: false),
                    StackTrace = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Source = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Comment = table.Column<string>(type: "varchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_System.Failure", x => x.FailureId);
                });

            migrationBuilder.CreateTable(
                name: "System.Parameter",
                columns: table => new
                {
                    ParameterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeCreation = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateTimeLastModification = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserCreationId = table.Column<long>(type: "bigint", nullable: false),
                    UserLastModificationId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Value = table.Column<string>(type: "varchar(8000)", nullable: false),
                    IsPrivate = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_System.Parameter", x => x.ParameterId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CMS.Menu");

            migrationBuilder.DropTable(
                name: "CMS.Role");

            migrationBuilder.DropTable(
                name: "CMS.RoleMenu");

            migrationBuilder.DropTable(
                name: "CMS.User");

            migrationBuilder.DropTable(
                name: "System.Failure");

            migrationBuilder.DropTable(
                name: "System.Parameter");
        }
    }
}
