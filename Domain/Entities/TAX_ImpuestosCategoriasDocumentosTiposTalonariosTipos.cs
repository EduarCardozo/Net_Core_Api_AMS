using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TAX_ImpuestosCategorias_DocumentosTiposTalonariosTipos")]
public class TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ImpuestoCategoriaDocumentoTipoTalonarioTipoID { get; set; }
    [Required]
    public int EmisorImpuestoCategoria { get; set; }
    [Required]
    public int ReceptorImpuestoCategoria { get; set; }
    public short? ReceptorPais { get; set; }
    [Required]
    public int DocumentoTipoID { get; set; }
    [Required]
    public int DocumentoTalonarioTipoID { get; set; }
}
