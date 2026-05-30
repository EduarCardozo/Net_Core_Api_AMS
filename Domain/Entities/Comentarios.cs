using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Comentarios")]
public class Comentarios
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ComentarioID { get; set; }
    [Required]
    public byte OwnerType { get; set; }
    [Required]
    [MaxLength(8000)]
    public string Texto { get; set; } = null!;
    [Required]
    public int OwnerID { get; set; }
    public DateTime? Fecha { get; set; }
    public int? UsuarioID { get; set; }
}
