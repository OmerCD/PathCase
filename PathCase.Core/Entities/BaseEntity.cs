using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PathCase.Core.Entities
{
    public abstract class BaseEntity
    {
        [BsonId] public ObjectId Id { get; set; }
    }
}