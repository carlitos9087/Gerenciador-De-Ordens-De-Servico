using GestaoOscAPI.Repositories;
using GestaoOscAPI.Models.Enums;
using GestaoOscAPI.Models.Entities;
using GestaoOscAPI.Models.Responses;

namespace GestaoOscAPI.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository usuarioRepository;

        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public Usuario CriarUsuario(string nome, string email, string senha, PerfilUsuario perfil, Setor setor)
        {
            Usuario usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                Senha = senha,
                Perfil = perfil,
                Setor = setor

            };

            usuarioRepository.Adicionar(usuario);
            return usuario;
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

        public List<Usuario> ListarTodos()
        {
            return usuarioRepository.ListarTodos();
        }
        public Usuario? BuscarPorId(int id)
        {
            return usuarioRepository.BuscarPorId(id);
        }

        public Usuario? BuscarPorEmail(string email)
        {
            return usuarioRepository.BuscarPorEmail(email);
        }

        public List<Usuario> BuscarPorSetor(Setor setor)
        {
            return usuarioRepository.BuscarPorSetor(setor);
        }

        public bool Atualizar(Usuario usuario)
        {
            return usuarioRepository.Atualizar(usuario);
        }

        public bool Deletar(int id)
        {
            return usuarioRepository.Deletar(id);
        }
    }
}
