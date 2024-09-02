import httpClient from './api';

export async function getDadosApi(first, rows) {
  try {
    const response = await httpClient.get(
      `/Produto?skip=${first}&take=${rows}`,
      {
        params: {
          skip: 0,
          take: rows,
        },
      },
    );
    return response.data;
  } catch (error) {
    // Trata erros de requisição
    console.error('Erro ao obter dados:', error);
  }
}

export async function realizaFiltroApi(filtroNome, filtroCodigo, rows) {
  try {
    const response = await httpClient.get('/Produto', {
      params: {
        nome: filtroNome,
        codigo: filtroCodigo ?? '',
        skip: 0,
        take: rows,
      },
    });
    return response.data;
  } catch (error) {
    console.error('Erro ao realizar filtro:', error);
  }
}

export async function criarProdutoApi(novoProduto) {
  try {
    const response = await httpClient.post('/Produto', novoProduto);

    if (response.status === 204) {
      return true;
    }
    return false;
  } catch (error) {
    console.error('Erro ao criar produto:', error);
    throw error;
  }
}

export async function atualizarProdutoApi(id, produto) {
  try {
    const response = await httpClient.put(`/Produto/${id}`, {
      Title: produto.title,
      Price: produto.price,
      Image: produto.image instanceof Array ? produto.image : null,
      Description: produto.description,
    });

    return response;
  } catch (error) {
    console.error('Erro ao atualizar produto:', error);
  }
}

export async function excluirProdutoApi(id) {
  try {
    const response = await httpClient.delete(`Produto/${id}`);

    if (response.status === 204) {
      return true;
    }
    return false;
  } catch (error) {
    console.error('Erro ao excluir produto:', error);
  }
}
