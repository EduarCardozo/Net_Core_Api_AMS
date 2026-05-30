using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("LicenciasLogs")]
public class LicenciasLogs
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LicenciaLogID { get; set; }
    public int? LicenciaID { get; set; }
    [Required]
    public int DepositoID { get; set; }
    [Required]
    public int RegistracionUsuario { get; set; }
    public DateTime? RegistracionFecha { get; set; }
    [MaxLength(50)]
    public string? RegistracionNumero { get; set; }
    [Required]
    public byte RegistracionEstado { get; set; }
    public int? UsuarioLoggedIn { get; set; }
    [MaxLength(15)]
    public string? IPAddressLoggedIn { get; set; }
    public DateTime? FechaLoggedIn { get; set; }
    [MaxLength(50)]
    public string? ComputerName { get; set; }
    [MaxLength(15)]
    public string? WFICS_Version { get; set; }
    [MaxLength(40)]
    public string? Script_Engine { get; set; }
    public int? CPU_ProcessorType { get; set; }
    public short? CPU_ProcessorLevel { get; set; }
    public int? CPU_ProcessorRevision { get; set; }
    public int? CPU_NumberOfProcessors { get; set; }
    [Required]
    public int CPU_PageSize { get; set; }
    public int? OS_PlattformID { get; set; }
    public int? OS_MajorVersion { get; set; }
    public int? OS_MinorVersion { get; set; }
    public int? OS_BuildNumber { get; set; }
    [MaxLength(50)]
    public string? OS_CSDVersion { get; set; }
    [MaxLength(50)]
    public string? VO_RootPathName { get; set; }
    [MaxLength(50)]
    public string? VO_NameBuffer { get; set; }
    [Required]
    public int VO_SerialNumber { get; set; }
    public int? VO_SystemFlags { get; set; }
    [MaxLength(50)]
    public string? VO_FileSystemNameBuffer { get; set; }
    [MaxLength(50)]
    public string? NAV_Name { get; set; }
    [MaxLength(255)]
    public string? NAV_Version { get; set; }
    public short? NAV_CookieEnabled { get; set; }
    [MaxLength(50)]
    public string? NAV_SystemLanguage { get; set; }
    [Required]
    public byte Estado { get; set; }
    public int? UsuarioHabilitacion { get; set; }
    [Required]
    public byte RequiereAutorizacion { get; set; }
}
