using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TAX_RegimenesTributariosImpuestosCategorias")]
public class TAX_RegimenesTributariosImpuestosCategorias
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RegimenTributarioImpuestoCategoriaID { get; set; }
    [Required]
    public int RegimenTributarioID { get; set; }
    [Required]
    public int ImpuestoCategoriaID { get; set; }
}
