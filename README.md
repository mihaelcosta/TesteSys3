# AlphaAPI Documentation

## Versão 1.0

### Informações Gerais

- **Título:** AlphaAPI
- **Versão:** v1

## Endpoints

### 1. **Buscar Produtos**

- **Método:** `GET`
- **Endpoint:** `/Produto`

#### Descrição

Busca produtos com base nos parâmetros fornecidos. É possível realizar paginação usando os parâmetros `skip` e `take`.

#### Parâmetros de Consulta

- **`nome`** (opcional)
  - Tipo: `string`
  - Descrição: Nome parcial ou completo do produto.

- **`codigo`** (opcional)
  - Tipo: `string`
  - Descrição: Código de barras parcial ou completo do produto.

- **`skip`** (opcional)
  - Tipo: `integer`
  - Formato: `int32`
  - Default: `0`
  - Descrição: Número de itens a serem ignorados na paginação.

- **`take`** (opcional)
  - Tipo: `integer`
  - Formato: `int32`
  - Default: `10`
  - Descrição: Número de itens a serem retornados na paginação.

#### Respostas

- **200 OK**
  - Descrição: Retorna a lista de produtos e o número total de itens.

- **500 Internal Server Error**
  - Descrição: Se ocorrer um erro no servidor.

### 2. **Adicionar Produto**

- **Método:** `POST`
- **Endpoint:** `/Produto`

#### Descrição

Adiciona um novo produto ao sistema. O produto deve ser fornecido no corpo da requisição.

#### Corpo da Requisição

- **Content-Type:** `application/json`, `application/json-patch+json`, `text/json`, `application/*+json`
- **Schema:** [CreateProdutoDto](#createprodutodto)

#### Respostas

- **204 No Content**
  - Descrição: Produto adicionado com sucesso, mas sem conteúdo retornado.

- **400 Bad Request**
  - Descrição: Se os dados fornecidos são inválidos.
  - Corpo: [ProblemDetails](#problemdetails)

- **500 Internal Server Error**
  - Descrição: Se ocorrer um erro no servidor.

### 3. **Editar Produto**

- **Método:** `PUT`
- **Endpoint:** `/Produto/{id}`

#### Descrição

Edita um produto existente no sistema. O produto é identificado pelo ID fornecido na URL.

#### Parâmetros de Caminho

- **`id`** (obrigatório)
  - Tipo: `integer`
  - Formato: `int32`
  - Descrição: Identificador único do produto a ser editado.

#### Corpo da Requisição

- **Content-Type:** `application/json`, `application/json-patch+json`, `text/json`, `application/*+json`
- **Schema:** [UpdateProdutoDto](#updateprodutodto)

#### Respostas

- **204 No Content**
  - Descrição: Produto editado com sucesso, mas sem conteúdo retornado.

- **400 Bad Request**
  - Descrição: Se os dados fornecidos são inválidos.
  - Corpo: [ProblemDetails](#problemdetails)

- **404 Not Found**
  - Descrição: Se o produto com o ID fornecido não for encontrado.
  - Corpo: [ProblemDetails](#problemdetails)

- **500 Internal Server Error**
  - Descrição: Se ocorrer um erro no servidor.

### 4. **Excluir Produto**

- **Método:** `DELETE`
- **Endpoint:** `/Produto/{id}`

#### Descrição

Exclui um produto existente do sistema. O produto é identificado pelo ID fornecido na URL.

#### Parâmetros de Caminho

- **`id`** (obrigatório)
  - Tipo: `integer`
  - Formato: `int32`
  - Descrição: Identificador único do produto a ser excluído.

#### Respostas

- **204 No Content**
  - Descrição: Produto excluído com sucesso, mas sem conteúdo retornado.

- **400 Bad Request**
  - Descrição: Se os dados fornecidos são inválidos.
  - Corpo: [ProblemDetails](#problemdetails)

- **404 Not Found**
  - Descrição: Se o produto com o ID fornecido não for encontrado.
  - Corpo: [ProblemDetails](#problemdetails)

- **500 Internal Server Error**
  - Descrição: Se ocorrer um erro no servidor.

## Schemas

### CreateProdutoDto

- **Descrição:** Objeto contendo as informações do produto a ser adicionado.
- **Tipo:** `object`
- **Propriedades:**
  - **title**
    - Tipo: `string`
    - Máximo: `100`
    - Mínimo: `0`
  - **price**
    - Tipo: `number`
    - Formato: `double`
    - Mínimo: `0.01`
  - **description**
    - Tipo: `string`
    - Máximo: `20`
    - Mínimo: `0`
  - **image**
    - Tipo: `string`
    - Formato: `byte`

### UpdateProdutoDto

- **Descrição:** Objeto contendo as novas informações do produto.
- **Tipo:** `object`
- **Propriedades:**
  - **title**
    - Tipo: `string`
    - Máximo: `100`
    - Mínimo: `0`
  - **price**
    - Tipo: `number`
    - Formato: `double`
    - Mínimo: `0.01`
  - **description**
    - Tipo: `string`
    - Máximo: `20`
    - Mínimo: `0`
  - **imagem**
    - Tipo: `string`
    - Formato: `byte`
    - Nullable: `true`

### ProblemDetails

- **Descrição:** Detalhes do problema retornado em caso de erro.
- **Tipo:** `object`
- **Propriedades:**
  - **type**
    - Tipo: `string`
    - Nullable: `true`
  - **title**
    - Tipo: `string`
    - Nullable: `true`
  - **status**
    - Tipo: `integer`
    - Formato: `int32`
    - Nullable: `true`
  - **detail**
    - Tipo: `string`
    - Nullable: `true`
  - **instance**
    - Tipo: `string`
    - Nullable: `true`
