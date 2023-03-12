using DesafioSistemaTarefas.Domain.Entities;
using DesafioSistemaTarefas.Domain.Inferfaces;
using Moq;
using System.Linq.Expressions;

namespace DesafioSistemaTarefas.Test.Mocks.Repositories
{
    public class MockHistoricoTarefaRepository : Mock<IHistoricoTarefaRepository>
    {
        public void MockGetAll(Task<IEnumerable<HistoricoTarefa>> output)
        {
            Setup(x => x.GetAll()).Returns(output);
        }
        public void MockGetById(int idHistoricoTarefa, Task<HistoricoTarefa> output)
        {
            Setup(x => x.GetById(idHistoricoTarefa)).Returns(output);
        }
        public void MockGetByPredicate(Task<HistoricoTarefa> output)
        {
            Setup(x => x.Get(It.IsAny<Expression<Func<HistoricoTarefa, bool>>>())).Returns(output);
        }
        public void MockAdd(HistoricoTarefa dadosEntrada, Task<HistoricoTarefa> output)
        {
            Setup(x => x.Add(It.Is<HistoricoTarefa>(a => a.ValidateWithoutId()))).Returns(output);
        }
        public void MockDelete(int idHistoricoTarefa, Task output)
        {
            Setup(x => x.Delete(It.Is<HistoricoTarefa>(a => a.Id == idHistoricoTarefa))).Returns(output);
        }
    }
}
