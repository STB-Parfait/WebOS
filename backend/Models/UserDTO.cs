using MongoDB.Bson;

namespace Backend.Models;

public class UserDTO
{
    public ObjectId Id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}