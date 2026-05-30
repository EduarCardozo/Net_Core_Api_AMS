using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("EquiposTipos")]
public class EquiposTipos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte EquipoTipoID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Codigo { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
}
