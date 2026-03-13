namespace GestaoOscAPI.Models.Requests
{
    public class CriarOscRequest 
    {
        public string Descricao { get; set; } = string.Empty;
        public string Equipamento { get; set; } = string.Empty;
        public int GerenteQualidadeId { get; set; }
        public int GerenteEngenhariaId { get; set; }
        public int GerenteProducaoId { get; set; }
        public int UsuarioLogadoId { get; set; }

    }
}

