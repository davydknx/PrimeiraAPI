using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.AspNetCore.Http; // Certifique-se de importar o namespace IFormFile

namespace PrimeiraAPI.Entities
{
    public class Customer
    {
        public Customer() // Adicionando um construtor sem parâmetros
        {
        }

        public Customer(string email, IFormFile photo) : this() // Removendo o filePath desnecessário
        {
            Email = email;
            Photo = photo;
        }

        public Customer(string customerName, string email, IFormFile photo, string filePath)
        {
            CustomerName = customerName;
            Email = email;
            Photo = photo;
            FilePath = filePath;
        }

        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("customer_name"), BsonRepresentation(BsonType.String)]
        public string? CustomerName { get; set; }

        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string? Email { get; set; }

        [BsonIgnore] // Ignorando este campo ao serializar/deserializar com o MongoDB
        public IFormFile Photo { get; set; }
        public string? FilePath { get; }
    }
}
