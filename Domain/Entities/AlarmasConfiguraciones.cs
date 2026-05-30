using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("AlarmasConfiguraciones")]
public class AlarmasConfiguraciones
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AlarmaConfiguracionID { get; set; }
    [Required]
    public byte Tipo { get; set; }
    [Required]
    public int UsuarioID { get; set; }
    [Required]
    public int TargetID { get; set; }
    [Required]
    public byte ValorTipo { get; set; }
    [Required]
    public decimal Valor { get; set; }
    [Required]
    public byte Estado { get; set; }
    [Required]
    public byte OutputChannel { get; set; }
    [MaxLength(100)]
    public string? Email { get; set; }
}
