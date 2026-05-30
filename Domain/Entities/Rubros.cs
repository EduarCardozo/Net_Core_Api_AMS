using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Rubros")]
public class Rubros
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RubroID { get; set; }
    [Required]
    [MaxLength(250)]
    public string Nombre { get; set; } = null!;
    [MaxLength(6)]
    public string? Color { get; set; }
}
