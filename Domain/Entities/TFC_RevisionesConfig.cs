using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TFC_RevisionesConfig")]
public class TFC_RevisionesConfig
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RevisionConfigID { get; set; }
    [Required]
    public short SistemaExternoID { get; set; }
    [Required]
    public byte TraficoNivel { get; set; }
    [Required]
    public int TraficoID { get; set; }
    public int? PlantillaID { get; set; }
    [Required]
    public byte EquipoNivel { get; set; }
    [Required]
    public int EquipoNivelID { get; set; }
    [Required]
    public DateTime VigenciaDesde { get; set; }
    [Required]
    public DateTime VigenciaHasta { get; set; }
}
