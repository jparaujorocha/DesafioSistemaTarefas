using Moq;
using DesafioSistemaTarefas.Application.Interfaces;
using DesafioSistemaTarefas.Application.DTOs;

namespace DesafioSistemaTarefas.Test.Mocks.Services
{
    public class MockTarefaService : Mock<ITarefaService>
    {
        public void MockBuscarTarefa(int idTarefa, TarefaDto output)
        {
            Setup(x => x.BuscarTarefa(It.Is<int>(i => i == idTarefa))).Returns(Task.FromResult(output));
        }
        public void MockBuscarTarefas(IEnumerable<TarefaDto> output)
        {
            Setup(x => x.BuscarTarefas()).Returns(Task.FromResult(output));
        }
        public void MockBuscarTarefasPorPeriodo(DateTime dataInicial, DateTime dataFinal, IEnumerable<TarefaDto> output)
        {
            Setup(x => x.BuscarTarefasPorPeriodo(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(Task.FromResult(output));
        }
        public void MockInserirTarefa(TarefaDto dadosEntrada, TarefaDto output)
        {
            Setup(x => x.InserirTarefa(It.Is<TarefaDto>(a => a == dadosEntrada))).Returns(Task.FromResult(output));
        }
        public void MockAtualizarTarefa(TarefaDto dadosEntrada, TarefaDto output)
        {
            Setup(x => x.AtualizarTarefa(It.Is<TarefaDto>(a => a == dadosEntrada))).Returns(Task.FromResult(output));
        }
        public void MockExcluirTarefa(int idHistorico, bool output)
        {
            Setup(x => x.ExcluirTarefa(It.Is<int>(a => a == idHistorico))).Returns(Task.FromResult(output));
        }
        public void MockConcluirTarefa(int idTarefa, bool output)
        {
            Setup(x => x.ConcluirTarefa(It.Is<int>(a => a == idTarefa))).Returns(Task.FromResult(output));
        }
        public void MockReativarTarefa(int idTarefa, TarefaDto output)
        {
            Setup(x => x.ReativarTarefa(It.Is<int>(a => a == idTarefa))).Returns(Task.FromResult(output));
        }
    }
}
