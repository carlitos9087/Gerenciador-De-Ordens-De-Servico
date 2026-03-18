namespace GestaoOscAPI.Models.Requests
{
    public class AtualizarOscRequest
    {
        public string Descricao { get; set; } = string.Empty;
        public string Equipamento { get; set; } = string.Empty;
        public string AcaoTomada { get; set; } = string.Empty;

    }
}
