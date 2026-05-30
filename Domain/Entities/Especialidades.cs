using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Especialidades")]
public class Especialidades
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EspecialidadID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [Required]
    [MaxLength(3)]
    public string Codigo { get; set; } = null!;
}
