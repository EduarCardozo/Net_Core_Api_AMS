using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("G_Paises")]
public class G_Paises
{
    [Key]
    public short PaisID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [Required]
    [MaxLength(2)]
    public string A2 { get; set; } = null!;
    [Required]
    [MaxLength(3)]
    public string A3 { get; set; } = null!;
    [MaxLength(3)]
    public string? Numero { get; set; }
    [MaxLength(30)]
    public string? RegionDenominacionLocal { get; set; }
    [MaxLength(50)]
    public string? Nombre_EN { get; set; }
    [MaxLength(50)]
    public string? Nombre_PT { get; set; }
    [MaxLength(50)]
    public string? Nombre_FR { get; set; }
    public short? CCCode { get; set; }
    public short? GMT { get; set; }
}
