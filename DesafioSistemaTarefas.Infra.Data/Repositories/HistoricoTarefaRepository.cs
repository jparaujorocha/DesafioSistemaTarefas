using DesafioSistemaTarefas.Domain.Entities;
using DesafioSistemaTarefas.Domain.Inferfaces;
using DesafioSistemaTarefas.Infra.Data.Context;
using DesafioSistemaTarefas.Infra.Data.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace DesafioSistemaTarefas.Infra.Data.Repositories
{
    public class HistoricoTarefaRepository : BaseRepository<HistoricoTarefa>, IHistoricoTarefaRepository
    {
        public HistoricoTarefaRepository(ApplicationDbContext historicoTarefaDbContext, ILogger<BaseRepository<HistoricoTarefa>> logger) : base(historicoTarefaDbContext, logger)
        {
        }
    }
}
