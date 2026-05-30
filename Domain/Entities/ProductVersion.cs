using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("ProductVersion")]
public class ProductVersion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductoVersionId { get; set; }
    [Required]
    public short Version { get; set; }
    [Required]
    public DateTime Fecha { get; set; }
}
