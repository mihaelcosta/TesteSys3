# Alpha - Documentação do Projeto

---

## Tecnologias Utilizadas

- **Backend:** C# (.NET 6)
- **Banco de Dados:** SQL Server
- **Frontend:** Vue.js
- **Containerização:** Docker

---

## Pré-configuração

Para executar a aplicação sem problemas, é necessário ter o Docker instalado na máquina. Além disso, para rodar localmente, serão necessárias as seguintes ferramentas:

- **.NET SDK 6:** Para compilar e executar a aplicação backend.
- **Banco de Dados SQL Server:** Para armazenar os dados da aplicação.
- **NodeJS:** Para gerenciar dependências e construir o front-end.

---

## Instalação

Siga os passos abaixo para configurar e rodar a aplicação:

1. **Clone o repositório do projeto:**

   Use o comando `git clone` seguido do link do repositório para clonar o projeto.

2. **Acesse a pasta do projeto:**

   Navegue até a pasta do projeto usando o comando `cd` seguido do nome da pasta.

3. **Excute os comandos para instalar o .NET e SDK no projeto**
   
   No terminal, execute os comando `docker pull mcr.microsoft.com/dotnet/sdk:6.0` e `docker pull mcr.microsoft.com/dotnet/aspnet:6.0` para realizar a instalação do .NET e dp SDK.
    

4. **Execute o Docker Compose:**

   No terminal, execute o comando `docker-compose up -d` para criar e iniciar os containers necessários.

5. **Acesse as URLs:**

   Após a execução do Docker Compose, acesse as URLs correspondentes às portas configuradas no arquivo `docker-compose.yml` para interagir com a aplicação.

---

## Informações Gerais

A aplicação "Alpha" foi desenvolvida utilizando várias medidas de segurança para proteger contra vulnerabilidades comuns. Essas medidas incluem:

- **Entity Framework:** Para impedir SQL Injection, as consultas ao banco de dados são feitas utilizando Entity Framework, um ORM que abstrai e parametriza as queries SQL.

- **Data Annotations:** Validações de entrada são feitas utilizando Data Annotations, garantindo que os dados recebidos estejam no formato correto antes de serem processados.

- **CORS (Cross-Origin Resource Sharing):** A aplicação está configurada para permitir o compartilhamento de recursos entre diferentes origens, mas de forma controlada, para evitar ataques como Cross-Site Scripting (XSS).

### Cabeçalhos de Segurança

Além das medidas mencionadas, a aplicação configura os seguintes cabeçalhos de segurança nas respostas HTTP:

- **X-Content-Type-Options: nosniff:** Prevê que o navegador interprete os arquivos de forma forçada como sendo do tipo declarado, evitando que um arquivo com um MIME-type incorreto seja processado como um tipo diferente.
  
- **X-Frame-Options: DENY:** Impede que a página seja carregada em um frame ou iframe, prevenindo ataques de clickjacking.
  
- **X-XSS-Protection: 1; mode=block:** Ativa a proteção contra Cross-Site Scripting (XSS) nos navegadores que suportam essa funcionalidade.
  
- **Content-Security-Policy: default-src 'self':** Define uma política de segurança de conteúdo que permite apenas o carregamento de scripts, estilos e outros recursos da própria origem (`self`), prevenindo a execução de scripts de origens não confiáveis.

---

## Endpoints da API

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
- **Schema:** `CreateProdutoDto`

#### Respostas

- **204 No Content**
  - Descrição: Produto adicionado com sucesso, mas sem conteúdo retornado.

- **400 Bad Request**
  - Descrição: Se os dados fornecidos são inválidos.
  - Corpo: `ProblemDetails`

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
- **Schema:** `UpdateProdutoDto`

#### Respostas

- **204 No Content**
  - Descrição: Produto editado com sucesso, mas sem conteúdo retornado.

- **400 Bad Request**
  - Descrição: Se os dados fornecidos são inválidos.
  - Corpo: `ProblemDetails`

- **404 Not Found**
  - Descrição: Se o produto com o ID fornecido não for encontrado.
  - Corpo: `ProblemDetails`

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
  - Corpo: `ProblemDetails`

- **404 Not Found**
  - Descrição: Se o produto com o ID fornecido não for encontrado.
  - Corpo: `ProblemDetails`

- **500 Internal Server Error**
  - Descrição: Se ocorrer um erro no servidor.

---

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
