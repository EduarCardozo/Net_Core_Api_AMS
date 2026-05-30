using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("PlantillasItems")]
public class PlantillasItems
{
    [Key]
    [Column(Order = 0)]
    public int PlantillaID { get; set; }
    [Key]
    [Column(Order = 1)]
    public int ItemID { get; set; }
}
