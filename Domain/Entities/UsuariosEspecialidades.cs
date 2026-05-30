using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("UsuariosEspecialidades")]
public class UsuariosEspecialidades
{
    [Key]
    [Column(Order = 0)]
    public int EspecialidadID { get; set; }
    [Key]
    [Column(Order = 1)]
    public int UsuarioID { get; set; }
}
