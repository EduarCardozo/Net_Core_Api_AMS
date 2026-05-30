using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Roles")]
public class Roles
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }
    [MaxLength(50)]
    public string? Nombre { get; set; }
    [MaxLength(100)]
    public string? Descripcion { get; set; }
    [Required]
    public byte Tipo { get; set; }
}
