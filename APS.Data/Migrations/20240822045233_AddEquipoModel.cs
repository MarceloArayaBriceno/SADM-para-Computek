using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEquipoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    description = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    isAuthorized = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user__B9BE370F4435B8CB", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    account_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    balance = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__account__46A222CDB9FFDFD3", x => x.account_id);
                    table.ForeignKey(
                        name: "FK_account_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "authorizations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    pages = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__authoriz__3213E83F41813066", x => x.id);
                    table.ForeignKey(
                        name: "FK_authorizations_userId",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "equipos",
                columns: table => new
                {
                    equipo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    marca = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    modelo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    nombre_cliente = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    motivo_ingreso = table.Column<string>(type: "text", nullable: false),
                    garantia_con_local = table.Column<bool>(type: "bit", nullable: false),
                    contraseña_equipo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    fecha_ingreso = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    usuario_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__equipos__50EAD2BF9783599C", x => x.equipo_id);
                    table.ForeignKey(
                        name: "FK__equipos__usuario__6A30C649",
                        column: x => x.usuario_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    message = table.Column<string>(type: "text", nullable: true),
                    is_read = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__notifica__E059842FAF406580", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK_notifications_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "aprobaciones",
                columns: table => new
                {
                    aprobacion_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    equipo_id = table.Column<int>(type: "int", nullable: false),
                    criterio = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    cumple = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__aprobaci__841444415F806074", x => x.aprobacion_id);
                    table.ForeignKey(
                        name: "FK__aprobacio__equip__6D0D32F4",
                        column: x => x.equipo_id,
                        principalTable: "equipos",
                        principalColumn: "equipo_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_user_id",
                table: "account",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_aprobaciones_equipo_id",
                table: "aprobaciones",
                column: "equipo_id");

            migrationBuilder.CreateIndex(
                name: "IX_authorizations_userId",
                table: "authorizations",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_equipos_usuario_id",
                table: "equipos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_user_id",
                table: "notifications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_user_username",
                table: "user",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "aprobaciones");

            migrationBuilder.DropTable(
                name: "authorizations");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "equipos");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
