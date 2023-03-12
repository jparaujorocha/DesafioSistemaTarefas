using DesafioSistemaTarefas.Application.DTOs;

namespace DesafioSistemaTarefas.Application.Interfaces
{
    public interface ITarefaService
    {
        TarefaDto BuscarTarefa(int idTarefa);
        IEnumerable<TarefaDto> BuscarTarefas();
        IEnumerable<TarefaDto> BuscarTarefasPorPeriodo(DateTime dataInicial, DateTime dataFinal);
        TarefaDto InserirTarefa(TarefaDto dadosTarefa);
        TarefaDto AtualizarTarefa(TarefaDto dadosTarefa);
        void ExcluirTarefa(int idTarefa);
        void ConcluirTarefa(int idTarefa);
        TarefaDto ReativarTarefa(int idTarefa);
    }
}
