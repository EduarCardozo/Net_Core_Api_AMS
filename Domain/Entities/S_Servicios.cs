using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("S_Servicios")]
public class S_Servicios
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int S_ServicioID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [Required]
    public int S_ServicioCategoriaID { get; set; }
}
