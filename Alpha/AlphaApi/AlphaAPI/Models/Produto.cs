using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaAPI.Models;

public class Produto
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required] // Torna o campo obrigatório
    [StringLength(100)] // Define o comprimento máximo do campo
    public string Title { get; set; }

    [Range(0.01, double.MaxValue)] // Define um intervalo para o campo
    public decimal Price { get; set; }

    [StringLength(20)]
    public string Description { get; set; }

    [Url]
    public string Image { get; set; }
}