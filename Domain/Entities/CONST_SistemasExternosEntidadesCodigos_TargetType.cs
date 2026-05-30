using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("CONST_SistemasExternosEntidadesCodigos_TargetType")]
public class CONST_SistemasExternosEntidadesCodigos_TargetType
{
    [Key]
    public byte TargetTypeId { get; set; }
    [Required]
    [MaxLength(50)]
    public string Codigo { get; set; } = null!;
}
