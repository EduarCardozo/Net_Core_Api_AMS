using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("Tareas")]
public class Tareas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TareaID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;
    [Required]
    [MaxLength(5)]
    public string Codigo { get; set; } = null!;
    [Required]
    public int EspecialidadID { get; set; }
    [Required]
    public byte Tipo { get; set; }
    [Required]
    public byte TipoAccionID { get; set; }
    [MaxLength(4000)]
    public string? Descripcion { get; set; }
    public short? CostoHH { get; set; }
}
