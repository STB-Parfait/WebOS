using Backend.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Backend.Database;

namespace Backend.Services;

public class UserService
{
    private readonly LocalDbContext _db;

    public UserService(LocalDbContext _db){ this._db = _db; }

    public async Task<User> Create(UserDTO dto)
    {
        var exists = await _db.users.AnyAsync(u => u.email == dto.email);

        if(exists)
        {
            throw new InvalidOperationException("Email already in use");
        }

        var newUser = new User(dto.username, dto.email, dto.password);

        _db.users.Add(newUser);
        await _db.SaveChangesAsync();

        return newUser;
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

        user.updateUserData(dto.username, dto.password);

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

    public async Task<User?> ValidarLogin(string email, string password)
        {
            var user = await _db.users
            .FirstOrDefaultAsync(u => u.email == email);

            if (user is null) return null;

            // 3. Verifica a senha
            // ATENÇÃO: Como estamos aprendendo, estamos comparando texto puro.
            // Em produção, aqui você usaria BCrypt.Verify(password, user.PasswordHash)
            if (user.password != password) return null;

            return user;
        }

    public async Task<bool> CheckEmail(string email)
    {
        return await _db.users.AnyAsync(u => u.email == email);
    }
}