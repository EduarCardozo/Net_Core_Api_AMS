using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("RES_NovedadesReemplazos")]
public class RES_NovedadesReemplazos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NovedadReemplazoID { get; set; }
    [Required]
    public int NovedadID { get; set; }
    [Required]
    public byte TargetType { get; set; }
    [Required]
    public int TargetID { get; set; }
    [MaxLength(1000)]
    public string? Comentario { get; set; }
}
