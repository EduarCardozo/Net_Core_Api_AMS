using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("SistemasExternosEntidadesCodigos")]
public class SistemasExternosEntidadesCodigos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SistemaExternoEntidadCodigoID { get; set; }
    [Required]
    public short SistemaExternoID { get; set; }
    [Required]
    public int OwnerID { get; set; }
    [Required]
    public byte TargetType { get; set; }
    [Required]
    public int TargetID { get; set; }
    [MaxLength(30)]
    public string? Codigo { get; set; }
    public int? IDExterno { get; set; }
}
