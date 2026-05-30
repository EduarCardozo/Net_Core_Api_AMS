using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("RemitosPartes")]
public class RemitosPartes
{
    [Key]
    [Column(Order = 0)]
    public int RemitoID { get; set; }
    [Key]
    [Column(Order = 1)]
    public int ParteID { get; set; }
    [Required]
    public decimal Cantidad { get; set; }
    [Required]
    public byte UnidadMedidaID { get; set; }
}
