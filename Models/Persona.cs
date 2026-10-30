
using System.ComponentModel.DataAnnotations;

namespace Epik.Api.Models;

public class Persona
{
    [Key]
    [Required]
    [StringLength(20)]
    public string Identificacion { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Nombres { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Apellidos { get; set; } = string.Empty;

    [Range(0, 120)]
    public int Edad { get; set; }

    [Required]
    [RegularExpression(
        "^(Masculino|Femenino)$",
        ErrorMessage = "El género debe ser Masculino o Femenino."
    )]
    public string Genero { get; set; } = string.Empty;
}