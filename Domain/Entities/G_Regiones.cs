using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("G_Regiones")]
public class G_Regiones
{
    [Key]
    public int RegionID { get; set; }
    [MaxLength(3)]
    public string? Codigo { get; set; }
    [MaxLength(50)]
    public string? Nombre { get; set; }
    [Required]
    public short PaisID { get; set; }
    [MaxLength(50)]
    public string? SubRegionDenominacionLocal { get; set; }
    public short? GMT { get; set; }
}
