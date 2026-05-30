using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TAX_Impuestos")]
public class TAX_Impuestos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short ImpuestoID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [Required]
    [MaxLength(5)]
    public string Codigo { get; set; } = null!;
    [Required]
    public byte GeoNivelAplicacion { get; set; }
    [Required]
    public short PaisID { get; set; }
    [Required]
    public int AdministradorTributarioID { get; set; }
    [Required]
    public int GeoTargetID { get; set; }
}
