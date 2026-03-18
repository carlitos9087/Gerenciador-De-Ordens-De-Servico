using GestaoOscAPI.Models.Entities;
using GestaoOscAPI.Models.Enums;

namespace GestaoOscAPI.Models.Responses
{
    public class OscResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Equipamento { get; set; } = string.Empty;
        public string AcaoTomada {  get; set; } = string.Empty;
        public DateTime DataEmissao { get; set; }
        public StatusOsc Status { get; set; }
        public int EmitenteId { get; set; }
        public string EmitenteNome { get; set; } = string.Empty;
        public string EmitenteSetor { get; set; } = string.Empty;
        public bool PrecisaQualidade { get; set; } = false;
        public bool PrecisaEngenharia { get; set; } = false;
        public bool PrecisaProducao { get; set; } = false;
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
                AcaoTomada = osc.AcaoTomada,
                Status = osc.Status,
                EmitenteId = osc.EmitenteId,
                EmitenteNome = osc.EmitenteNome,
                EmitenteSetor = osc.EmitenteSetor,
                PrecisaQualidade = osc.PrecisaQualidade,
                PrecisaEngenharia = osc.PrecisaEngenharia,
                PrecisaProducao = osc.PrecisaProducao,
                QualidadeAssinou = osc.QualidadeAssinou,
                EngenhariaAssinou = osc.EngenhariaAssinou,
                ProducaoAssinou = osc.ProducaoAssinou

            };
        }
    }
}
