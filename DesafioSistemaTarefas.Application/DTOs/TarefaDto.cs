using System.Diagnostics.CodeAnalysis;

namespace DesafioSistemaTarefas.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class TarefaDto : BaseDto
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public DateTime dataHoraTarefa { get; set; }
        public int idStatusTarefa { get; set; }
        public string status { get; set; }
    }
}
