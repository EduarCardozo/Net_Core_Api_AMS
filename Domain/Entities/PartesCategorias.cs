using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("PartesCategorias")]
public class PartesCategorias
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short ParteCategoriaID { get; set; }
    [Required]
    [MaxLength(5)]
    public string Codigo { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [Required]
    public byte UtilizaNumeroSerie { get; set; }
}
