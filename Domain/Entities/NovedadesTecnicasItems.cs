using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("NovedadesTecnicasItems")]
public class NovedadesTecnicasItems
{
    [Key]
    [Column(Order = 0)]
    public int NovedadTecnicaID { get; set; }
    [Key]
    [Column(Order = 1)]
    public int PlantillaID { get; set; }
    [Key]
    [Column(Order = 2)]
    public int ItemID { get; set; }
    [MaxLength(4000)]
    public string? Descripcion { get; set; }
}
