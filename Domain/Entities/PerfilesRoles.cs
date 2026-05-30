using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("PerfilesRoles")]
public class PerfilesRoles
{
    [Key]
    [Column(Order = 0)]
    public int PerfilID { get; set; }
    [Key]
    [Column(Order = 1)]
    public short RolID { get; set; }
}
