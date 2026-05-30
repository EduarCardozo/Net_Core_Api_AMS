using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("UsuariosPerfiles")]
public class UsuariosPerfiles
{
    [Key]
    [Column(Order = 0)]
    public int UsuarioID { get; set; }
    public short? EmpresaID { get; set; }
    [Key]
    [Column(Order = 1)]
    public int PerfilID { get; set; }
}
