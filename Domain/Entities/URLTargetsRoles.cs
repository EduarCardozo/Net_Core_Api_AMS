using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("URLTargetsRoles")]
public class URLTargetsRoles
{
    [Key]
    [Column(Order = 0)]
    public int URLTargetID { get; set; }
    [Key]
    [Column(Order = 1)]
    public short RolID { get; set; }
}
