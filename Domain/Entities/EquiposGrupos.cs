using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("EquiposGrupos")]
public class EquiposGrupos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short EquipoGrupoID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
}
