using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("G_Localidades")]
public class G_Localidades
{
    [Key]
    public int LocalidadID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [Required]
    public int RegionID { get; set; }
    [Required]
    public int SubRegionID { get; set; }
    [Required]
    public byte TipoCodPostal { get; set; }
    [MaxLength(15)]
    public string? CodPostal { get; set; }
    public double? Latitud { get; set; }
    public double? Longitud { get; set; }
    public short? GMT { get; set; }
    [Required]
    public short PaisID { get; set; }
    [MaxLength(5)]
    public string? Codigo { get; set; }
}
