using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TAX_PersonasJuridicasTipos")]
public class TAX_PersonasJuridicasTipos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte PersonaJuridicaTipoID { get; set; }
    [Required]
    [MaxLength(30)]
    public string NombreAR { get; set; } = null!;
    [MaxLength(30)]
    public string? NombreCO { get; set; }
}
