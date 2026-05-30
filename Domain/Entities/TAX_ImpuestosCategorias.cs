using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TAX_ImpuestosCategorias")]
public class TAX_ImpuestosCategorias
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ImpuestoCategoriaID { get; set; }
    [Required]
    public short ImpuestoID { get; set; }
    [Required]
    [MaxLength(30)]
    public string Nombre { get; set; } = null!;
    [MaxLength(5)]
    public string? Codigo { get; set; }
    [Required]
    public byte RolPagador { get; set; }
    [Required]
    public byte RolFacturador { get; set; }
    [Required]
    public byte RolTributador { get; set; }
    [Required]
    public byte RolAgenteRetencion { get; set; }
    [Required]
    public byte RolAgentePercepcion { get; set; }
}
