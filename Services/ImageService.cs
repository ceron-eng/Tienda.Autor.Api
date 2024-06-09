using Grpc.Core;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Tienda.Autor.Api.Models;

namespace Tienda.Autor.Api.Services
{
    public class ImageService
    {
        private readonly IMongoCollection<Image> _images;

        public ImageService(ImageDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _images = database.GetCollection<Image>(settings.ImagesCollectionName);
        }

        public async Task SaveImageAsync(Image image) =>
            await _images.InsertOneAsync(image);
    }

    public class ImageDatabaseSettings
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? ImagesCollectionName { get; set; }
    }
}

