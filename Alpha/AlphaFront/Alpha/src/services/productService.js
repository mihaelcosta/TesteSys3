import httpClient from './api';

export async function getDadosApi(first, rows) {
  try {
    // Realiza a requisição HTTP
    const response = await httpClient.get(
      `/Produto?skip=${first}&take=${rows}`,
    );

    // Processa a resposta e retorna os dados
    return response;
  } catch (error) {
    // Trata erros de requisição
    console.error('Erro ao obter dados:', error);
    throw error; // Propaga o erro para que o chamador possa tratá-lo
  }
}

export async function realizaFiltroApi(filtroNome, filtroCodigo, rows) {
  try {
    const response = await httpClient.get('/Produto', {
      params: {
        nome: filtroNome,
        codigo: filtroCodigo ?? '', // Use uma string vazia se filtroCodigo for null ou undefined
        skip: 0,
        take: rows,
      },
    });
    return response.data; // Retorna a resposta para ser usada no componente
  } catch (error) {
    console.error('Erro ao realizar filtro:', error);
    throw error; // Re-throws the error to be handled by the caller
  }
}

export async function criarProdutoApi(novoProduto) {
  try {
    // Realiza a requisição POST
    const response = await httpClient.post('/Produto', novoProduto);

    // Verifica o status da resposta
    if (response.status === 204) {
      return true; // Indica que a operação foi bem-sucedida
    }
    return false; // Caso o status não seja 204
  } catch (error) {
    // Trata erros de requisição
    console.error('Erro ao criar produto:', error);
    throw error; // Propaga o erro para que o chamador possa tratá-lo
  }
}

export async function atualizarProdutoApi(id, produto) {
  try {
    // Realiza a requisição PUT
    const response = await httpClient.put(`/Produto/${id}`, {
      Title: produto.title,
      Price: produto.price,
      Image: produto.image instanceof Array ? produto.image : null,
      Description: produto.description,
    });

    // Retorna a resposta ou um indicador de sucesso
    return response;
  } catch (error) {
    // Trata erros de requisição
    console.error('Erro ao atualizar produto:', error);
    throw error; // Propaga o erro para que o chamador possa tratá-lo
  }
}

export async function excluirProdutoApi(id) {
  try {
    // Realiza a requisição DELETE
    const response = await httpClient.delete(`Produto/${id}`);

    // Verifica o status da resposta
    if (response.status === 204) {
      return true; // Indica que a exclusão foi bem-sucedida
    }
    return false; // Caso o status não seja 204
  } catch (error) {
    // Trata erros de requisição
    console.error('Erro ao excluir produto:', error);
    throw error; // Propaga o erro para que o chamador possa tratá-lo
  }
}
