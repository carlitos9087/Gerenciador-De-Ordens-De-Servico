namespace GestaoOscAPI.Models
{
    public class Osc
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Equipamento { get; set; } = string.Empty;
        public DateTime DataEmissao { get; set; }
        public StatusOsc Status { get; set; }

        // Emitente — preenchido automaticamente pelo usuário logado
        public int EmitenteId { get; set; }
        public string EmitenteNome { get; set; } = string.Empty;
        public string EmitenteSetor { get; set; } = string.Empty;

        // Gerentes escolhidos pelo emitente
        public Usuario? GerenteQualidade { get; set; }
        public Usuario? GerenteEngenharia { get; set; }
        public Usuario? GerenteProducao { get; set; }

        // Controle de quem já assinou
        public bool QualidadeAssinou { get; set; } = false;
        public bool EngenhariaAssinou { get; set; } = false;
        public bool ProducaoAssinou { get; set; } = false;
    }
}
