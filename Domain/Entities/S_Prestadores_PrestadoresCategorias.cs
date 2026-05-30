using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Api.Domain.Entities;

[Table("S_Prestadores_PrestadoresCategorias")]
public class S_Prestadores_PrestadoresCategorias
{
    [Key]
    [Column(Order = 0)]
    public int PrestadorId { get; set; }
    [Key]
    [Column(Order = 1)]
    public short PrestadorCategoriaId { get; set; }
}
