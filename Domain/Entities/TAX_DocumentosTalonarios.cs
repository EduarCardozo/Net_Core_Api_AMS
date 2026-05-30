using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TAX_DocumentosTalonarios")]
public class TAX_DocumentosTalonarios
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DocumentoTalonarioID { get; set; }
    [Required]
    public int DocumentoTalonarioTipoID { get; set; }
    [Required]
    public byte OwnerTipo { get; set; }
    [Required]
    public int OwnerID { get; set; }
    [MaxLength(5)]
    public string? Serie { get; set; }
    [Required]
    public int Inicial { get; set; }
    [Required]
    public int Final { get; set; }
    [Required]
    public int Actual { get; set; }
    [Required]
    public byte Estado { get; set; }
}
