
using Epik.Api.Models;

namespace Epik.Api.Services;

public interface IPersonaService
{
    // Insertar una persona
    Task<Persona> InsertarAsync(Persona persona);

    // Buscar persona por identificación
    Task<Persona?> BuscarPorIdentificacionAsync(string identificacion);

    // Listar mujeres utilizando la vista SQL
    Task<List<MujerView>> ListarMujeresAsync();

    // Actualizar únicamente la edad
    Task<bool> ActualizarEdadAsync(string identificacion, int edad);

    // Eliminar persona por identificación
    Task<bool> EliminarAsync(string identificacion);
}