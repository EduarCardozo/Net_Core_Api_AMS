using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("SistemasExternos")]
public class SistemasExternos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short SistemaExternoID { get; set; }
    [MaxLength(50)]
    public string? Nombre { get; set; }
}
