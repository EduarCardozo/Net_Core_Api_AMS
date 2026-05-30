using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("RES_NovedadesOrdenesTrabajo")]
public class RES_NovedadesOrdenesTrabajo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NovedadOrdenTrabajoID { get; set; }
    [Required]
    public int NovedadID { get; set; }
    [Required]
    public int OrdenTrabajoID { get; set; }
}
