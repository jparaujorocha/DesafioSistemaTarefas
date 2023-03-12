namespace DesafioSistemaTarefas.Application.DTOs
{
    public class HistoricoTarefaDto : TarefaDto
    {
        public int IdTarefa { get; set; }
        public DateTime? DataHoraExclusao { get; set; }
        public DateTime? DataHoraConclusao { get; set; }
    }
}
