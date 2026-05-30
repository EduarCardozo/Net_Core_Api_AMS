using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Monedas")]
public class Monedas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }
    [MaxLength(20)]
    public string? Nombre { get; set; }
    [Required]
    [MaxLength(10)]
    public string NombreCorto { get; set; } = null!;
    [Required]
    [MaxLength(3)]
    public string Simbolo { get; set; } = null!;
    [Required]
    public short Pais { get; set; }
    [MaxLength(3)]
    public string? ISOCode { get; set; }
    [Required]
    public byte Defecto { get; set; }
    [Required]
    public byte Estado { get; set; }
}
