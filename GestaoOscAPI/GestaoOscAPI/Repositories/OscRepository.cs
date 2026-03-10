using GestaoOscAPI.Models;

namespace GestaoOscAPI.Repositories
{
    public class OscRepository
    {
        private List<Osc> oscs;

        public OscRepository()
        {
            oscs = new List<Osc>();
        }

        public List<Osc> ListarTodas()
        {
            return oscs;
        }

        public Osc? BuscarPorId(int id)
        {
            return oscs.FirstOrDefault(osc => osc.Id == id);
        }

        public bool Adicionar(Osc osc)
        {
            oscs.Add(osc);
            return true;
        }

        public bool Atualizar(Osc osc)
        {
            var index = oscs.FindIndex(o => o.Id == osc.Id);

            if (index == -1)
            {
                return false;
            }

            oscs[index] = osc;
            return true;
        }




    }
}
