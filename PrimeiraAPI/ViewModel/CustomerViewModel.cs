﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PrimeiraAPI.Model
{
    public class CustomerViewModel
    {

        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("customer_name"), BsonRepresentation(BsonType.String)]
        public string? CustomerName { get; set; }

        [BsonElement("email"), BsonRepresentation(BsonType.String)]

        public string? Email { get; set; }

        public IFormFile Photo { get; set; }


    }
}

