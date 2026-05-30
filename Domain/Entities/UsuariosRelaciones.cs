using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("UsuariosRelaciones")]
public class UsuariosRelaciones
{
    [Key]
    [Column(Order = 0)]
    public int UsuarioID { get; set; }
    [Key]
    [Column(Order = 1)]
    public byte RelacionTipo { get; set; }
    [Key]
    [Column(Order = 2)]
    public int RelacionID { get; set; }
    [MaxLength(1000)]
    public string? PushID { get; set; }
}
