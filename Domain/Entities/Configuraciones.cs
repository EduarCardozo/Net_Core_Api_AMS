using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Configuraciones")]
public class Configuraciones
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ConfiguracionID { get; set; }
    [Required]
    public byte Type { get; set; }
    [Required]
    public int OwnerID { get; set; }
    [Required]
    public string ConfigXML { get; set; } = null!;
}
