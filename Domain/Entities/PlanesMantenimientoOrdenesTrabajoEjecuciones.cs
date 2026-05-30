using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("PlanesMantenimientoOrdenesTrabajoEjecuciones")]
public class PlanesMantenimientoOrdenesTrabajoEjecuciones
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PlanMantenimientoOrdenTrabajoEjecucionID { get; set; }
    [Required]
    public int PlanMantenimientoOrdenTrabajoID { get; set; }
    [Required]
    public int EquipoID { get; set; }
    [Required]
    public DateTime FechaEjecucion { get; set; }
    [Required]
    public decimal Medicion { get; set; }
    public int? OrdenTrabajoID { get; set; }
}
