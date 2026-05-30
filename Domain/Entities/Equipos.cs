using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Equipos")]
public class Equipos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EquipoID { get; set; }
    [Required]
    public byte EquipoTipoID { get; set; }
    public short? MarcaID { get; set; }
    public int? MarcaModeloID { get; set; }
    [MaxLength(10)]
    public string? Codigo { get; set; }
    [Required]
    [MaxLength(20)]
    public string Nombre { get; set; } = null!;
    public short? EquipoGrupoID { get; set; }
    public Guid? OpenID { get; set; }
}
