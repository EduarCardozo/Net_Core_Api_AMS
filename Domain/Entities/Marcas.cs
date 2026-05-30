using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Marcas")]
public class Marcas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short MarcaID { get; set; }
    [MaxLength(5)]
    public string? Codigo { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
}
