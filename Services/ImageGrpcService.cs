using Grpc.Core;
using Tienda.Autor.Api.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Tienda.Autor.Api.Models;


namespace Tienda.Autor.Api.Services
{
    public class ImageGrpcService : imageservice.ImageService.ImageServiceBase
    {
        private readonly ILogger<ImageGrpcService> _logger;
        private readonly ImageService _imageService;

        public ImageGrpcService(ILogger<ImageGrpcService> logger, ImageService imageService)
        {
            _logger = logger;
            _imageService = imageService;
        }

        public override async Task<SaveImageResponse> SaveImage(SaveImageRequest request, ServerCallContext context)
        {
            try
            {
                var image = new Image
                {
                    Guid = request.Guid,
                    Name = request.Name,
                    Data = request.Image.ToByteArray()
                };

                await _imageService.SaveImageAsync(image);

                return new SaveImageResponse { Success = true, Message = "Image saved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving image");
                return new SaveImageResponse { Success = false, Message = "Error saving image" };
            }
        }
    }
}
