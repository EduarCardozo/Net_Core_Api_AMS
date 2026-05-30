using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("RES_TurnosRecursos")]
public class RES_TurnosRecursos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TurnoRecursoID { get; set; }
    [Required]
    public int TurnoID { get; set; }
    [Required]
    public byte TargetType { get; set; }
    [Required]
    public int TargetID { get; set; }
    [Required]
    public int TurnoLocacionID { get; set; }
    [Required]
    public DateTime VigenciaDesde { get; set; }
    public DateTime? VigenciaHasta { get; set; }
}
