using GestaoOscAPI.Repositories;
using GestaoOscAPI.Models.Enums;
using GestaoOscAPI.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GestaoOscAPI.Services
{
    public class OscService
    {
        private readonly OscRepository oscRepository;
        private readonly UsuarioService usuarioService;

        public OscService(OscRepository oscRepository, UsuarioService usuarioService)
        {
            this.oscRepository = oscRepository;
            this.usuarioService = usuarioService;
        }

        public Osc CriarOsc(string descricao, string equipamento, string AcaoTomada, Usuario usuarioLogado)
        {
            Osc osc = new Osc 
            {
                Descricao = descricao,
                Equipamento = equipamento,
                AcaoTomada = AcaoTomada,
                DataEmissao = DateTime.UtcNow,
                EmitenteId = usuarioLogado.Id,
                EmitenteNome = usuarioLogado.Nome,
                EmitenteSetor = usuarioLogado.Setor.ToString()

            };

            oscRepository.Adicionar(osc);
            return osc;
        } 

        public List<Osc> ListarTodas()
        {
            return oscRepository.ListarTodas();
        }

        public Osc? BuscarPorId(int id)
        {
            return oscRepository.BuscarPorId(id);
        }

        public List<Osc> BuscarPorEmitente(int emitenteId)
        {
            return oscRepository.BuscarPorEmitente(emitenteId);
        }

        public List<Osc> BuscarPorGerente(int usuarioId)
        {
            return oscRepository.BuscarPorGerente(usuarioId); 
        }

        public bool Atualizar(Osc osc)
        {
            return oscRepository.Atualizar(osc);
        }

        public bool Concluir(int oscId, int usuarioId)
        {
            Usuario? usuario = usuarioService.BuscarPorId(usuarioId);
            Osc? osc = oscRepository.BuscarPorId(oscId);

            if (usuario == null || usuario.Perfil != PerfilUsuario.Administrador)
                return false;
            if (osc == null)
                return false;
            if (!osc.QualidadeAssinou || !osc.EngenhariaAssinou || !osc.ProducaoAssinou)
                return false;

            osc.Status = StatusOsc.Concluida;
            return oscRepository.Atualizar(osc);
        }

        public bool Cancelar(int oscId, int usuarioId)
        {
            Usuario? usuario = usuarioService.BuscarPorId(usuarioId);
            Osc? osc = oscRepository.BuscarPorId(oscId);

            if (usuario == null || usuario.Perfil != PerfilUsuario.Administrador)
                return false;

            if (osc == null)
                return false;

            osc.Status = StatusOsc.Cancelada;
            return oscRepository.Atualizar(osc);
        }



        public bool AssinarOSC (int oscId, int usuarioId)
        {
            Osc? osc = oscRepository.BuscarPorId(oscId);
            Usuario? usuario = usuarioService.BuscarPorId(usuarioId);

            if (osc == null || usuario == null)
                return false;

            if (usuario.Setor == Setor.Qualidade && !osc.QualidadeAssinou)
                osc.QualidadeAssinou = true;
            else if (usuario.Setor == Setor.Engenharia && !osc.EngenhariaAssinou)
                osc.EngenhariaAssinou = true;
            else if (usuario.Setor == Setor.Producao && !osc.ProducaoAssinou)
                osc.ProducaoAssinou = true;
            else
                return false;

            bool todasAssinadas = osc.QualidadeAssinou && osc.EngenhariaAssinou && osc.ProducaoAssinou;

            if (todasAssinadas)
                osc.Status = StatusOsc.AguardandoValidacao;

            oscRepository.Atualizar(osc);
            return true;

        }

        public bool Deletar (int id)
        {
            return oscRepository.Deletar(id);
        }
    }
}
