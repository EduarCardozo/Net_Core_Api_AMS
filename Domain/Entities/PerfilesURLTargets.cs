using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("PerfilesURLTargets")]
public class PerfilesURLTargets
{
    [Key]
    [Column(Order = 0)]
    public int PerfilID { get; set; }
    [Key]
    [Column(Order = 1)]
    public int URLTargetID { get; set; }
}
