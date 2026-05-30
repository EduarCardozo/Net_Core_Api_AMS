using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("PartesStockMovimientos")]
public class PartesStockMovimientos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ParteStockMovimientoID { get; set; }
    [Required]
    public DateTime Fecha { get; set; }
    [Required]
    public byte TipoMovimiento { get; set; }
    [Required]
    public int DepositoID { get; set; }
    [Required]
    public int ParteID { get; set; }
    [Required]
    public byte UnidadMedidaID { get; set; }
    [Required]
    public decimal StockAnterior { get; set; }
    [Required]
    public decimal Cantidad { get; set; }
    [Required]
    public decimal StockSaldo { get; set; }
}
