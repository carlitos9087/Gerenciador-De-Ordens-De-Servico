using GestaoOscAPI.Repositories;

using GestaoOscAPI.Models;

namespace GestaoOscAPI.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository usuarioRepository;

        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public Usuario? ValidarLogin(string email, string senha)
        {
            Usuario? user = usuarioRepository.BuscarPorEmail(email);

            if (user == null || user.Senha != senha)
            {
                return null;
            }
            return user;
            
        }

        public List<Usuario> BuscarPorSetor(Setor setor)
        {
            return usuarioRepository.BuscarPorSetor(setor);
        }
    }
}
