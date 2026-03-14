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

        public Usuario? GerenteQualidade { get; set; }
        public Usuario? GerenteEngenharia { get; set; }
        public Usuario? GerenteProducao { get; set; }

        public bool QualidadeAssinou { get; set; } = false;
        public bool EngenhariaAssinou { get; set; } = false;
        public bool ProducaoAssinou { get; set; } = false;
    }
}
