using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Items")]
public class Items
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ItemID { get; set; }
    [Required]
    [MaxLength(250)]
    public string Nombre { get; set; } = null!;
    [Required]
    public int RubroID { get; set; }
    [Required]
    public int InhabilitaTurno { get; set; }
}
