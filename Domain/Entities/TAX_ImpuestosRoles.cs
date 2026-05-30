using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TAX_ImpuestosRoles")]
public class TAX_ImpuestosRoles
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte ImpuestoRolID { get; set; }
    [Required]
    [MaxLength(30)]
    public string Nombre { get; set; } = null!;
    [MaxLength(5)]
    public string? Codigo { get; set; }
}
