using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Usuarios")]
public class Usuarios
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UsuarioID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [MaxLength(50)]
    public string? Email { get; set; }
    [Required]
    [MaxLength(15)]
    public string LoginName { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Password { get; set; } = null!;
    [Required]
    public byte MenuReload { get; set; }
    [Required]
    public byte Estado { get; set; }
    public string? MenuXML { get; set; }
    [MaxLength(50)]
    public string? SmartCardCredential { get; set; }
    public string? PermisosXML { get; set; }
    public Guid? OpenID { get; set; }
    [Required]
    public int DepositoID { get; set; }
}
