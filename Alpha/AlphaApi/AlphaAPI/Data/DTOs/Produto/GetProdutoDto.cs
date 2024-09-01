namespace AlphaAPI.Data.DTOs;

public class GetProdutoDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
}