using GestaoOscAPI.Data;
using GestaoOscAPI.Models.Entities;
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
            return context.Oscs
        .Include(o => o.GerenteQualidade)
        .Include(o => o.GerenteEngenharia)
        .Include(o => o.GerenteProducao)
        .ToList();
        }

        public Osc? BuscarPorId(int id)
        {
            return context.Oscs
                .Include(o => o.GerenteQualidade)
                .Include(o => o.GerenteEngenharia)
                .Include(o => o.GerenteProducao)
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
