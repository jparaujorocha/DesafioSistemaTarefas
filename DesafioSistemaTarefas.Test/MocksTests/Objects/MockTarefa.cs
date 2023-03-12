using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Domain.Entities;

namespace DesafioSistemaTarefas.Test.Mocks.Objects
{
    public class MockTarefa
    {
        public readonly IEnumerable<Tarefa> listTarefaMockValido = new List<Tarefa>
            {
                new Tarefa(1, "Teste 1", "Teste Tarefa 1",DateTime.Now.AddDays(1), 2),
                new Tarefa(2, "Teste 2", "Teste Tarefa 2",DateTime.Now.AddDays(2), 3),
                new Tarefa(3, "Teste 3", "Teste Tarefa 3",DateTime.Now.AddDays(3), 2),
                new Tarefa(4, "Teste 4", "Teste Tarefa 4",DateTime.Now.AddDays(4), 3),
                new Tarefa(5, "Teste 5",  "Teste Tarefa 5",DateTime.Now.AddDays(5), 2)
            };

        public readonly Tarefa tarefaMockValido = new Tarefa(1, "Teste 1", "Teste Tarefa 1", DateTime.Now.AddDays(1), 2);

        public IEnumerable<TarefaDto> listTarefaDtoMock;
        public TarefaDto tarefaDtoMock;
    }
}
