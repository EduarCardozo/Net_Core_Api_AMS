using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("G_Direcciones")]
public class G_Direcciones
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DireccionID { get; set; }
    public int? PaisID { get; set; }
    [Required]
    public int LocalidadID { get; set; }
    [MaxLength(50)]
    public string? Calle { get; set; }
    [MaxLength(5)]
    public string? Numero { get; set; }
    [MaxLength(5)]
    public string? Piso { get; set; }
    [MaxLength(5)]
    public string? Departamento { get; set; }
    [MaxLength(10)]
    public string? CodPostal { get; set; }
    public int? ComunaId { get; set; }
}
