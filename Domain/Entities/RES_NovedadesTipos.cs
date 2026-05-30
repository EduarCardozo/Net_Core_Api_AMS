using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("RES_NovedadesTipos")]
public class RES_NovedadesTipos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NovedadTipoID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [Required]
    [MaxLength(10)]
    public string Codigo { get; set; } = null!;
    [Required]
    public byte InhabilitaTrabajo { get; set; }
    [Required]
    public byte ExigeReemplazo { get; set; }
    [Required]
    public byte Estado { get; set; }
    public byte? RequierePlanMantenimiento { get; set; }
}
