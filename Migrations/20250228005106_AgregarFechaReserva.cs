using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P01_2023EQ651_2022CI650.Migrations
{
    /// <inheritdoc />
    public partial class AgregarFechaReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activa",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Hora",
                table: "Reservas");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Reservas",
                newName: "FechaReserva");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaReserva",
                table: "Reservas",
                newName: "Fecha");

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "Reservas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Hora",
                table: "Reservas",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
