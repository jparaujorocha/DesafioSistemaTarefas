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
    public class TarefaApi
    {
        private MockTarefa _mockTarefa;
        private MockTarefaService _mockTarefaService;
        private readonly ILogger<TarefaController> _mockLoggerTarefa = new Mock<ILogger<TarefaController>>().Object;

        [Test]
        public void GetListaTarefa_SemParametro_RetornaListaObjeto()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();


            _mockTarefa.listTarefaDtoMock = MockMapper.mockMapper.Map<IEnumerable<TarefaDto>>(_mockTarefa.listTarefaMockValido);

            _mockTarefaService.MockBuscarTarefas(_mockTarefa.listTarefaDtoMock);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (IEnumerable<TarefaDto>)((OkObjectResult)tarefaController.GetTarefas().Result.Result).Value;

            _mockTarefa.listTarefaDtoMock.Should().BeEquivalentTo(resultObject);
        }
        [Test]
        public void GetListaTarefa_NenhumRegistroEncontrado_RetornaMensagemErro()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefa.listTarefaDtoMock = new List<TarefaDto>();

            _mockTarefaService.MockBuscarTarefas(_mockTarefa.listTarefaDtoMock);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (string)((NotFoundObjectResult)tarefaController.GetTarefas().Result.Result).Value;

            resultObject.Should().BeSameAs("Nenhuma Tarefa Encontrada.");
        }
        [Test]
        public void GetTarefaPorId_IdValido_RetornaObjeto()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefa.tarefaDtoMock = MockMapper.mockMapper.Map<TarefaDto>(_mockTarefa.tarefaMockValido);

            _mockTarefaService.MockBuscarTarefa(_mockTarefa.tarefaMockValido.Id, _mockTarefa.tarefaDtoMock);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (TarefaDto)((OkObjectResult)tarefaController.GetTarefa(_mockTarefa.tarefaMockValido.Id).Result.Result).Value;

            _mockTarefa.tarefaDtoMock.Should().BeEquivalentTo(resultObject);
        }
        [Test]
        public void GetTarefaPorId_IdInvalido_RetornaMensagemErro()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefa.tarefaDtoMock = MockMapper.mockMapper.Map<TarefaDto>(_mockTarefa.tarefaMockValido);

            _mockTarefaService.MockBuscarTarefa(_mockTarefa.tarefaMockValido.Id, _mockTarefa.tarefaDtoMock);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (string)((NotFoundObjectResult)tarefaController.GetTarefa(580).Result.Result).Value;

            resultObject.Should().BeSameAs("Tarefa não encontrada.");
        }
        [Test]
        public void GetTarefaPorPeriodo_DadosEncontrados_RetornaObjeto()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefa.listTarefaDtoMock = MockMapper.mockMapper.Map<IEnumerable<TarefaDto>>(_mockTarefa.listTarefaMockValido);
            DateTime dataInicial = new DateTime(DateTime.Now.AddDays(-90).Year, DateTime.Now.AddDays(-90).Month, DateTime.Now.AddDays(-90).Day, 0, 0, 0);
            DateTime dataFinal = new DateTime(DateTime.Now.AddDays(10).Year, DateTime.Now.AddDays(10).Month, DateTime.Now.AddDays(10).Day, 23, 59, 59);

            _mockTarefaService.MockBuscarTarefasPorPeriodo(dataInicial, dataFinal, _mockTarefa.listTarefaDtoMock);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (IEnumerable<TarefaDto>)((OkObjectResult)tarefaController.GetTarefasPorPeriodo(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(10)).Result.Result).Value;

            _mockTarefa.listTarefaDtoMock.Should().BeEquivalentTo(resultObject);
        }
        [Test]
        public void GetTarefaPorPeriodo_DadosNaoEncontrados_RetornaMensagemErro()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefa.listTarefaDtoMock = null;

            DateTime dataInicial = new DateTime(DateTime.Now.AddDays(-150).Year, DateTime.Now.AddDays(-150).Month, DateTime.Now.AddDays(-150).Day, 0, 0, 0);
            DateTime dataFinal = new DateTime(DateTime.Now.AddDays(-20).Year, DateTime.Now.AddDays(-20).Month, DateTime.Now.AddDays(-20).Day, 23, 59, 59);

            _mockTarefaService.MockBuscarTarefasPorPeriodo(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(10), _mockTarefa.listTarefaDtoMock);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (string)((NotFoundObjectResult)tarefaController.GetTarefasPorPeriodo(dataInicial, dataFinal).Result.Result).Value;

            resultObject.Should().BeSameAs("Nenhuma Tarefa Encontrada.");
        }
        [Test]
        public void PostInsertTarefa_DadosValidos_RetornaObjeto()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefa.tarefaDtoMock = MockMapper.mockMapper.Map<TarefaDto>(_mockTarefa.tarefaMockValido);
            var dadosEntrada = _mockTarefa.tarefaDtoMock;
            dadosEntrada.id = 0;

            _mockTarefaService.MockInserirTarefa(dadosEntrada, _mockTarefa.tarefaDtoMock);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = tarefaController.InsertTarefa(dadosEntrada);


            resultObject.IsCompletedSuccessfully.Should().BeTrue();
        }
        [Test]
        public void PostInsertTarefa_DadosInvalidos_RetornaMensagemErro()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefa.tarefaDtoMock = null;
            var dadosEntrada = MockMapper.mockMapper.Map<TarefaDto>(_mockTarefa.tarefaMockValido);

            _mockTarefaService.MockInserirTarefa(dadosEntrada, _mockTarefa.tarefaDtoMock);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (string)((BadRequestObjectResult)tarefaController.InsertTarefa(_mockTarefa.tarefaDtoMock).Result.Result).Value;

            resultObject.Should().BeSameAs("Dados Inválidos.");
        }
        [Test]
        public void DeleteTarefa_DadosValidos_RetornaMensagemSucesso()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefaService.MockExcluirTarefa(_mockTarefa.tarefaMockValido.Id, true);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (string)((OkObjectResult)tarefaController.DeleteTarefa(_mockTarefa.tarefaMockValido.Id).Result).Value;

            resultObject.Should().BeSameAs("Tarefa excluída com sucesso.");
        }
        [Test]
        public void DeleteTarefa_DadosInvalidos_RetornaMensagemErro()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefaService.MockExcluirTarefa(_mockTarefa.tarefaMockValido.Id, true);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (string)((BadRequestObjectResult)tarefaController.DeleteTarefa(0).Result).Value;

            resultObject.Should().BeSameAs("Tarefa enviada para exclusão inválida.");
        }
        [Test]
        public void PostRestauraTarefa_DadosValidos_RetornaObjeto()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefa.tarefaDtoMock = MockMapper.mockMapper.Map<TarefaDto>(_mockTarefa.tarefaMockValido);

            _mockTarefaService.MockReativarTarefa(_mockTarefa.tarefaMockValido.Id, _mockTarefa.tarefaDtoMock);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (tarefaController.PostRestaurarTarefa(_mockTarefa.tarefaMockValido.Id));

            resultObject.IsCompletedSuccessfully.Should().BeTrue();
        }
        [Test]
        public void PostRestauraTarefa_DadosInvalidos_RetornaMensagemErro()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefa.tarefaDtoMock = new TarefaDto();

            _mockTarefaService.MockReativarTarefa(_mockTarefa.tarefaMockValido.Id, _mockTarefa.tarefaDtoMock);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (string)((BadRequestObjectResult)tarefaController.PostRestaurarTarefa(0).Result.Result).Value;

            resultObject.Should().BeSameAs("Tarefa enviada para reativação inválida.");
        }

        [Test]
        public void PutConcluirTarefa_DadosValidos_RetornaMensagemSucesso()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefaService.MockConcluirTarefa(_mockTarefa.tarefaMockValido.Id, true);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (OkResult)tarefaController.UpdateConcluirTarefa(_mockTarefa.tarefaMockValido.Id).Result;

            resultObject.StatusCode.Should().Be(200);
        }

        [Test]
        public void PutConcluirTarefa_DadosInvalidos_RetornaMensagemErro()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefaService.MockConcluirTarefa(_mockTarefa.tarefaMockValido.Id, true);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (string)((BadRequestObjectResult)tarefaController.UpdateConcluirTarefa(0).Result).Value;

            resultObject.Should().BeSameAs("Tarefa enviada para conclusão inválida.");
        }

        [Test]
        public void PutUpdateTarefa_DadosValidos_RetornaMensagemSucesso()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefa.tarefaDtoMock = MockMapper.mockMapper.Map<TarefaDto>(_mockTarefa.tarefaMockValido);

            _mockTarefaService.MockAtualizarTarefa(_mockTarefa.tarefaDtoMock, _mockTarefa.tarefaDtoMock);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (TarefaDto)((OkObjectResult)tarefaController.UpdateTarefa(_mockTarefa.tarefaDtoMock).Result.Result).Value;

            _mockTarefa.tarefaDtoMock.Should().BeEquivalentTo(resultObject);
        }

        [Test]
        public void PutCUpdateTarefa_DadosInvalidos_RetornaMensagemErro()
        {
            _mockTarefaService = new MockTarefaService();
            _mockTarefa = new MockTarefa();

            _mockTarefa.tarefaDtoMock = null;
            _mockTarefaService.MockAtualizarTarefa(_mockTarefa.tarefaDtoMock, _mockTarefa.tarefaDtoMock);
            var tarefaController = new TarefaController(_mockTarefaService.Object, _mockLoggerTarefa);

            var resultObject = (string)((BadRequestObjectResult)tarefaController.UpdateTarefa(_mockTarefa.tarefaDtoMock).Result.Result).Value;

            resultObject.Should().BeSameAs("Tarefa enviada para atualização inválida.");
        }
    }
}
