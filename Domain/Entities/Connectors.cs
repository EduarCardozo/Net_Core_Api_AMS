using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Connectors")]
public class Connectors
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ConnectorID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [Required]
    [MaxLength(200)]
    public string WebService { get; set; } = null!;
    [Required]
    public short Type { get; set; }
    [Required]
    [MaxLength(50)]
    public string Token { get; set; } = null!;
    [MaxLength(50)]
    public string? CodigoComercio { get; set; }
    [MaxLength(20)]
    public string? Usuario { get; set; }
    [MaxLength(80)]
    public string? Password { get; set; }
    public string? ConfiguracionXML { get; set; }
    [Required]
    public DateTime FechaDesde { get; set; }
    [Required]
    public DateTime FechaHasta { get; set; }
    [Required]
    public byte Estado { get; set; }
}
