using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("UsuariosRoles")]
public class UsuariosRoles
{
    [Key]
    [Column(Order = 0)]
    public int Usuario { get; set; }
    [Key]
    [Column(Order = 1)]
    public short Rol { get; set; }
    public int? Empresa { get; set; }
}
