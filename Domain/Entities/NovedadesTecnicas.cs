using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("NovedadesTecnicas")]
public class NovedadesTecnicas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NovedadTecnicaID { get; set; }
    [Required]
    public int PlantillaID { get; set; }
    [Required]
    public DateTime FechaOperacion { get; set; }
    [Required]
    public int EquipoID { get; set; }
    [Required]
    public int UsuarioID { get; set; }
    public int? ViajeID { get; set; }
    [Required]
    public byte Estado { get; set; }
    [MaxLength(20)]
    public string? Numero { get; set; }
    public int? RevisionID { get; set; }
}
