namespace AlphaAPI.Data.DTOs.FakeStoreApi;

public class FakeStoreProductDTO
{
    public string Title { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Category { get; set; } = "General";
}