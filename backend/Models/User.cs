using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace Backend.Models;

public class User
{
    public ObjectId Id { get; private set; }
    public string username { get; private set; }
    public string email { get; private set; }
    public string password { get; private set; }
}