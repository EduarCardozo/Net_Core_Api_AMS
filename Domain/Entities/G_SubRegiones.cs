using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("G_SubRegiones")]
public class G_SubRegiones
{
    [Key]
    public int SubRegionID { get; set; }
    [Required]
    public int RegionID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [MaxLength(4)]
    public string? Codigo { get; set; }
}
