using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("OrdenesTrabajoUsuarios")]
public class OrdenesTrabajoUsuarios
{
    [Key]
    [Column(Order = 0)]
    public int OrdenTrabajoID { get; set; }
    [Key]
    [Column(Order = 1)]
    public int UsuarioID { get; set; }
    [Required]
    public DateTime FechaAsignacion { get; set; }
    [Required]
    public byte Estado { get; set; }
}
