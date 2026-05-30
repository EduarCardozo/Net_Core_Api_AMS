using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("OrdenesTrabajoOperaciones")]
public class OrdenesTrabajoOperaciones
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrdenTrabajoOperacionID { get; set; }
    [Required]
    public int OrdenTrabajoID { get; set; }
    [Required]
    public DateTime FechaOperacion { get; set; }
    public int? UsuarioID { get; set; }
    [Required]
    public byte Operacion { get; set; }
}
