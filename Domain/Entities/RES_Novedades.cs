using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("RES_Novedades")]
public class RES_Novedades
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NovedadID { get; set; }
    [Required]
    public int NovedadTipoID { get; set; }
    [Required]
    public DateTime VigenciaDesde { get; set; }
    public DateTime? VigenciaHasta { get; set; }
    [Required]
    public byte TargetType { get; set; }
    public int? TargetID { get; set; }
}
