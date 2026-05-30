using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("OrdenesTrabajoDetalles")]
public class OrdenesTrabajoDetalles
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrdenTrabajoDetalleID { get; set; }
    [Required]
    public int OrdenTrabajoID { get; set; }
    [MaxLength(4)]
    public string? Extension { get; set; }
    [Required]
    public byte[] Data { get; set; } = null!;
}
