
using System.ComponentModel.DataAnnotations;

namespace Epik.Api.Dtos;

public class ActualizarEdadRequest
{
    [Range(0, 120, ErrorMessage = "La edad debe estar entre 0 y 120.")]
    public int Edad { get; set; }
}