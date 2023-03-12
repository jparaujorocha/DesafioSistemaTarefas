using DesafioSistemaTarefas.Domain.Entities;
using DesafioSistemaTarefas.Domain.Inferfaces;
using DesafioSistemaTarefas.Infra.Data.Context;
using DesafioSistemaTarefas.Infra.Data.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace DesafioSistemaTarefas.Infra.Data.Repositories
{
    public class TarefaRepository : BaseRepository<Tarefa>, ITarefaRepository
    {
        public TarefaRepository(ApplicationDbContext tarefaDbContext, ILogger<BaseRepository<Tarefa>> logger) : base(tarefaDbContext, logger)
        {
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasByPeriodo(DateTime DataInicial, DateTime DataFinal)
        {
            _logger.LogInformation("Start operation to Get Tarefas by Periodo");
            return await base.GetAll(a => a.DataHoraTarefa >= DataInicial && a.DataHoraTarefa <= DataFinal);
        }
    }
}
