using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("S_Prestadores")]
public class S_Prestadores
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PrestadorID { get; set; }
    [MaxLength(50)]
    public string? RazonSocial { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [MaxLength(20)]
    public string? Telefono { get; set; }
    [MaxLength(20)]
    public string? Fax { get; set; }
    [MaxLength(50)]
    public string? Email { get; set; }
    [Required]
    public short PrestadorCategoriaID { get; set; }
    [MaxLength(13)]
    public string? TAXID { get; set; }
    [Required]
    public byte Estado { get; set; }
    public byte? TAXPersonaTipoID { get; set; }
    public byte? TAXPersonaJuridicaTipoID { get; set; }
    public short? TAXPaisDocumentoID { get; set; }
}
