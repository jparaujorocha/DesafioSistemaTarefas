using DesafioSistemaTarefas.Application.DTOs;

namespace DesafioSistemaTarefas.Application.Interfaces
{
    public interface ITarefaService
    {
        Task<TarefaDto> BuscarTarefa(int idTarefa);
        Task<IEnumerable<TarefaDto>> BuscarTarefas();
        Task<TarefaDto> InserirTarefa(TarefaDto dadosTarefa);
        Task<bool> ExcluirTarefa(int idTarefa);
        Task<IEnumerable<TarefaDto>> BuscarTarefasPorPeriodo(DateTime dataInicial, DateTime dataFinal);
        Task<TarefaDto> AtualizarTarefa(TarefaDto dadosTarefa);
        Task<bool> ConcluirTarefa(int idTarefa);
        Task<TarefaDto> ReativarTarefa(int idTarefa);
    }
}
