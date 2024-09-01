using AlphaAPI.Data.DTOs;
using AlphaAPI.Models;

namespace AlphaAPI.Services;

public interface IProdutoService
{
    Task<(List<GetProdutoDto> Produtos, int TotalItems)> BuscarProdutosAsync(string? nome, string? codigo, int skip,
        int take);
    Task<Produto> AdicionarProdutoAsync(CreateProdutoDto produtoDto);
    Task<bool> EditarProdutoAsync(int id, UpdateProdutoDto produtoDto);
    Task<bool> ExcluirProdutoAsync(int id);
}