using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Talleres")]
public class Talleres
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TallerID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [Required]
    [MaxLength(5)]
    public string Codigo { get; set; } = null!;
    [MaxLength(50)]
    public string? Responsable { get; set; }
    [MaxLength(200)]
    public string? Email { get; set; }
    [MaxLength(20)]
    public string? Telefono { get; set; }
    [Required]
    public byte Tipo { get; set; }
}
