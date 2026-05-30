using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("MarcasModelos")]
public class MarcasModelos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MarcaModeloID { get; set; }
    [Required]
    public short MarcaID { get; set; }
    [MaxLength(20)]
    public string? Codigo { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
}
