using GestaoOscAPI.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoOscAPI.Models.Entities
{
    public class Osc
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public bool QualidadeAssinou { get; set; } = false;
        public bool EngenhariaAssinou { get; set; } = false;
        public bool ProducaoAssinou { get; set; } = false;
    }
}
