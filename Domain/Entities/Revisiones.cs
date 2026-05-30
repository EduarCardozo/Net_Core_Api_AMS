using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Revisiones")]
public class Revisiones
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RevisionID { get; set; }
    [Required]
    public int PlantillaID { get; set; }
    [Required]
    public int PlantillaTipoID { get; set; }
    [Required]
    public int EquipoID { get; set; }
    public int? ViajeID { get; set; }
    public short? SistemaExternoID { get; set; }
    [Required]
    public DateTime FechaProgramada { get; set; }
    [Required]
    public DateTime FechaVencimiento { get; set; }
    public DateTime? FechaCarga { get; set; }
    [Required]
    public byte Estado { get; set; }
    public int? UsuarioAuditorID { get; set; }
}
