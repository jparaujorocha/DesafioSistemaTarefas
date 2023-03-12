using DesafioSistemaTarefas.Domain.Entities;
using DesafioSistemaTarefas.Domain.Inferfaces.Base;

namespace DesafioSistemaTarefas.Domain.Inferfaces
{
    public interface ITarefaRepository : IBaseRepository<Tarefa>
    {
        Task<IEnumerable<Tarefa>> GetTarefasByPeriodo(DateTime DataInicial, DateTime DataFinal);
    }
}
