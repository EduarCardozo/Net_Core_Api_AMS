using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("RES_Turnos")]
public class RES_Turnos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TurnoID { get; set; }
    [MaxLength(100)]
    public string? Nombre { get; set; }
    [MaxLength(5)]
    public string? Codigo { get; set; }
    [Required]
    public byte Tipo { get; set; }
    [Required]
    public DateTime VigenciaDesde { get; set; }
    public DateTime? VigenciaHasta { get; set; }
    [Required]
    [MaxLength(7)]
    public string Frecuencia { get; set; } = null!;
    public DateTime? HoraDesde { get; set; }
    public DateTime? HoraHasta { get; set; }
    [Required]
    public byte Estado { get; set; }
}
