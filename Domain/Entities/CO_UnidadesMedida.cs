using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("CO_UnidadesMedida")]
public class CO_UnidadesMedida
{
    [Key]
    public byte UnidadMedidaID { get; set; }
    [Required]
    [MaxLength(20)]
    public string Nombre { get; set; } = null!;
    [Required]
    [MaxLength(4)]
    public string Abreviatura { get; set; } = null!;
    [Required]
    public byte Tipo { get; set; }
}
