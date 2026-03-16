using GestaoOscAPI.Data;
using GestaoOscAPI.Models.Entities;
using GestaoOscAPI.Models.Enums;

namespace GestaoOscAPI.Repositories
{
    public class UsuarioRepository
    {
        private readonly AppDbContext context;

        public UsuarioRepository(AppDbContext context) 
        {
            this.context = context;
        }

        public bool Adicionar(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            context.SaveChanges();
            return true;
        }

        public List<Usuario> ListarTodos()
        {
            return context.Usuarios.ToList();
        }

        public Usuario? BuscarPorId(int id)
        {
            return context.Usuarios.FirstOrDefault(usuario => usuario.Id == id);
        }

        public Usuario? BuscarPorEmail(string email)
        {
            return context.Usuarios.FirstOrDefault(usuario => usuario.Email.Equals(email));
        }

        public List<Usuario> BuscarPorSetor(Setor setor)
        {
            return context.Usuarios.Where(usuario => usuario.Setor == setor).ToList();
        }

        public bool Atualizar(Usuario usuario)
        {
            context.Usuarios.Update(usuario);
            context.SaveChanges();
            return true;
        }

        public bool Deletar(int id)
        {
            Usuario? usuario = context.Usuarios.FirstOrDefault(usuario => usuario.Id == id);

            if (usuario == null)
                return false;

            context.Usuarios.Remove(usuario);
            context.SaveChanges();
            return true;
        }
    }
}
