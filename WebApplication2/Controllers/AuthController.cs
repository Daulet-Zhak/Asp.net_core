using Microsoft.AspNetCore.Mvc;
using WebApplication2.Application;
using WebApplication2.Domain;
using MongoDB.Driver;
using WebApplication2.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwt;
    private readonly IMongoCollection<User> _users;

    public AuthController(JwtService jwt, IConfiguration config, MongoDbService db)
    {
        _jwt = jwt;
        _users = db.Database.GetCollection<User>("users");
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        var userExists = await _users.Find(u => u.Username == dto.Username).FirstOrDefaultAsync();
        if (userExists != null) return BadRequest("Пользователь уже существует");

        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new User { Username = dto.Username, PasswordHash = hash };
        await _users.InsertOneAsync(user);
        return Ok("Пользователь зарегистрирован");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        var user = await _users.Find(u => u.Username == dto.Username).FirstOrDefaultAsync();
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Неверный логин или пароль");

        var token = _jwt.GenerateToken(user.Id);
        
        return Ok(new { token });
    }
}
