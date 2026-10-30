using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Epik.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMujeresView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
        CREATE OR REPLACE VIEW public.vw_mujeres AS
        SELECT
            "Identificacion",
            "Nombres",
            "Apellidos",
            "Edad",
            "Genero"
        FROM public."Persona"
        WHERE "Genero" = 'Femenino';
        """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
        DROP VIEW IF EXISTS public.vw_mujeres;
        """);
        }
    }
}