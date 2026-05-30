using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("OrdenesTrabajoTareas")]
public class OrdenesTrabajoTareas
{
    [Key]
    [Column(Order = 0)]
    public int OrdenTrabajoID { get; set; }
    [Key]
    [Column(Order = 1)]
    public int TareaID { get; set; }
    [Key]
    [Column(Order = 2)]
    public int EspecialidadID { get; set; }
    public DateTime? CierreFecha { get; set; }
    public byte? Estado { get; set; }
}
