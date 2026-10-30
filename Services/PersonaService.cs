
using Epik.Api.Data;
using Epik.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Epik.Api.Services;

public class PersonaService : IPersonaService
{
    private readonly ApplicationDbContext _context;

    public PersonaService(ApplicationDbContext context)
    {
        _context = context;
    }

    // INSERTAR PERSONA
    public async Task<Persona> InsertarAsync(Persona persona)
    {
        var existe = await _context.Personas
            .AnyAsync(p => p.Identificacion == persona.Identificacion);

        if (existe)
        {
            throw new InvalidOperationException(
                "Ya existe una persona con esa identificación."
            );
        }

        _context.Personas.Add(persona);

        await _context.SaveChangesAsync();

        return persona;
    }

    // BUSCAR POR IDENTIFICACIÓN
    public async Task<Persona?> BuscarPorIdentificacionAsync(
        string identificacion)
    {
        return await _context.Personas
            .AsNoTracking()
            .FirstOrDefaultAsync(
                p => p.Identificacion == identificacion
            );
    }

    // LISTAR MUJERES UTILIZANDO LA VISTA SQL
    public async Task<List<MujerView>> ListarMujeresAsync()
    {
        return await _context.Mujeres
            .AsNoTracking()
            .ToListAsync();
    }

    // ACTUALIZAR ÚNICAMENTE LA EDAD
    public async Task<bool> ActualizarEdadAsync(
        string identificacion,
        int edad)
    {
        var persona = await _context.Personas
            .FirstOrDefaultAsync(
                p => p.Identificacion == identificacion
            );

        if (persona == null)
        {
            return false;
        }

        var propiedadEdad = _context.Entry(persona)
            .Property(p => p.Edad);

        propiedadEdad.CurrentValue = edad;
        propiedadEdad.IsModified = true;

        await _context.SaveChangesAsync();

        return true;
    }

    // ELIMINAR POR IDENTIFICACIÓN
    public async Task<bool> EliminarAsync(string identificacion)
    {
        var persona = await _context.Personas
            .FirstOrDefaultAsync(
                p => p.Identificacion == identificacion
            );

        if (persona == null)
        {
            return false;
        }

        _context.Personas.Remove(persona);

        await _context.SaveChangesAsync();

        return true;
    }
}