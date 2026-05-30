using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("EquiposMediciones")]
public class EquiposMediciones
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EquipoMedicionID { get; set; }
    [Required]
    public int EquipoID { get; set; }
    [Required]
    public byte MedicionTipo { get; set; }
    [Required]
    public DateTime FechaLectura { get; set; }
    [Required]
    public decimal Valor { get; set; }
    public int? UsuarioID { get; set; }
    [MaxLength(50)]
    public string? ReferenciaExterna { get; set; }
    public DateTime? ReferenciaExternaFechaCierre { get; set; }
}
