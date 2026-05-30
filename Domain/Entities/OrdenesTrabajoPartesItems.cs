using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("OrdenesTrabajoPartesItems")]
public class OrdenesTrabajoPartesItems
{
    [Key]
    [Column(Order = 0)]
    public int OrdenTrabajoID { get; set; }
    [Key]
    [Column(Order = 1)]
    public int ParteItemID { get; set; }
}
