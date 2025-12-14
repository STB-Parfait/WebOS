using Backend.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Backend.Database;

namespace Backend.Services;

public class UserService
{
    private readonly LocalDbContext _db;

    public UserService(LocalDbContext db){ this._db = db; }

    public async Task<UserResponseDTO> Create(UserDTO dto)
    {
        var exists = await _db.users.AnyAsync(u => u.email == dto.email);

        if(exists)
        {
            throw new InvalidOperationException("Email already in use");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.password);

        var newUser = new User(dto.username, dto.email, passwordHash);

        _db.users.Add(newUser);
        await _db.SaveChangesAsync();

        return new UserResponseDTO(newUser.Id, newUser.username, newUser.email);
    }

    public async Task<User?> GetById(string id)
    {
        if(!ObjectId.TryParse(id, out var objectId)) return null;

        return await _db.users.FindAsync(objectId);
    }

    public async Task<bool> Update(string id, UserDTO dto)
    {
        if (!ObjectId.TryParse(id, out var objectId)) return false;
        var user = await _db.users.FindAsync(objectId);
        if (user is null) return false;

        string newPass = user.password;

        if (!string.IsNullOrEmpty(dto.password))
        {
            newPass = BCrypt.Net.BCrypt.HashPassword(dto.password);
        }

        user.updateUserData(dto.username, newPass);

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId)) return false;

        var user = await _db.users.FindAsync(objectId);

        if (user is null) return false;

        _db.users.Remove(user);

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<UserResponseDTO?> ValidarLogin(string email, string password)
        {
            var user = await _db.users
            .FirstOrDefaultAsync(u => u.email == email);

            if (user is null) return null;

            bool validPassword = BCrypt.Net.BCrypt.Verify(password, user.password);

            if (!validPassword) return null;

            return new UserResponseDTO(user.Id, user.username, user.email);
        }

    public async Task<bool> CheckEmail(string email)
    {
        return await _db.users.AnyAsync(u => u.email == email);
    }
}