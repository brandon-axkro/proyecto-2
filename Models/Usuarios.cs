using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class Usuario
{
    [BsonId, BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("Nombre")]
    public string Nombre { get; set; } = string.Empty;
    
    [BsonElement("Nombre")]
    public string Password { get; set; } = string.Empty;

    [BsonElement("Nombre")]
    public string Correo { get; set; } = string.Empty;
}