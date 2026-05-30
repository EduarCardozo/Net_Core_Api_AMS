using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("PartesItemsRemitos")]
public class PartesItemsRemitos
{
    [Key]
    [Column(Order = 0)]
    public int ParteItemID { get; set; }
    [Key]
    [Column(Order = 1)]
    public int RemitoID { get; set; }
}
