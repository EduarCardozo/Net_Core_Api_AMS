using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("EquiposPartesItems")]
public class EquiposPartesItems
{
    [Key]
    [Column(Order = 0)]
    public int EquipoID { get; set; }
    [Key]
    [Column(Order = 1)]
    public int ParteItemID { get; set; }
}
