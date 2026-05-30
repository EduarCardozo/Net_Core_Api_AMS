using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("S_PrestadoresCategorias")]
public class S_PrestadoresCategorias
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short PrestadorCategoriaID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
}
