using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("PlanesMantenimientoOrdenesTrabajo")]
public class PlanesMantenimientoOrdenesTrabajo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PlanMantenimientoOrdenTrabajoID { get; set; }
    [Required]
    public int PlanMantenimientoID { get; set; }
    public int? OrdenTrabajoID { get; set; }
    public byte? InicioUnidadMedidaID { get; set; }
    public decimal? InicioCantidad { get; set; }
    [Required]
    public byte Frecuencia { get; set; }
    public byte? FrecuenciaUnidadMedidaID { get; set; }
    public decimal? FrecuenciaCantidad { get; set; }
    public DateTime? FechaEjecutar { get; set; }
}
