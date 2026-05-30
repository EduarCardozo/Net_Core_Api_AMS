using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("G_DireccionesOwners")]
public class G_DireccionesOwners
{
    [Key]
    [Column(Order = 0)]
    public byte DireccionTipo { get; set; }
    [Key]
    [Column(Order = 1)]
    public int DireccionID { get; set; }
    [Key]
    [Column(Order = 2)]
    public byte OwnerType { get; set; }
    [Key]
    [Column(Order = 3)]
    public int OwnerID { get; set; }
}
