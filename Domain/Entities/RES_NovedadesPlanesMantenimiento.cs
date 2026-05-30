using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("RES_NovedadesPlanesMantenimiento")]
public class RES_NovedadesPlanesMantenimiento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NovedadPlanMantenimientoID { get; set; }
    [Required]
    public int NovedadID { get; set; }
    [Required]
    public int PlanMantenimientoID { get; set; }
}
