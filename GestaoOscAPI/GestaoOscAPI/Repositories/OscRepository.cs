using GestaoOscAPI.Data;
using GestaoOscAPI.Models.Entities;
using GestaoOscAPI.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace GestaoOscAPI.Repositories
{
    public class OscRepository
    {
        private readonly AppDbContext context;

        public OscRepository(AppDbContext context)
        {
            this.context = context;
        }

        public bool Adicionar(Osc osc)
        {
            context.Oscs.Add(osc);
            context.SaveChanges();
            return true;
        }

        public List<Osc> ListarTodas()
        {
            return context.Oscs.ToList();
        }

        public List<Osc> BuscarPorEmitente (int emitendeId)
        {
            return context.Oscs
                .Where(o => o.EmitenteId == emitendeId)
                .ToList();
        }

        public List<Osc> BuscarPorGerente(int usuarioId)
        {
            Usuario? usuario = context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
            if (usuario == null)
                return new List<Osc>();

            return context.Oscs
                .Where(o =>
                    o.Status == StatusOsc.AguardandoAssinaturas &&
                    ((usuario.Setor == Setor.Qualidade &&!o.QualidadeAssinou) ||
                    (usuario.Setor == Setor.Engenharia &&!o.EngenhariaAssinou) ||
                    (usuario.Setor == Setor.Producao && !o.ProducaoAssinou)))
                .ToList();
        }

        public Osc? BuscarPorId(int id)
        {
            return context.Oscs
                .FirstOrDefault(o => o.Id == id);
        }

        public bool Atualizar(Osc osc)
        {
            context.Oscs.Update(osc);
            context.SaveChanges();
            return true;
        }

        public bool Deletar(int id)
        {
            Osc? osc = context.Oscs.FirstOrDefault(osc => osc.Id == id);

            if (osc == null)
                return false;

            context.Oscs.Remove(osc);
            context.SaveChanges();
            return true;
        }


    }
}
