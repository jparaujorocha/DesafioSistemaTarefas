using DesafioSistemaTarefas.Domain.Entities;
using DesafioSistemaTarefas.Domain.Inferfaces;
using Moq;
using System.Linq.Expressions;

namespace DesafioSistemaTarefas.Test.Mocks.Repositories
{
    public class MockTarefaRepository : Mock<ITarefaRepository>
    {
        public void MockGetAll(Task<IEnumerable<Tarefa>> output)
        {
            Setup(x => x.GetAll()).Returns(output);
        }
        public void MockGetById(int idTarefa, Task<Tarefa> output)
        {
            Setup(x => x.GetById(idTarefa)).Returns(output);
        }
        public void MockAdd(Tarefa dadosEntrada, Task<Tarefa> output)
        {
            Setup(x => x.Add(It.Is<Tarefa>(a => a.ValidateWithoutId()))).Returns(output);
        }
        public void MockDelete(int idTarefa, Task output)
        {
            Setup(x => x.Delete(It.Is<Tarefa>(a => a.Id == idTarefa))).Returns(output);
        }
        public void MockUpdate(Tarefa dadosEntrada, Task output)
        {
            Setup(x => x.Update(It.Is<Tarefa>(a => a.ValidateWithId()))).Returns(output);
        }
        public void MockGetTarefasByPeriodo(DateTime dataInicial, DateTime dataFinal, Task<IEnumerable<Tarefa>> output)
        {
            Setup(x => x.GetTarefasByPeriodo(It.Is<DateTime>(a => dataInicial >= a), It.Is<DateTime>(a => dataFinal <= a))).Returns(output);
        }

        public void MockGetAllByPredicate(Task<IEnumerable<Tarefa>> output)
        {
            Setup(x => x.GetAll(It.IsAny<Expression<Func<Tarefa, bool>>>())).Returns(output);
        }
    }
}
