using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("PlanesMantenimiento")]
public class PlanesMantenimiento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PlanMantenimientoID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Codigo { get; set; } = null!;
    [Required]
    public byte TargetType { get; set; }
    public int? TargetID { get; set; }
    [Required]
    public DateTime CreacionFecha { get; set; }
    [Required]
    public int CreacionUsuarioID { get; set; }
    [Required]
    public byte Estado { get; set; }
}
