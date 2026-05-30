using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("RES_Feriados")]
public class RES_Feriados
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FeriadoID { get; set; }
    [Required]
    public DateTime FechaFeriado { get; set; }
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = null!;
    [Required]
    public DateTime FechaReal { get; set; }
    [Required]
    public byte GEONivelAplicacion { get; set; }
    [Required]
    public int GEOTargetID { get; set; }
}
