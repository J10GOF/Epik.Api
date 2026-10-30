
using Epik.Api.Dtos;
using Epik.Api.Models;
using Epik.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Epik.Api.Controllers;

[ApiController]
[Route("api/personas")]
public class PersonaController : ControllerBase
{
    private readonly IPersonaService _personaService;

    public PersonaController(IPersonaService personaService)
    {
        _personaService = personaService;
    }

    // POST: api/personas
    // INSERTAR PERSONA
    [HttpPost]
    public async Task<ActionResult<Persona>> Crear(
        [FromBody] Persona persona)
    {
        try
        {
            var resultado = await _personaService.InsertarAsync(persona);

            return CreatedAtAction(
                nameof(BuscarPorIdentificacion),
                new { identificacion = resultado.Identificacion },
                resultado
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }

    // GET: api/personas/mujeres
    // LISTAR MUJERES UTILIZANDO LA VISTA
    [HttpGet("mujeres")]
    public async Task<ActionResult<List<MujerView>>> ListarMujeres()
    {
        var mujeres = await _personaService.ListarMujeresAsync();

        return Ok(mujeres);
    }

    // GET: api/personas/{identificacion}
    // BUSCAR POR IDENTIFICACIÓN
    [HttpGet("{identificacion}")]
    public async Task<ActionResult<Persona>> BuscarPorIdentificacion(
        string identificacion)
    {
        var persona = await _personaService
            .BuscarPorIdentificacionAsync(identificacion);

        if (persona == null)
        {
            return NotFound(new
            {
                mensaje = "No se encontró una persona con esa identificación."
            });
        }

        return Ok(persona);
    }

    // PUT: api/personas/{identificacion}/edad
    // ACTUALIZAR ÚNICAMENTE LA EDAD
    [HttpPut("{identificacion}/edad")]
    public async Task<IActionResult> ActualizarEdad(
        string identificacion,
        [FromBody] ActualizarEdadRequest request)
    {
        var actualizado = await _personaService.ActualizarEdadAsync(
            identificacion,
            request.Edad
        );

        if (!actualizado)
        {
            return NotFound(new
            {
                mensaje = "No se encontró una persona con esa identificación."
            });
        }

        return NoContent();
    }

    // DELETE: api/personas/{identificacion}
    // ELIMINAR PERSONA
    [HttpDelete("{identificacion}")]
    public async Task<IActionResult> Eliminar(string identificacion)
    {
        var eliminado = await _personaService.EliminarAsync(identificacion);

        if (!eliminado)
        {
            return NotFound(new
            {
                mensaje = "No se encontró una persona con esa identificación."
            });
        }

        return NoContent();
    }
}