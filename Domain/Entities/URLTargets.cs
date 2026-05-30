using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("URLTargets")]
public class URLTargets
{
    [Key]
    public int URLTargetID { get; set; }
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = null!;
}
