using DesafioSistemaTarefas.API.Controllers;
using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Test.Mocks.Objects;
using DesafioSistemaTarefas.Test.Mocks.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DesafioSistemaTarefas.Test.APITests
{
    public class HistoricoTarefaApi
    {
        private MockHistoricoTarefa _mockHistoricoTarefa;
        private MockHistoricoTarefaService _mockHistoricoTarefaService;
        private readonly ILogger<HistoricoTarefaController> _mockLoggerHistoricoTarefa = new Mock<ILogger<HistoricoTarefaController>>().Object;

        [Test]
        public void GetListaHistoricoTarefa_SemParametro_RetornaListaObjeto()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockHistoricoTarefa = new MockHistoricoTarefa();


            _mockHistoricoTarefa.listHistoricoTarefaDtoMock = MockMapper.mockMapper.Map<IEnumerable<HistoricoTarefaDto>>(_mockHistoricoTarefa.listHistoricoTarefaMockValido);

            _mockHistoricoTarefaService.MockBuscarHistoricoTarefas(_mockHistoricoTarefa.listHistoricoTarefaDtoMock);
            var historicoTarefaController = new HistoricoTarefaController(_mockHistoricoTarefaService.Object, _mockLoggerHistoricoTarefa);

            var resultObject = (IEnumerable<HistoricoTarefaDto>)((OkObjectResult)historicoTarefaController.Get().Result.Result).Value;

            _mockHistoricoTarefa.listHistoricoTarefaDtoMock.Should().BeEquivalentTo(resultObject);
        }
        [Test]
        public void GetListaHistoricoTarefa_NenhumRegistroEncontrado_RetornaMensagemErro()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefa.listHistoricoTarefaDtoMock = new List<HistoricoTarefaDto>();

            _mockHistoricoTarefaService.MockBuscarHistoricoTarefas(_mockHistoricoTarefa.listHistoricoTarefaDtoMock);
            var historicoTarefaController = new HistoricoTarefaController(_mockHistoricoTarefaService.Object, _mockLoggerHistoricoTarefa);

            var resultObject = (string)((NotFoundObjectResult)historicoTarefaController.Get().Result.Result).Value;

            resultObject.Should().BeSameAs("Nenhum Historico da Tarefa Encontrado.");
        }
        [Test]
        public void GetHistoricoTarefaPorId_IdValido_RetornaObjeto()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefa.historicoTarefaDtoMock = MockMapper.mockMapper.Map<HistoricoTarefaDto>(_mockHistoricoTarefa.historicoTarefaMockValido);

            _mockHistoricoTarefaService.MockBuscarHistoricoTarefaPorId(_mockHistoricoTarefa.historicoTarefaMockValido.Id, _mockHistoricoTarefa.historicoTarefaDtoMock);
            var historicoTarefaController = new HistoricoTarefaController(_mockHistoricoTarefaService.Object, _mockLoggerHistoricoTarefa);

            var resultObject = (HistoricoTarefaDto)((OkObjectResult)historicoTarefaController.Get(_mockHistoricoTarefa.historicoTarefaMockValido.Id).Result.Result).Value;

            _mockHistoricoTarefa.historicoTarefaDtoMock.Should().BeEquivalentTo(resultObject);
        }
        [Test]
        public void GetHistoricoTarefaPorId_IdInvalido_RetornaMensagemErro()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefa.historicoTarefaDtoMock = MockMapper.mockMapper.Map<HistoricoTarefaDto>(_mockHistoricoTarefa.historicoTarefaMockValido);

            _mockHistoricoTarefaService.MockBuscarHistoricoTarefaPorId(_mockHistoricoTarefa.historicoTarefaMockValido.Id, _mockHistoricoTarefa.historicoTarefaDtoMock);
            var historicoTarefaController = new HistoricoTarefaController(_mockHistoricoTarefaService.Object, _mockLoggerHistoricoTarefa);

            var resultObject = (string)((NotFoundObjectResult)historicoTarefaController.Get(580).Result.Result).Value;

            resultObject.Should().BeSameAs("Histórico da tarefa não encontrado.");
        }
        [Test]
        public void GetHistoricoTarefaPorIdTarefa_IdTarefaValido_RetornaObjeto()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefa.historicoTarefaDtoMock = MockMapper.mockMapper.Map<HistoricoTarefaDto>(_mockHistoricoTarefa.historicoTarefaMockValido);

            _mockHistoricoTarefaService.MockBuscarHistoricoTarefaPorIdTarefa(_mockHistoricoTarefa.historicoTarefaMockValido.IdTarefa, _mockHistoricoTarefa.historicoTarefaDtoMock);
            var historicoTarefaController = new HistoricoTarefaController(_mockHistoricoTarefaService.Object, _mockLoggerHistoricoTarefa);

            var resultObject = (HistoricoTarefaDto)((OkObjectResult)historicoTarefaController.GetByIdTarefa(_mockHistoricoTarefa.historicoTarefaMockValido.IdTarefa).Result.Result).Value;

            _mockHistoricoTarefa.historicoTarefaDtoMock.Should().BeEquivalentTo(resultObject);
        }
        [Test]
        public void GetHistoricoTarefaPorIdTarefa_IdTarefaInvalido_RetornaMensagemErro()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefa.historicoTarefaDtoMock = MockMapper.mockMapper.Map<HistoricoTarefaDto>(_mockHistoricoTarefa.historicoTarefaMockValido);

            _mockHistoricoTarefaService.MockBuscarHistoricoTarefaPorIdTarefa(_mockHistoricoTarefa.historicoTarefaMockValido.IdTarefa, _mockHistoricoTarefa.historicoTarefaDtoMock);
            var historicoTarefaController = new HistoricoTarefaController(_mockHistoricoTarefaService.Object, _mockLoggerHistoricoTarefa);

            var resultObject = (string)((NotFoundObjectResult)historicoTarefaController.Get(580).Result.Result).Value;

            resultObject.Should().BeSameAs("Histórico da tarefa não encontrado.");
        }
        [Test]
        public void PostInsertHistoricoTarefa_DadosValidos_RetornaObjeto()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefa.historicoTarefaDtoMock = MockMapper.mockMapper.Map<HistoricoTarefaDto>(_mockHistoricoTarefa.historicoTarefaMockValido);
            var dadosEntrada = _mockHistoricoTarefa.historicoTarefaDtoMock;
            dadosEntrada.Id = 0;

            _mockHistoricoTarefaService.MockInserirHistoricoTarefa(dadosEntrada, _mockHistoricoTarefa.historicoTarefaDtoMock);
            var historicoTarefaController = new HistoricoTarefaController(_mockHistoricoTarefaService.Object, _mockLoggerHistoricoTarefa);

            var resultObject = (HistoricoTarefaDto)((ObjectResult)historicoTarefaController.Post(dadosEntrada).Result.Result).Value;

            _mockHistoricoTarefa.historicoTarefaDtoMock.Should().BeEquivalentTo(resultObject);
        }
        [Test]
        public void PostInsertHistoricoTarefa_DadosInvalidos_RetornaMensagemErro()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefa.historicoTarefaDtoMock = null;
            var dadosEntrada = MockMapper.mockMapper.Map<HistoricoTarefaDto>(_mockHistoricoTarefa.historicoTarefaMockValido);

            _mockHistoricoTarefaService.MockInserirHistoricoTarefa(dadosEntrada, _mockHistoricoTarefa.historicoTarefaDtoMock);
            var historicoTarefaController = new HistoricoTarefaController(_mockHistoricoTarefaService.Object, _mockLoggerHistoricoTarefa);

            var resultObject = (string)((BadRequestObjectResult)historicoTarefaController.Post(_mockHistoricoTarefa.historicoTarefaDtoMock).Result.Result).Value;

            resultObject.Should().BeSameAs("Dados do histórico da tarefa inválidos.");
        }
        [Test]
        public void DeleteHistoricoTarefa_DadosValidos_RetornaMensagemSucesso()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefaService.MockExcluirHistoricoTarefa(_mockHistoricoTarefa.historicoTarefaMockValido.Id, true);
            var historicoTarefaController = new HistoricoTarefaController(_mockHistoricoTarefaService.Object, _mockLoggerHistoricoTarefa);

            var resultObject = (string)((OkObjectResult)historicoTarefaController.Delete(_mockHistoricoTarefa.historicoTarefaMockValido.Id).Result).Value;

            resultObject.Should().BeSameAs("Historico Apagado com Sucesso.");
        }
        [Test]
        public void DeleteInsertHistoricoTarefa_DadosInvalidos_RetornaMensagemErro()
        {
            _mockHistoricoTarefaService = new MockHistoricoTarefaService();
            _mockHistoricoTarefa = new MockHistoricoTarefa();

            _mockHistoricoTarefaService.MockExcluirHistoricoTarefa(_mockHistoricoTarefa.historicoTarefaMockValido.Id, true);
            var historicoTarefaController = new HistoricoTarefaController(_mockHistoricoTarefaService.Object, _mockLoggerHistoricoTarefa);

            var resultObject = (string)((BadRequestObjectResult)historicoTarefaController.Delete(0).Result).Value;

            resultObject.Should().BeSameAs("Histórico da Tarefa enviado para exclusão inválido.");
        }

    }
}
