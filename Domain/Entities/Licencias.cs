using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Licencias")]
public class Licencias
{
    [Key]
    public int LicenciaID { get; set; }
    public DateTime? VigenciaDesde { get; set; }
    public DateTime? VigenciaHasta { get; set; }
    public byte? Estado { get; set; }
    public byte? TipoLicencia { get; set; }
    public short? SubTipoLicencia { get; set; }
    public short? ProductoModuloID { get; set; }
    public int? LicenciaGlobalID { get; set; }
}
