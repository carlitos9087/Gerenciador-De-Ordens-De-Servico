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
            return context.Oscs
        .Include(o => o.GerenteQualidade)
        .Include(o => o.GerenteEngenharia)
        .Include(o => o.GerenteProducao)
        .ToList();
        }

        public List<Osc> BuscarPorEmitente (int emitendeId)
        {
            return context.Oscs
                .Include(o => o.GerenteQualidade)
                .Include(o => o.GerenteEngenharia)
                .Include(o => o.GerenteProducao)
                .Where(o => o.EmitenteId == emitendeId)
                .ToList();
        }

        public List<Osc> BuscarPorGerente(int usuarioId)
        {
            return context.Oscs
                .Include(o => o.GerenteQualidade)
                .Include(o => o.GerenteEngenharia)
                .Include(o => o.GerenteProducao)
                .Where(o => o.GerenteQualidade!.Id == usuarioId ||
                            o.GerenteEngenharia!.Id == usuarioId ||
                            o.GerenteProducao!.Id == usuarioId)
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
