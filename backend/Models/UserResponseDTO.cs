using MongoDB.Bson;

public class UserResponseDTO
{
    public string Id { get; set; }
    public string username { get; set; }
    public string email { get; set; }

    public UserResponseDTO(ObjectId id,string username, string email)
    {
        Id = id.ToString();
        this.username = username;
        this.email = email;
    }
}