using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Remitos")]
public class Remitos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RemitoID { get; set; }
    [Required]
    [MaxLength(20)]
    public string Numero { get; set; } = null!;
    [Required]
    public byte SenderType { get; set; }
    [Required]
    public int SenderId { get; set; }
    [Required]
    public DateTime Fecha { get; set; }
    [Required]
    public byte ReceiverType { get; set; }
    [Required]
    public int ReceiverId { get; set; }
    public int? OrdenTrabajoID { get; set; }
}
