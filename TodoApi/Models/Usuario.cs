public class Usuario
{
    public int ID{ get; set; }
    public string Email{ get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}