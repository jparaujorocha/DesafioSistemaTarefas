using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Application.Interfaces;
using DesafioSistemaTarefas.Application.Services;
using DesafioSistemaTarefas.Domain.Entities;
using DesafioSistemaTarefas.Domain.Exceptions;
using DesafioSistemaTarefas.Test.Mocks.Objects;
using DesafioSistemaTarefas.Test.Mocks.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DesafioSistemaTarefas.Test.ApplicationTests.Services
{
    public class HistoricoTarefaServiceTest
    {
        private MockHistoricoTarefaRepository _mockHistoricoTarefaRepository;
        private MockHistoricoTarefa _mockHistoricoTarefa;
        private readonly ILogger<HistoricoTarefaService> _mockLoggerHistoricoTarefa = new Mock<ILogger<HistoricoTarefaService>>().Object;

        [Test]
        public void GetHistoricoTarefaByIdTarefa_IdTarefaExistente_RetornaObjeto()
        {
            _mockHistoricoTarefaRepository = new MockHistoricoTarefaRepository();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefa.historicoTarefaDtoMock = MockMapper.mockMapper.Map<HistoricoTarefaDto>(_mockHistoricoTarefa.historicoTarefaMockValido);

            _mockHistoricoTarefaRepository.MockGetByPredicate(Task.FromResult(_mockHistoricoTarefa.historicoTarefaMockValido));

            IHistoricoTarefaService historicoTarefaService = new HistoricoTarefaService(_mockHistoricoTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerHistoricoTarefa);

            var result = historicoTarefaService.BuscarPorIdTarefa(_mockHistoricoTarefa.historicoTarefaMockValido.IdTarefa);

            _mockHistoricoTarefa.historicoTarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }

        [Test]
        public void GetHistoricoTarefaByIdTarefa_IdTarefaNaoExistente_RetornaObjetoNulo()
        {
            _mockHistoricoTarefaRepository = new MockHistoricoTarefaRepository();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefa.historicoTarefaDtoMock = new HistoricoTarefaDto();
            var historicoNulo = MockMapper.mockMapper.Map<HistoricoTarefa>(_mockHistoricoTarefa.historicoTarefaDtoMock);

            _mockHistoricoTarefaRepository.MockGetByPredicate(Task.FromResult(historicoNulo));

            IHistoricoTarefaService historicoTarefaService = new HistoricoTarefaService(_mockHistoricoTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerHistoricoTarefa);

            var result = historicoTarefaService.BuscarPorIdTarefa(_mockHistoricoTarefa.historicoTarefaMockValido.IdTarefa);

            _mockHistoricoTarefa.historicoTarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }
        [Test]
        public void GetHistoricoTarefaById_IdExistente_RetornaObjeto()
        {
            _mockHistoricoTarefaRepository = new MockHistoricoTarefaRepository();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefa.historicoTarefaDtoMock = MockMapper.mockMapper.Map<HistoricoTarefaDto>(_mockHistoricoTarefa.historicoTarefaMockValido);

            _mockHistoricoTarefaRepository.MockGetById(_mockHistoricoTarefa.historicoTarefaMockValido.IdTarefa, Task.FromResult(_mockHistoricoTarefa.historicoTarefaMockValido));

            IHistoricoTarefaService historicoTarefaService = new HistoricoTarefaService(_mockHistoricoTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerHistoricoTarefa);

            var result = historicoTarefaService.BuscarPorId(_mockHistoricoTarefa.historicoTarefaMockValido.IdTarefa);

            _mockHistoricoTarefa.historicoTarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }

        [Test]
        public void GetHistoricoTarefaById_IdHistoricoTarefaNaoExistente_RetornaObjetoNulo()
        {
            _mockHistoricoTarefaRepository = new MockHistoricoTarefaRepository();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefa.historicoTarefaDtoMock = new HistoricoTarefaDto();
            var historicoNulo = MockMapper.mockMapper.Map<HistoricoTarefa>(_mockHistoricoTarefa.historicoTarefaDtoMock);

            _mockHistoricoTarefaRepository.MockGetById(_mockHistoricoTarefa.historicoTarefaMockValido.IdTarefa, Task.FromResult(historicoNulo));

            IHistoricoTarefaService historicoTarefaService = new HistoricoTarefaService(_mockHistoricoTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerHistoricoTarefa);

            var result = historicoTarefaService.BuscarPorId(520);

            _mockHistoricoTarefa.historicoTarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }

        [Test]
        public void GetListaHistoricoTarefa_DadosExistentes_RetornaObjeto()
        {
            _mockHistoricoTarefaRepository = new MockHistoricoTarefaRepository();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefa.listHistoricoTarefaDtoMock = MockMapper.mockMapper.Map<IEnumerable<HistoricoTarefaDto>>(_mockHistoricoTarefa.listHistoricoTarefaMockValido);

            _mockHistoricoTarefaRepository.MockGetAll(Task.FromResult(_mockHistoricoTarefa.listHistoricoTarefaMockValido));

            IHistoricoTarefaService historicoTarefaService = new HistoricoTarefaService(_mockHistoricoTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerHistoricoTarefa);

            var result = historicoTarefaService.BuscarHistoricoTarefas();

            _mockHistoricoTarefa.listHistoricoTarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }

        [Test]
        public void GetListaHistoricoTarefa_DadosNaoExistentes_RetornaObjetoVazio()
        {
            _mockHistoricoTarefaRepository = new MockHistoricoTarefaRepository();
            _mockHistoricoTarefa = new MockHistoricoTarefa();
            _mockHistoricoTarefa.listHistoricoTarefaDtoMock = new List<HistoricoTarefaDto>();
            var listaHistoricoTarefaVazia = MockMapper.mockMapper.Map<IEnumerable<HistoricoTarefa>>(_mockHistoricoTarefa.listHistoricoTarefaDtoMock);

            _mockHistoricoTarefaRepository.MockGetAll(Task.FromResult(listaHistoricoTarefaVazia));

            IHistoricoTarefaService historicoTarefaService = new HistoricoTarefaService(_mockHistoricoTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerHistoricoTarefa);

            var result = historicoTarefaService.BuscarHistoricoTarefas();

            _mockHistoricoTarefa.listHistoricoTarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }

        [Test]
        public void InsertHistoricoTarefa_DadosValidos_RetornaObjeto()
        {
            _mockHistoricoTarefaRepository = new MockHistoricoTarefaRepository();
            _mockHistoricoTarefa = new MockHistoricoTarefa();
            var dadosEntrada = MockMapper.mockMapper.Map<HistoricoTarefa>(_mockHistoricoTarefa.historicoTarefaMockValido);
            _mockHistoricoTarefa.historicoTarefaDtoMock = MockMapper.mockMapper.Map<HistoricoTarefaDto>(_mockHistoricoTarefa.historicoTarefaMockValido);

            dadosEntrada.SetDataCriacao(DateTime.Now);
            dadosEntrada.SetDataHoraConclusao(DateTime.Now);
            dadosEntrada.Id = 0;

            _mockHistoricoTarefa.historicoTarefaDtoMock = MockMapper.mockMapper.Map<HistoricoTarefaDto>(dadosEntrada);

            _mockHistoricoTarefaRepository.MockAdd(dadosEntrada, Task.FromResult(_mockHistoricoTarefa.historicoTarefaMockValido));

            IHistoricoTarefaService historicoTarefaService = new HistoricoTarefaService(_mockHistoricoTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerHistoricoTarefa);

            var result = historicoTarefaService.InserirHistoricoTarefa(_mockHistoricoTarefa.historicoTarefaDtoMock);

            _mockHistoricoTarefa.historicoTarefaDtoMock.Should().BeEquivalentTo(result.Result);
        }

        [Test]
        public void InsertHistoricoTarefa_ObjetoEmBranco_RetornaExcecao()
        {
            _mockHistoricoTarefaRepository = new MockHistoricoTarefaRepository();
            _mockHistoricoTarefa = new MockHistoricoTarefa();
            _mockHistoricoTarefa.historicoTarefaDtoMock = null;

            _mockHistoricoTarefaRepository.MockAdd(_mockHistoricoTarefa.historicoTarefaMockValido, Task.FromResult(_mockHistoricoTarefa.historicoTarefaMockValido));

            IHistoricoTarefaService historicoTarefaService = new HistoricoTarefaService(_mockHistoricoTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerHistoricoTarefa);

            var exception = Assert.ThrowsAsync<DomainException>(() => historicoTarefaService.InserirHistoricoTarefa(_mockHistoricoTarefa.historicoTarefaDtoMock));

            exception.Should().NotBeNull();
            exception.Message.Should().BeSameAs("Necessário informar uma tarefa do histórico para inserção.");
        }

        [Test]
        public void ExcluirHistoricoTarefa_DadosValidos_OperacaoValida()
        {
            _mockHistoricoTarefaRepository = new MockHistoricoTarefaRepository();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefaRepository.MockDelete(_mockHistoricoTarefa.historicoTarefaMockValido.Id, Task.FromResult(_mockHistoricoTarefa.historicoTarefaMockValido));
            _mockHistoricoTarefaRepository.MockGetById(_mockHistoricoTarefa.historicoTarefaMockValido.Id, Task.FromResult(_mockHistoricoTarefa.historicoTarefaMockValido));

            IHistoricoTarefaService historicoTarefaService = new HistoricoTarefaService(_mockHistoricoTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerHistoricoTarefa);

            bool result = historicoTarefaService.ExcluirHistoricoTarefa(_mockHistoricoTarefa.historicoTarefaMockValido.Id).Result;

            result.Should().BeTrue();
        }

        [Test]
        public void ExcluirHistoricoTarefa_DadosInvalidos_RetornaExcecao()
        {
            _mockHistoricoTarefaRepository = new MockHistoricoTarefaRepository();
            _mockHistoricoTarefa = new MockHistoricoTarefa();
            _mockHistoricoTarefa.historicoTarefaDtoMock = MockMapper.mockMapper.Map<HistoricoTarefaDto>(_mockHistoricoTarefa.historicoTarefaMockValido);

            _mockHistoricoTarefaRepository.MockDelete(_mockHistoricoTarefa.historicoTarefaMockValido.Id, Task.FromResult(_mockHistoricoTarefa.historicoTarefaMockValido));

            IHistoricoTarefaService historicoTarefaService = new HistoricoTarefaService(_mockHistoricoTarefaRepository.Object, MockMapper.mockMapper, _mockLoggerHistoricoTarefa);

            var exception = Assert.ThrowsAsync<DomainException>(() => historicoTarefaService.ExcluirHistoricoTarefa(300));

            exception.Should().NotBeNull();
            exception.Message.Should().BeSameAs("Necessário informar um registro válido para exclusão.");
        }
    }
}
