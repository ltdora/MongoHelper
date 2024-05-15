using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace HelloWorldMongoDB.Models
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("username")]
        public string UserName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Passwords { get; set; }

        [BsonElement("created_at")]
        public DateTime CreateDate { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

    }
}
