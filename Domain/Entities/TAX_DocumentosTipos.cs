using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TAX_DocumentosTipos")]
public class TAX_DocumentosTipos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DocumentoTipoID { get; set; }
    [MaxLength(50)]
    public string? Nombre { get; set; }
    [MaxLength(5)]
    public string? Codigo { get; set; }
}
