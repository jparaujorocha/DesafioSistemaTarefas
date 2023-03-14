using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Application.Interfaces;
using DesafioSistemaTarefas.Application.Services;
using DesafioSistemaTarefas.Domain.Entities;
using DesafioSistemaTarefas.Domain.Enums;
using DesafioSistemaTarefas.Domain.Exceptions;
using DesafioSistemaTarefas.Test.Mocks.Objects;
using DesafioSistemaTarefas.Test.Mocks.Repositories;
using DesafioSistemaTarefas.Test.Mocks.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DesafioSistemaTarefas.Test.ApplicationTests.Services
{
    public class TarefaServiceTest
    {
        private MockTarefa _mockTarefa;
        private MockHistoricoTarefa _mockHistoricoTarefa;
        private MockTarefaRepository _mockTarefaRepository;
        private MockHistoricoTarefaService _mockHistoricoTarefaService;
        private readonly ILogger<TarefaService> _mockLoggerTarefa = new Mock<ILogger<TarefaService>>().Object;


        [Test]
        public void GetTarefasByPeriodo_PeriodoValido_RetornaListaObjeto()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();

            _mockTarefa.listTarefaDtoMock = MockMapper.mockMapper.Map<IEnumerable<TarefaDto>>(_mockTarefa.listTarefaMockValido);

            DateTime dataInicial = new DateTime(DateTime.Now.AddDays(-90).Year, DateTime.Now.AddDays(-90).Month, DateTime.Now.AddDays(-90).Day, 0, 0, 0);
            DateTime dataFinal = new DateTime(DateTime.Now.AddDays(10).Year, DateTime.Now.AddDays(10).Month, DateTime.Now.AddDays(10).Day, 23, 59, 59);

            _mockTarefaRepository.MockGetAllByPredicate(Task.FromResult(_mockTarefa.listTarefaMockValido));
            _mockTarefaRepository.MockGetTarefasByPeriodo(dataInicial, dataFinal, Task.FromResult(_mockTarefa.listTarefaMockValido));

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var result = tarefaService.BuscarTarefasPorPeriodo(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(10));

            _mockTarefa.listTarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }

        [Test]
        public void GetTarefasByPeriodo_PeriodoInvalido_RetornaListaObjetoVazia()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();

            _mockTarefa.listTarefaDtoMock = new List<TarefaDto>();

            DateTime dataInicial = new DateTime(DateTime.Now.AddDays(-150).Year, DateTime.Now.AddDays(-150).Month, DateTime.Now.AddDays(-150).Day, 0, 0, 0);
            DateTime dataFinal = new DateTime(DateTime.Now.AddDays(-20).Year, DateTime.Now.AddDays(-20).Month, DateTime.Now.AddDays(-20).Day, 23, 59, 59);

            _mockTarefaRepository.MockGetAllByPredicate(Task.FromResult(_mockTarefa.listTarefaMockValido));
            _mockTarefaRepository.MockGetTarefasByPeriodo(dataInicial, dataFinal, Task.FromResult(_mockTarefa.listTarefaMockValido));

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var result = tarefaService.BuscarTarefasPorPeriodo(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(10));

            _mockTarefa.listTarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }

        [Test]
        public void GetTarefaById_IdExistente_RetornaObjeto()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();
            _mockTarefa.tarefaDtoMock = MockMapper.mockMapper.Map<TarefaDto>(_mockTarefa.tarefaMockValido);

            _mockTarefaRepository.MockGetById(_mockTarefa.tarefaMockValido.Id, Task.FromResult(_mockTarefa.tarefaMockValido));

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var result = tarefaService.BuscarTarefa(_mockTarefa.tarefaMockValido.Id);

            _mockTarefa.tarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }

        [Test]
        public void GetTarefaById_IdTarefaNaoExistente_RetornaObjetoNulo()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();

            _mockTarefa.tarefaDtoMock = new TarefaDto();
            var historicoNulo = MockMapper.mockMapper.Map<Tarefa>(_mockTarefa.tarefaDtoMock);

            _mockTarefaRepository.MockGetById(_mockTarefa.tarefaMockValido.Id, Task.FromResult(historicoNulo));

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var result = tarefaService.BuscarTarefa(520);
            _mockTarefa.tarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }

        [Test]
        public void GetListaTarefa_DadosExistentes_RetornaObjeto()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();

            _mockTarefa.listTarefaDtoMock = MockMapper.mockMapper.Map<IEnumerable<TarefaDto>>(_mockTarefa.listTarefaMockValido);

            _mockTarefaRepository.MockGetAll(Task.FromResult(_mockTarefa.listTarefaMockValido));

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var result = tarefaService.BuscarTarefas();

            _mockTarefa.listTarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }

        [Test]
        public void GetListaTarefa_DadosNaoExistentes_RetornaObjetoVazio()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();
            _mockTarefa.listTarefaDtoMock = new List<TarefaDto>();
            var listaTarefaVazia = MockMapper.mockMapper.Map<IEnumerable<Tarefa>>(_mockTarefa.listTarefaDtoMock);

            _mockTarefaRepository.MockGetAll(Task.FromResult(listaTarefaVazia));

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var result = tarefaService.BuscarTarefas();

            _mockTarefa.listTarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }

        [Test]
        public void InsertTarefa_DadosValidos_RetornaObjeto()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();
            var dadosEntrada = MockMapper.mockMapper.Map<Tarefa>(_mockTarefa.tarefaMockValido);
            _mockTarefa.tarefaDtoMock = MockMapper.mockMapper.Map<TarefaDto>(_mockTarefa.tarefaMockValido);

            dadosEntrada.Id = 0;

            _mockTarefa.tarefaDtoMock = MockMapper.mockMapper.Map<TarefaDto>(dadosEntrada);

            _mockTarefaRepository.MockAdd(dadosEntrada, Task.FromResult(_mockTarefa.tarefaMockValido));

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var result = tarefaService.InserirTarefa(_mockTarefa.tarefaDtoMock);

            _mockTarefa.tarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }

        [Test]
        public void InsertTarefa_ObjetoEmBranco_RetornaExcecao()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();
            _mockTarefa.tarefaDtoMock = null;

            _mockTarefaRepository.MockAdd(_mockTarefa.tarefaMockValido, Task.FromResult(_mockTarefa.tarefaMockValido));

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var exception = Assert.ThrowsAsync<DomainException>(() => tarefaService.InserirTarefa(_mockTarefa.tarefaDtoMock));

            exception.Should().NotBeNull();
            exception.Message.Should().BeSameAs("Necessário informar uma tarefa para inserção.");
        }
        [Test]
        public void UpdateTarefa_DadosValidos_RetornaObjeto()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();

            _mockTarefa.tarefaDtoMock = MockMapper.mockMapper.Map<TarefaDto>(_mockTarefa.tarefaMockValido);            

            _mockTarefaRepository.MockGetById(_mockTarefa.tarefaMockValido.Id, Task.FromResult(_mockTarefa.tarefaMockValido));
            _mockTarefaRepository.MockUpdate(_mockTarefa.tarefaMockValido, Task.FromResult(_mockTarefa.tarefaMockValido));

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var result = tarefaService.AtualizarTarefa(_mockTarefa.tarefaDtoMock);

            result.Result.Should().NotBeNull();
            _mockTarefa.tarefaDtoMock.dataAtualizacao = result.Result.dataAtualizacao;
            _mockTarefa.tarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }

        [Test]
        public void UpdateTarefa_ObjetoInvalido_RetornaExcecao()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();

            _mockTarefa.tarefaDtoMock = new TarefaDto();

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var exception = Assert.ThrowsAsync<DomainException>(() => tarefaService.AtualizarTarefa(_mockTarefa.tarefaDtoMock));

            exception.Should().NotBeNull();
            exception.Message.Should().BeSameAs("Necessário informar uma tarefa para atualização.");
        }

        [Test]
        public void UpdateTarefa_ObjetoNaoEncontrado_RetornaExcecao()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();

            _mockTarefa.tarefaDtoMock = MockMapper.mockMapper.Map<TarefaDto>(_mockTarefa.tarefaMockValido);
            _mockTarefaRepository.MockGetById(5000, Task.FromResult(_mockTarefa.tarefaMockValido));

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var exception = Assert.ThrowsAsync<DomainException>(() => tarefaService.AtualizarTarefa(_mockTarefa.tarefaDtoMock));

            exception.Should().NotBeNull();
            exception.Message.Should().BeSameAs("Nenhuma tarefa encontrada para atualização.");
        }
        [Test]
        public void ExcluirTarefa_DadosValidos_OperacaoValida()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();

            _mockTarefaRepository.MockDelete(_mockTarefa.tarefaMockValido.Id, Task.FromResult(_mockTarefa.tarefaMockValido));
            _mockTarefaRepository.MockGetById(_mockTarefa.tarefaMockValido.Id, Task.FromResult(_mockTarefa.tarefaMockValido));

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            bool result = tarefaService.ExcluirTarefa(_mockTarefa.tarefaMockValido.Id).Result;
            result.Should().BeTrue();
        }

        [Test]
        public void ExcluirTarefa_DadosInvalidos_RetornaExcecao()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();
            _mockTarefa.tarefaDtoMock = MockMapper.mockMapper.Map<TarefaDto>(_mockTarefa.tarefaMockValido);

            _mockTarefaRepository.MockDelete(_mockTarefa.tarefaMockValido.Id, Task.FromResult(_mockTarefa.tarefaMockValido));

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var exception = Assert.ThrowsAsync<DomainException>(() => tarefaService.ExcluirTarefa(300));

            exception.Should().NotBeNull();
            exception.Message.Should().BeSameAs("Tarefa informada não encontrada na base.");
        }
        [Test]
        public void ConcluirTarefa_DadosValidos_OperacaoValida()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();

            _mockTarefaRepository.MockDelete(_mockTarefa.tarefaMockValido.Id, Task.FromResult(_mockTarefa.tarefaMockValido));
            _mockTarefaRepository.MockGetById(_mockTarefa.tarefaMockValido.Id, Task.FromResult(_mockTarefa.tarefaMockValido));

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            bool result = tarefaService.ConcluirTarefa(_mockTarefa.tarefaMockValido.Id).Result;
            result.Should().BeTrue();
        }

        [Test]
        public void ConcluirTarefa_DadosInvalidos_RetornaExcecao()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();
            _mockTarefa.tarefaDtoMock = MockMapper.mockMapper.Map<TarefaDto>(_mockTarefa.tarefaMockValido);

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var exception = Assert.ThrowsAsync<DomainException>(() => tarefaService.ConcluirTarefa(300));

            exception.Should().NotBeNull();
            exception.Message.Should().BeSameAs("Tarefa informada não encontrada na base.");
        }
        [Test]
        public void ReativarTarefa_DadosValidos_OperacaoValida()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();
            _mockTarefa = new MockTarefa();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefa.historicoTarefaDtoMock = MockMapper.mockMapper.Map<HistoricoTarefaDto>(_mockTarefa.tarefaMockValido);
            _mockTarefa.tarefaDtoMock = MockMapper.mockMapper.Map<TarefaDto>(_mockTarefa.tarefaMockValido);
            
            var dadosEntrada = _mockTarefa.tarefaMockValido;
            dadosEntrada.Id = 0;
            _mockTarefa.tarefaMockValido.SetStatusTarefa((int)EnumStatusTarefa.ATIVA);

            _mockHistoricoTarefaService.MockExcluirHistoricoTarefa(_mockHistoricoTarefa.historicoTarefaMockValido.Id, true);
            _mockHistoricoTarefaService.MockBuscarHistoricoTarefaPorIdTarefa(_mockTarefa.tarefaMockValido.Id, _mockHistoricoTarefa.historicoTarefaDtoMock);
            _mockTarefaRepository.MockAdd(dadosEntrada, Task.FromResult(_mockTarefa.tarefaMockValido));
            _mockTarefaRepository.MockGetById(_mockTarefa.tarefaMockValido.Id, Task.FromResult(_mockTarefa.tarefaMockValido));

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var result = tarefaService.ReativarTarefa(_mockTarefa.tarefaMockValido.Id).Result;
            result.Should().NotBeNull();
            result.idStatusTarefa.Should().Be((int)EnumStatusTarefa.ATIVA);
        }

        [Test]
        public void ReativarTarefa_IdInvalido_RetornaExcecao()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockTarefaRepository = new MockTarefaRepository();;

            ITarefaService tarefaService = new TarefaService(_mockHistoricoTarefaService.Object, _mockTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerTarefa);

            var exception = Assert.ThrowsAsync<DomainException>(() => tarefaService.ReativarTarefa(300));

            exception.Should().NotBeNull();
            exception.Message.Should().BeSameAs("Tarefa informada não encontrada na base histórica.");
        }
    }
}
