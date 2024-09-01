using AlphaAPI.Data;
using AlphaAPI.Data.DTOs;
using AlphaAPI.Models;
using AlphaAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AlphaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(ProdutoContext context, IMapper mapper, IImgurService imgurService, IProdutoService produtoService, IFakeStoreAPIService fakeStoreApiService)
    {
        _produtoService = produtoService;
    }
    /// <summary>
    /// Busca produtos com base nos parâmetros fornecidos.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite buscar produtos por nome ou código de barras. É possível realizar paginação usando os parâmetros 'skip' e 'take'.
    /// </remarks>
    /// <param name="nome">Nome parcial ou completo do produto (opcional).</param>
    /// <param name="codigo">Código de barras parcial ou completo do produto (opcional).</param>
    /// <param name="skip">Número de itens a serem ignorados na paginação (padrão: 0).</param>
    /// <param name="take">Número de itens a serem retornados na paginação (padrão: 10).</param>
    /// <returns>Uma lista de produtos e o número total de itens.</returns>
    /// <response code="200">Retorna a lista de produtos e o número total de itens.</response>
    /// <response code="500">Se ocorrer um erro no servidor.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> BuscarProdutos([FromQuery] string? nome = null, [FromQuery] string? codigo = null, [FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var (produtos, totalItems) = await _produtoService.BuscarProdutosAsync(nome, codigo, skip, take);

            var retorno = new
            {
                Produtos = produtos,
                TotalItems = totalItems
            };

            return Ok(retorno);
        }
        catch (InvalidOperationException ex)
        {
            // Tratar exceções específicas para erros de operação
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        catch (Exception ex)
        {
            // Tratar outras exceções
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao buscar os produtos.");
        }
    }
    
    /// <summary>
    /// Adiciona um novo produto ao sistema.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite a adição de um novo produto ao sistema. O produto deve ser fornecido no corpo da requisição.
    /// </remarks>
    /// <param name="produtoDto">Objeto contendo as informações do produto a ser adicionado.</param>
    /// <returns>Retorna um status code indicando o resultado da operação.</returns>
    /// <response code="204">Produto adicionado com sucesso, mas sem conteúdo retornado.</response>
    /// <response code="400">Se os dados fornecidos são inválidos.</response>
    /// <response code="500">Se ocorrer um erro no servidor.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> AdicionarProduto([FromBody] CreateProdutoDto produtoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var produto = await _produtoService.AdicionarProdutoAsync(produtoDto);
            return NoContent(); // Retorna NoContent após a criação bem-sucedida
        }
        catch (InvalidOperationException ex)
        {
            // Tratar exceções específicas para erros de operação
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        catch (Exception ex)
        {
            // Tratar outras exceções
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao adicionar o produto.");
        }
    }

    /// <summary>
    /// Edita um produto existente no sistema.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite a edição das informações de um produto existente. O produto é identificado pelo ID fornecido na URL.
    /// </remarks>
    /// <param name="id">Identificador único do produto a ser editado.</param>
    /// <param name="produtoDto">Objeto contendo as novas informações do produto.</param>
    /// <returns>Retorna um status code indicando o resultado da operação.</returns>
    /// <response code="204">Produto editado com sucesso, mas sem conteúdo retornado.</response>
    /// <response code="400">Se os dados fornecidos são inválidos.</response>
    /// <response code="404">Se o produto com o ID fornecido não for encontrado.</response>
    /// <response code="500">Se ocorrer um erro no servidor.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> EditarProduto(int id, [FromBody] UpdateProdutoDto produtoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            bool resultado = await _produtoService.EditarProdutoAsync(id, produtoDto);

            if (!resultado)
            {
                return NotFound(); // Produto não encontrado
            }

            return NoContent(); // Atualização bem-sucedida
        }
        catch (InvalidOperationException ex)
        {
            // Tratar exceções específicas para erros de operação
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        catch (Exception ex)
        {
            // Tratar outras exceções
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao editar o produto.");
        }
    }

    /// <summary>
    /// Exclui um produto existente do sistema.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite a exclusão de um produto identificado pelo ID fornecido na URL.
    /// </remarks>
    /// <param name="id">Identificador único do produto a ser excluído.</param>
    /// <returns>Retorna um status code indicando o resultado da operação.</returns>
    /// <response code="204">Produto excluído com sucesso, mas sem conteúdo retornado.</response>
    /// <response code="400">Se os dados fornecidos são inválidos.</response>
    /// <response code="404">Se o produto com o ID fornecido não for encontrado.</response>
    /// <response code="500">Se ocorrer um erro no servidor.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> ExcluirProduto(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            bool resultado = await _produtoService.ExcluirProdutoAsync(id);

            if (!resultado)
            {
                return NotFound(); // Produto não encontrado
            }

            return NoContent(); // Exclusão bem-sucedida
        }
        catch (InvalidOperationException ex)
        {
            // Tratar exceções específicas para erros de operação
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        catch (Exception ex)
        {
            // Tratar outras exceções
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao excluir o produto.");
        }
    }
}