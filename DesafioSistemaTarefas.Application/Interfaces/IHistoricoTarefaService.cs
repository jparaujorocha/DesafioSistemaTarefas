using DesafioSistemaTarefas.Application.DTOs;

namespace DesafioSistemaTarefas.Application.Interfaces
{
    public interface IHistoricoTarefaService
    {
        Task<HistoricoTarefaDto> BuscarPorId(int idHistoricoTarefa);
        Task<HistoricoTarefaDto> BuscarPorIdTarefa(int idTarefa);
        Task<IEnumerable<HistoricoTarefaDto>> BuscarLista();
        Task<HistoricoTarefaDto> Inserir(HistoricoTarefaDto dadosTarefa);
        Task<bool> Excluir(int idHistoricoTarefa);
    }
}
