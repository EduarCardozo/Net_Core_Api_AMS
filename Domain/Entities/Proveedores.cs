using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Proveedores")]
public class Proveedores
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProveedorID { get; set; }
    [Required]
    [MaxLength(50)]
    public string RazonSocial { get; set; } = null!;
    [Required]
    [MaxLength(3)]
    public string Codigo { get; set; } = null!;
}
