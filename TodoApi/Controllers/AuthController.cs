using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly AuthService _authService = new();
    private readonly JwtService _jwtService = new();
    private readonly TodoContext _context;

    public AuthController(TodoContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public IActionResult Register(string email, string password)
    {
        // Verificar si el usuario ya existe
        if (_context.Usuarios.Any(u => u.Email == email))
        {
            return BadRequest("El usuario ya existe");
        }

        var hash = _authService.HashPassword(password);
        var usuario = new Usuario { Email = email, PasswordHash = hash };

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        return Ok("Usuario registrado exitosamente");
    }

    [HttpPost("login")]
    public IActionResult Login(string email, string password)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);
        if (usuario == null || !_authService.VerifyPassword(password, usuario.PasswordHash))
            return Unauthorized("Credenciales inválidas");

        var token = _jwtService.GenerateToken(email);
        return Ok(new { Token = token });
    }
  
}