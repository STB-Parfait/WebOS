using MongoDB.Bson;

namespace Backend.Models;

public class UserDTO
{
    public ObjectId Id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }

    public UserDTO(string username, string email, string password)
    {
        this.username = username;
        this.email = email;
        this.password = password;
    }
}