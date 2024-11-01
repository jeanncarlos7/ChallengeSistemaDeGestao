﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SistemaDeGestao.Models
{
    public class ClienteInsertModel
    {
        [BsonElement("Nome")]
        public string Nome { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }
    }
}