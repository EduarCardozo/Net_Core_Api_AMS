using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TAX_AdministradoresTributarios")]
public class TAX_AdministradoresTributarios
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AdministradorTributarioID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [MaxLength(50)]
    public string? Codigo { get; set; }
    [Required]
    public byte GeoNivelAplicacion { get; set; }
    [Required]
    public int GeoTargetID { get; set; }
    [Required]
    public short PaisID { get; set; }
    [Required]
    public int S_PrestadorID { get; set; }
}
