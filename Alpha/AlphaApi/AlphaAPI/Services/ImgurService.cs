namespace AlphaAPI.Services;

public class ImgurService : IImgurService
{
    private static readonly string ClientId = "ad78c71461397f5"; 

    public async Task<string> UploadImageAsync(byte[] imageBytes)
    {
        using var client = new HttpClient();
        using var form = new MultipartFormDataContent();

        // Adicionar a imagem ao conteúdo do formulário
        var imageContent = new ByteArrayContent(imageBytes);
        imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
        form.Add(imageContent, "image", "image.jpg"); // Nome do campo e nome do arquivo

        // Adicionar o Client ID no cabeçalho de autorização
        client.DefaultRequestHeaders.Add("Authorization", $"Client-ID {ClientId}");

        // Adicionar a URL da API do Imgur
        var url = "https://api.imgur.com/3/upload";

        // Enviar a solicitação para o Imgur
        var response = await client.PostAsync(url, form);
        response.EnsureSuccessStatusCode();

        // Ler a resposta JSON
        var responseString = await response.Content.ReadAsStringAsync();
        dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

        // Retornar a URL da imagem carregada
        return result.data.link;
    }
}