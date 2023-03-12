using DesafioSistemaTarefas.Application.DTOs;

namespace DesafioSistemaTarefas.Application.Interfaces
{
    public interface IHistoricoTarefaService
    {
        HistoricoTarefaDto BuscarPorId(int idHistoricoTarefa);
        HistoricoTarefaDto BuscarPorIdTarefa(int idTarefa);
        IEnumerable<HistoricoTarefaDto> BuscarHistoricoTarefas();
        HistoricoTarefaDto InserirHistoricoTarefa(HistoricoTarefaDto dadosTarefa);
        void ExcluirHistoricoTarefa(int idHistoricoTarefa);
    }
}
