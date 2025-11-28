using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace Backend.Models;

public class User
{
    public ObjectId Id { get; private set; }
    public string username { get; private set; }
    public string email { get; private set; }
    public string password { get; private set; }

    public User(string username, string email, string password)
    {
        if(!string.IsNullOrWhiteSpace(username))
        {
            this.username = username;
        }
        if(!string.IsNullOrWhiteSpace(email))
        {
            this.email = email;
        }
        if(!string.IsNullOrWhiteSpace(password))
        {
            this.password = password;
        }
    }

    public void updateUserData(string username, string password)
    {
        if(!string.IsNullOrEmpty(username))
        {
            this.username = username;
        }
        if(!string.IsNullOrEmpty(password))
        {
            this.password = password;
        }
    }
}