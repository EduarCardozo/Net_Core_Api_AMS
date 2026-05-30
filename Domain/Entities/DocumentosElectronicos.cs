using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("DocumentosElectronicos")]
public class DocumentosElectronicos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DocumentoElectronicoId { get; set; }
    [Required]
    public byte OwnerType { get; set; }
    public int? OwnerId { get; set; }
    [Required]
    [MaxLength(200)]
    public string FileName { get; set; } = null!;
    public byte[]? Data { get; set; }
    public DateTime? LastUpdate { get; set; }
    public DateTime? Fecha { get; set; }
    public int? FileSize { get; set; }
    [MaxLength(200)]
    public string? S3Key { get; set; }
    [MaxLength(20)]
    public string? ContentType { get; set; }
}
