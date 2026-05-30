using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Alarmas")]
public class Alarmas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AlarmaID { get; set; }
    [Required]
    public int AlarmaConfiguracionID { get; set; }
    [Required]
    public DateTime Fecha { get; set; }
    [Required]
    public decimal ValorReferencia { get; set; }
    [Required]
    public byte Estado { get; set; }
    public DateTime? FechaDiferida { get; set; }
    public DateTime? FechaDesactivacion { get; set; }
}
