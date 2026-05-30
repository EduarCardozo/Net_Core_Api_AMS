using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("RevisionesEstados")]
public class RevisionesEstados
{
    [Key]
    [Column(Order = 0)]
    public int RevisionID { get; set; }
    [Key]
    [Column(Order = 1)]
    public int PlantillaID { get; set; }
    [Key]
    [Column(Order = 2)]
    public int ItemID { get; set; }
    [Required]
    public byte Estado { get; set; }
    [Required]
    [MaxLength(200)]
    public string Comentario { get; set; } = null!;
}
