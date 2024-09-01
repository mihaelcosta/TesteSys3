using AlphaAPI.Data;
using AlphaAPI.Data.DTOs;
using AlphaAPI.Data.DTOs.FakeStoreApi;
using AlphaAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AlphaAPI.Services;

public class ProdutoService : IProdutoService
{
    private readonly ProdutoContext _context;
    private readonly IMapper _mapper;
    private readonly IImgurService _imgurService;
    private readonly IFakeStoreAPIService _fakeStoreAPIService;

    public ProdutoService(ProdutoContext context, IMapper mapper, IImgurService imgurService, IFakeStoreAPIService fakeStoreApiService)
    {
        _context = context;
        _mapper = mapper;
        _imgurService = imgurService;
        _fakeStoreAPIService = fakeStoreApiService;
    }
    
    public async Task<(List<GetProdutoDto> Produtos, int TotalItems)> BuscarProdutosAsync(string? nome, string? codigo, int skip, int take)
    {
        try
        {
            var query = _context.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(w => w.Title.Contains(nome));
            }

            if (!string.IsNullOrEmpty(codigo))
            {
                query = query.Where(w => w.Description.Contains(codigo));
            }

            var totalItems = await query.CountAsync();
            var produtos = await query
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var retornoProdutos = _mapper.Map<List<GetProdutoDto>>(produtos);

            return (retornoProdutos, totalItems);
        }
        catch (Exception ex)
        {
            // Log exception (consider using a logging framework)
            Console.WriteLine(ex);
            throw new InvalidOperationException("Ocorreu um erro ao buscar os produtos.", ex);
        }
    }

    public async Task<Produto> AdicionarProdutoAsync(CreateProdutoDto produtoDto)
    {
        if (produtoDto == null)
        {
            throw new ArgumentNullException(nameof(produtoDto));
        }

        Produto produto = _mapper.Map<Produto>(produtoDto);

        try
        {
            var imagemUrl = await _imgurService.UploadImageAsync(produtoDto.Image);
            produto.Image = imagemUrl;

            _context.Produtos.Add(produto);
            
            await _context.SaveChangesAsync();
            
            FakeStoreProductDTO productDto = _mapper.Map<FakeStoreProductDTO>(produto);
            await _fakeStoreAPIService.CreateProductAsync(productDto);
            
            return produto;
        }
        catch (Exception ex)
        {
            // Log exception (consider using a logging framework)
            Console.WriteLine(ex);
            throw new InvalidOperationException("Ocorreu um erro ao adicionar o produto.", ex);
        }
    }
    
    public async Task<bool> EditarProdutoAsync(int id, UpdateProdutoDto produtoDto)
    {
        if (produtoDto == null)
        {
            throw new ArgumentNullException(nameof(produtoDto));
        }

        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

        if (produto == null)
        {
            return false; // Produto não encontrado
        }

        var imagemAnterior = produto.Image;
        _mapper.Map(produtoDto, produto);

        if (produtoDto.Imagem != null)
        {
            try
            {
                var imagemUrl = await _imgurService.UploadImageAsync(produtoDto.Imagem);
                produto.Image = imagemUrl;
            }
            catch (Exception ex)
            {
                // Log exception (consider using a logging framework)
                Console.WriteLine(ex);
                // Optional: handle the exception as needed
                produto.Image = imagemAnterior; // Reverte a imagem se o upload falhar
            }
        }
        else
        {
            produto.Image = imagemAnterior;
        }

        try
        {
            await _context.SaveChangesAsync();
            
            FakeStoreProductDTO productDto = _mapper.Map<FakeStoreProductDTO>(produto);
            await _fakeStoreAPIService.UpdateProductAsync(id, productDto);
            return true;
        }
        catch (Exception ex)
        {
            // Log exception (consider using a logging framework)
            Console.WriteLine(ex);
            throw new InvalidOperationException("Ocorreu um erro ao salvar as alterações.", ex);
        }
    }
    
    public async Task<bool> ExcluirProdutoAsync(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

        if (produto == null)
        {
            return false; // Produto não encontrado
        }

        try
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            
            await _fakeStoreAPIService.DeleteProductAsync(id);
            
            return true;
        }
        catch (Exception ex)
        {
            // Log exception (consider using a logging framework)
            Console.WriteLine(ex);
            throw new InvalidOperationException("Ocorreu um erro ao excluir o produto.", ex);
        }
    }
}