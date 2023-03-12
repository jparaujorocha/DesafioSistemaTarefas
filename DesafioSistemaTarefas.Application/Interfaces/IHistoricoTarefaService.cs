using DesafioSistemaTarefas.Application.DTOs;

namespace DesafioSistemaTarefas.Application.Interfaces
{
    public interface IHistoricoTarefaService
    {
        Task<HistoricoTarefaDto> BuscarPorId(int idHistoricoTarefa);
        Task<HistoricoTarefaDto> BuscarPorIdTarefa(int idTarefa);
        Task<IEnumerable<HistoricoTarefaDto>> BuscarHistoricoTarefas();
        Task<HistoricoTarefaDto> InserirHistoricoTarefa(HistoricoTarefaDto dadosTarefa);
        Task<bool> ExcluirHistoricoTarefa(int idHistoricoTarefa);
    }
}
