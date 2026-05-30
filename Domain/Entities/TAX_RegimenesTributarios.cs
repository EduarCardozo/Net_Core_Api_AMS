using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("TAX_RegimenesTributarios")]
public class TAX_RegimenesTributarios
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RegimenTributarioID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [Required]
    public byte RegimenTributarioTipo { get; set; }
    [Required]
    public short PaisID { get; set; }
    [Required]
    public int AdministradorTributarioID { get; set; }
}
