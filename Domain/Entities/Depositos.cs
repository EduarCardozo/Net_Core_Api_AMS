using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Depositos")]
public class Depositos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DepositoID { get; set; }
    [Required]
    [MaxLength(3)]
    public string Codigo { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
}
