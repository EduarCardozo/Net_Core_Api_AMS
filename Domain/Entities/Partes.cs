using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Partes")]
public class Partes
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ParteID { get; set; }
    [Required]
    [MaxLength(20)]
    public string Codigo { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    public short? MarcaID { get; set; }
    public int? MarcaModeloID { get; set; }
    [Required]
    public short ParteCategoriaID { get; set; }
    [Required]
    [MaxLength(200)]
    public string Descripcion { get; set; } = null!;
    [Required]
    public byte UnidadMedidaID { get; set; }
    [Required]
    public bool VerificaNumeroSerie { get; set; }
}
