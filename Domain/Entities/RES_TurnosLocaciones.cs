using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("RES_TurnosLocaciones")]
public class RES_TurnosLocaciones
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TurnoLocacionID { get; set; }
    [Required]
    public int TurnoID { get; set; }
    [Required]
    public byte PlaceType { get; set; }
    public int? PlaceID { get; set; }
}
