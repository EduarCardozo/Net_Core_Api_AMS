using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TAX_ImpuestosAlicuotas")]
public class TAX_ImpuestosAlicuotas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ImpuestoAlicuotaID { get; set; }
    [Required]
    public short ImpuestoID { get; set; }
    [Required]
    public int S_ServicioID { get; set; }
    [Required]
    public byte TipoAlicuota { get; set; }
    [Required]
    public byte GeoNivelAplicacion { get; set; }
    [Required]
    public int GeoID { get; set; }
    [Required]
    public DateTime FechaInicio { get; set; }
    [Required]
    public DateTime FechaFin { get; set; }
    [Required]
    public short MonedaID { get; set; }
    public decimal? TopeMinimo { get; set; }
    public decimal? TopeMaximo { get; set; }
    [Required]
    public decimal BaseImponible { get; set; }
    [Required]
    public decimal MontoFijoNoImponible { get; set; }
    [Required]
    public decimal Alicuota { get; set; }
}
