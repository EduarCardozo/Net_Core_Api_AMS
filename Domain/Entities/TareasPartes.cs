using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TareasPartes")]
public class TareasPartes
{
    [Key]
    [Column(Order = 0)]
    public int TareaID { get; set; }
    [Key]
    [Column(Order = 1)]
    public int ParteID { get; set; }
    [Required]
    public decimal Cantidad { get; set; }
}
