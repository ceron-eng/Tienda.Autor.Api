using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Tienda.Autor.Api.Models
{
    public class Image
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Guid { get; set; } // Nuevo campo para almacenar el GUID

        public string Name { get; set; }

        public byte[] Data { get; set; }
    }
}
