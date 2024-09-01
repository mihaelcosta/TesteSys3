using System.Text;
using System.Text.Json;
using AlphaAPI.Data.DTOs.FakeStoreApi;

namespace AlphaAPI.Services;

public class FakeStoreAPIService : IFakeStoreAPIService
{
    private readonly HttpClient _httpClient;
    
    public FakeStoreAPIService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<FakeStoreProductDTO> CreateProductAsync(FakeStoreProductDTO product)
    {
        try
        {
            // Serializa o objeto ProductDTO em JSON
            var jsonContent = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

            // Faz a requisição POST para a API externa
            var response = await _httpClient.PostAsync("https://fakestoreapi.com/products", jsonContent);

            // Verifica se a resposta foi bem-sucedida
            response.EnsureSuccessStatusCode();

            // Lê o conteúdo da resposta e desserializa para ProductDTO
            var responseContent = await response.Content.ReadAsStringAsync();
            var createdProduct = JsonSerializer.Deserialize<FakeStoreProductDTO>(responseContent);

            return createdProduct;
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine($"Erro na requisição HTTP: {httpEx.Message}");
            throw;
        }
        catch (JsonException jsonEx)
        {
            Console.WriteLine($"Erro ao processar JSON: {jsonEx.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            throw;
        }
    }

    public async Task<FakeStoreProductDTO> UpdateProductAsync(int productId, FakeStoreProductDTO product)
    {
        try
        {
            // Serializa o objeto ProductDTO em JSON
            var jsonContent = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

            // Faz a requisição PUT para a API externa
            var response = await _httpClient.PutAsync($"https://fakestoreapi.com/products/{productId}", jsonContent);

            // Verifica se a resposta foi bem-sucedida
            response.EnsureSuccessStatusCode();

            // Lê o conteúdo da resposta e desserializa para ProductDTO
            var responseContent = await response.Content.ReadAsStringAsync();
            var updatedProduct = JsonSerializer.Deserialize<FakeStoreProductDTO>(responseContent);

            return updatedProduct;
        }
        catch (HttpRequestException httpEx)
        {
            // Tratar exceções relacionadas a requisições HTTP
            Console.WriteLine($"Erro na requisição HTTP: {httpEx.Message}");
            throw;
        }
        catch (JsonException jsonEx)
        {
            // Tratar exceções relacionadas à serialização/deserialização JSON
            Console.WriteLine($"Erro ao processar JSON: {jsonEx.Message}");
            throw;
        }
        catch (Exception ex)
        {
            // Tratar outras exceções gerais
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            throw;
        }
    }
    
    public async Task<bool> DeleteProductAsync(int productId)
    {
        try
        {
            // Faz a requisição DELETE para a API externa
            var response = await _httpClient.DeleteAsync($"https://fakestoreapi.com/products/{productId}");

            // Verifica se a resposta foi bem-sucedida
            if (response.IsSuccessStatusCode)
            {
                // A resposta geralmente não contém corpo para uma requisição DELETE bem-sucedida
                return true;
            }
            else
            {
                // Lê o conteúdo da resposta para possíveis mensagens de erro
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erro ao deletar o produto: {responseContent}");
                return false;
            }
        }
        catch (HttpRequestException httpEx)
        {
            // Tratar exceções relacionadas a requisições HTTP
            Console.WriteLine($"Erro na requisição HTTP: {httpEx.Message}");
            throw; // Re-throw ou trate o erro conforme necessário
        }
        catch (Exception ex)
        {
            // Tratar outras exceções gerais
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            throw; // Re-throw ou trate o erro conforme necessário
        }
    }
}