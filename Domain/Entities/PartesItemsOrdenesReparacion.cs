using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("PartesItemsOrdenesReparacion")]
public class PartesItemsOrdenesReparacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ParteItemOrdenReparacionID { get; set; }
    public int? ParteItemID { get; set; }
    public int? OrdenReparacionID { get; set; }
}
