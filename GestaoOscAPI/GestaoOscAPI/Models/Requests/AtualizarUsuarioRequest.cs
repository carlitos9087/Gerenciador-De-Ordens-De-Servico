using GestaoOscAPI.Models.Enums;

namespace GestaoOscAPI.Models.Requests
{
    public class AtualizarUsuarioRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public PerfilUsuario Perfil { get; set; }
        public Setor Setor { get; set; }
    }
}
