using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("PartesItems")]
public class PartesItems
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ParteItemID { get; set; }
    [Required]
    public int ParteID { get; set; }
    [Required]
    public int DepositoID { get; set; }
    [MaxLength(40)]
    public string? NumeroSerie { get; set; }
    [Required]
    public byte Estado { get; set; }
}
