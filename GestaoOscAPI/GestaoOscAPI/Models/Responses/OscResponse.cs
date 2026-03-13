using GestaoOscAPI.Models.Entities;
using GestaoOscAPI.Models.Enums;

namespace GestaoOscAPI.Models.Responses
{
    public class OscResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Equipamento { get; set; } = string.Empty;
        public DateTime DataEmissao { get; set; }
        public StatusOsc Status { get; set; }
        public int EmitenteId { get; set; }
        public string EmitenteNome { get; set; } = string.Empty;
        public string EmitenteSetor { get; set; } = string.Empty;
        public UsuarioResponse? GerenteQualidade { get; set; }
        public UsuarioResponse? GerenteEngenharia { get; set; }
        public UsuarioResponse? GerenteProducao { get; set; }
        public bool QualidadeAssinou { get; set; }
        public bool EngenhariaAssinou { get; set; }
        public bool ProducaoAssinou { get; set; }

        public static OscResponse FromOsc(Osc osc)
        {


            return new OscResponse
            {
                Id = osc.Id,
                Descricao = osc.Descricao,
                Equipamento = osc.Equipamento,
                DataEmissao = osc.DataEmissao,
                Status = osc.Status,
                EmitenteId = osc.EmitenteId,
                EmitenteNome = osc.EmitenteNome,
                EmitenteSetor = osc.EmitenteSetor,
                GerenteQualidade = osc.GerenteQualidade != null ? UsuarioResponse.FromUsuario(osc.GerenteQualidade) : null,
                GerenteEngenharia = osc.GerenteEngenharia != null ? UsuarioResponse.FromUsuario(osc.GerenteEngenharia) : null,
                GerenteProducao = osc.GerenteProducao != null ? UsuarioResponse.FromUsuario(osc.GerenteProducao) : null,
                QualidadeAssinou = osc.QualidadeAssinou,
                EngenhariaAssinou = osc.EngenhariaAssinou,
                ProducaoAssinou = osc.ProducaoAssinou

            };
        }
    }
}
