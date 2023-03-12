using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Domain.Entities;

namespace DesafioSistemaTarefas.Test.Mocks.Objects
{
    public class MockHistoricoTarefa
    {
        public readonly IEnumerable<HistoricoTarefa> listHistoricoTarefaMockValido = new List<HistoricoTarefa>
            {
                new HistoricoTarefa(1, 6, DateTime.Now, null,"Teste 1", "Teste Historico 1",DateTime.Now.AddDays(1), 2),
                new HistoricoTarefa(2, 7, null,DateTime.Now,"Teste 2", "Teste Historico 2",DateTime.Now.AddDays(2), 3),
                new HistoricoTarefa(3, 8, DateTime.Now,null,"Teste 3", "Teste Historico 3",DateTime.Now.AddDays(3), 2),
                new HistoricoTarefa(4, 9, null,DateTime.Now,"Teste 4", "Teste Historico 4",DateTime.Now.AddDays(4), 3),
                new HistoricoTarefa(5, 10, DateTime.Now, null,"Teste 5",  "Teste Historico 5",DateTime.Now.AddDays(5), 2)
            };

        public readonly HistoricoTarefa historicoTarefaMockValido = new HistoricoTarefa(1, 6, DateTime.Now, null, "Teste 1", "Teste Historico 1", DateTime.Now.AddDays(1), 2);

        public IEnumerable<HistoricoTarefaDto> listHistoricoTarefaDtoMock;
        public HistoricoTarefaDto historicoTarefaDtoMock;

    }
}
