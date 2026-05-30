using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("PartesStocks")]
public class PartesStocks
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ParteStockID { get; set; }
    [Required]
    public int DepositoID { get; set; }
    [Required]
    public int ParteID { get; set; }
    [Required]
    public byte UnidadMedidaID { get; set; }
    [Required]
    public decimal Stock { get; set; }
}
