namespace AlphaAPI.Services;

public interface IImgurService
{
    Task<string> UploadImageAsync(byte[] imageBytes);
}