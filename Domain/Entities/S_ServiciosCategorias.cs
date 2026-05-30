using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("S_ServiciosCategorias")]
public class S_ServiciosCategorias
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int S_ServicioCategoriaID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
}
