using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TAX_AdministradoresTributariosActividadesEconomicas")]
public class TAX_AdministradoresTributariosActividadesEconomicas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AdministradorTributarioActividadEconomicaID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Codigo { get; set; } = null!;
    [Required]
    public int AdministradorTributarioID { get; set; }
    public int? S_ServicioID { get; set; }
}
