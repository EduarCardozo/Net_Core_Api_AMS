using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("LicenciasMoviles")]
public class LicenciasMoviles
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LicenciaMovilID { get; set; }
    [Required]
    public int LicenciaID { get; set; }
    [Required]
    [MaxLength(50)]
    public string IdentificadorMovil { get; set; } = null!;
    [MaxLength(50)]
    public string? IPRestringida { get; set; }
    [Required]
    public byte Estado { get; set; }
    [MaxLength(50)]
    public string? Keys { get; set; }
    public int? RegistracionUsuarioID { get; set; }
}
