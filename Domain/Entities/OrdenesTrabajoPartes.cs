using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("OrdenesTrabajoPartes")]
public class OrdenesTrabajoPartes
{
    [Key]
    [Column(Order = 0)]
    public int OrdenTrabajoID { get; set; }
    [Key]
    [Column(Order = 1)]
    public int ParteID { get; set; }
    [Required]
    public decimal Cantidad { get; set; }
    public int? UsuarioID { get; set; }
    [Required]
    public DateTime FechaAsignacion { get; set; }
}
