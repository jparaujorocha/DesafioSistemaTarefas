namespace DesafioSistemaTarefas.Application.DTOs
{
    public class TarefaDto : BaseDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraTarefa { get; set; }
        public int IdStatusTarefa { get; set; }
        public string StatusTarefa { get; set; }
    }
}
