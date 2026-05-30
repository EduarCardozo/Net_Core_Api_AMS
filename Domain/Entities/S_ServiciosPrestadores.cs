using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("S_ServiciosPrestadores")]
public class S_ServiciosPrestadores
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ServicioPrestadorID { get; set; }
    [Required]
    public int ServicioID { get; set; }
    [Required]
    public int PrestadorID { get; set; }
    public byte? PorCtaOrdenTerceros { get; set; }
}
