using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("OrdenesTrabajo")]
public class OrdenesTrabajo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrdenTrabajoID { get; set; }
    [MaxLength(20)]
    public string? Numero { get; set; }
    [Required]
    public byte Tipo { get; set; }
    public int? PlantillaOTID { get; set; }
    [Required]
    public DateTime CreacionFecha { get; set; }
    public int? CreacionUsuarioID { get; set; }
    public DateTime? EntregaFechaLimite { get; set; }
    public int? EquipoID { get; set; }
    public DateTime? InicioFecha { get; set; }
    public DateTime? CierreFecha { get; set; }
    public decimal? CostoValor { get; set; }
    public byte? Estado { get; set; }
    public int? DepositoID { get; set; }
    public int? NovedadTecnicaID { get; set; }
    public short? CostoHH { get; set; }
}
