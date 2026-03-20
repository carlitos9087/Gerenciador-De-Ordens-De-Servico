namespace GestaoOscAPI.Models.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public UsuarioResponse Usuario { get; set; } = new UsuarioResponse();
    }
}
