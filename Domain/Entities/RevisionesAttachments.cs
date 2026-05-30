using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("RevisionesAttachments")]
public class RevisionesAttachments
{
    [Key]
    [Column(Order = 0)]
    public int RevisionID { get; set; }
    [Key]
    [Column(Order = 1)]
    public byte Type { get; set; }
    [Required]
    public string Attachment { get; set; } = null!;
}
