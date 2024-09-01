using AlphaAPI.Data.DTOs.FakeStoreApi;

namespace AlphaAPI.Services;

public interface IFakeStoreAPIService
{
    Task<FakeStoreProductDTO> CreateProductAsync(FakeStoreProductDTO product);
    Task<FakeStoreProductDTO> UpdateProductAsync(int productId, FakeStoreProductDTO product);
    Task<bool> DeleteProductAsync(int productId);
}