using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TAX_ImpuestosTimbrados")]
public class TAX_ImpuestosTimbrados
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ImpuestoTimbradoID { get; set; }
    [Required]
    public int S_PrestadorID { get; set; }
    [Required]
    public short ImpuestoID { get; set; }
    [Required]
    [MaxLength(20)]
    public string Numero { get; set; } = null!;
    [Required]
    public DateTime FechaEmision { get; set; }
    [Required]
    public DateTime FechaValidezDesde { get; set; }
    [Required]
    public DateTime FechaValidezHasta { get; set; }
    [Required]
    public short MonedaID { get; set; }
    [Required]
    public decimal Valor { get; set; }
}
