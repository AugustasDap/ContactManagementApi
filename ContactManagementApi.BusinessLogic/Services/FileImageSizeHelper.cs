using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;


namespace ContactManagementApi.BusinessLogic.Services
{
    public static class FileImageSizeHelper
    {
        public static async Task SaveImageAsync(string filePath, IFormFile file, int width = 200, int height = 200)
        {
            using (var stream = file.OpenReadStream())
            {
                var image = await Image.LoadAsync<Rgba32>(stream);

                
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Crop,
                    Size = new Size(width, height)
                }));
                                
                await image.SaveAsync(filePath);
            }
        }
    }
}
