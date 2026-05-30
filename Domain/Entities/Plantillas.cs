using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Plantillas")]
public class Plantillas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PlantillaID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [Required]
    public int UsuarioCreacionID { get; set; }
    [Required]
    public DateTime FechaCreacion { get; set; }
    [Required]
    public DateTime VigenciaDesde { get; set; }
    [Required]
    public DateTime VigenciaHasta { get; set; }
    [Required]
    public byte Estado { get; set; }
    [Required]
    public int PlantillaTipoID { get; set; }
}
