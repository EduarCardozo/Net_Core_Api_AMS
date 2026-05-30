using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TAX_RegimenesTributariosS_Prestadores")]
public class TAX_RegimenesTributariosS_Prestadores
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RegimenTributarioS_PrestadorID { get; set; }
    public int? RegimenTributarioID { get; set; }
    public int? S_PrestadorID { get; set; }
}
