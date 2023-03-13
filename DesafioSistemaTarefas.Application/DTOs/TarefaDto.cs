using System.Diagnostics.CodeAnalysis;

namespace DesafioSistemaTarefas.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class TarefaDto : BaseDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraTarefa { get; set; }
        public int IdStatusTarefa { get; set; }
        public string Status { get; set; }
    }
}
