using System.ComponentModel.DataAnnotations;

namespace AlphaAPI.Data.DTOs;

public class UpdateProdutoDto
{
    [Required(ErrorMessage = "O nome do produto é obrigatório!", AllowEmptyStrings = false)]
    [StringLength(100)]
    public string Title { get; set; }
    [Required(ErrorMessage = "O preço do produto é obrigatório!", AllowEmptyStrings = false)]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "o código de barras é obrigatório!", AllowEmptyStrings = false)]
    [StringLength(20)]
    public string Description { get; set; }
    public byte[]? Imagem{ get; set; }
}