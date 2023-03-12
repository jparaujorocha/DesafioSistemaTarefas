using System.Diagnostics.CodeAnalysis;

namespace DesafioSistemaTarefas.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class HistoricoTarefaDto : TarefaDto
    {
        public int IdTarefa { get; set; }
        public DateTime? DataHoraExclusao { get; set; }
        public DateTime? DataHoraConclusao { get; set; }
    }
}
