using DesafioSistemaTarefas.Domain.Entities;
using DesafioSistemaTarefas.Domain.Enums;
using DesafioSistemaTarefas.Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace DesafioSistemaTarefas.Test.DomainTests
{
    public class HistoricoTarefaTest
    {

        [Theory]
        [TestCase(0, 3, "Hst 1.", "Teste de Hst nº 1.", "2022-12-20", 1, "2022-12-20", "2022-12-20")]
        public void SetarParametrosHistoricoTarefa_ParametrosValidos_RetornarObjetoValido(int id, int idTarefa, string nome, string descricao, DateTime dataHoraTarefa,
                                                                       int idStatusTarefa, DateTime? dataHoraExclusao, DateTime? dataHoraConclusao)
        {

            HistoricoTarefa dadosTarefa = new HistoricoTarefa(id, idTarefa, dataHoraExclusao, dataHoraConclusao, nome, descricao,
                                                      dataHoraTarefa, idStatusTarefa);
            dadosTarefa.SetStatusTarefa((int)EnumStatusTarefa.CONCLUIDA);
            dadosTarefa.SetDataHoraTarefa(DateTime.Now.AddDays(15));
            dadosTarefa.SetDescricao("Teste tarefa nº 2");
            dadosTarefa.SetNome("Teste nº 2");
            dadosTarefa.SetId(2);
            dadosTarefa.SetDataHoraExclusao(dataHoraExclusao);
            dadosTarefa.SetDataHoraConclusao(dataHoraConclusao);

            Action action = () => dadosTarefa.ValidateWithId();

            action.Should().NotThrow<DomainException>();

            action.Target.Should().NotBeNull();
        }
        [Theory]
        [TestCase(0, 3, "Hst 1.", "Teste de Hst nº 1.", "2022-12-20", 1, "2022-12-20", "2022-12-20")]
        public void SetarParametrosHistoricoTarefa_ParametrosValidosIdZero_RetornarObjetoValido(int id, int idTarefa, string nome, string descricao, DateTime dataHoraTarefa,
                                                                       int idStatusTarefa, DateTime? dataHoraExclusao, DateTime? dataHoraConclusao)
        {

            HistoricoTarefa dadosTarefa = new HistoricoTarefa(id, idTarefa, dataHoraExclusao, dataHoraConclusao, nome, descricao,
                                                      dataHoraTarefa, idStatusTarefa);
            dadosTarefa.SetStatusTarefa((int)EnumStatusTarefa.CONCLUIDA);
            dadosTarefa.SetDataHoraTarefa(DateTime.Now.AddDays(15));
            dadosTarefa.SetDescricao("Teste tarefa nº 3");
            dadosTarefa.SetNome("Teste nº 3");
            dadosTarefa.SetId(0);
            dadosTarefa.SetDataHoraExclusao(dataHoraExclusao);
            dadosTarefa.SetDataHoraConclusao(dataHoraConclusao);

            Action action = () => dadosTarefa.ValidateWithoutId();

            action.Should().NotThrow<DomainException>();

            action.Target.Should().NotBeNull();
        }
        [Theory]
        [TestCase(0, 3, "Hst 1.", "Teste de Hst nº 1.", "2022-12-20", 1, "2022-12-20", "2022-12-20")]
        public void SetarParametrosTarefa_ParametrosInvalidos_RetornaExcecao(int id, int idTarefa, string nome, string descricao, DateTime dataHoraTarefa,
                                                                       int idStatusTarefa, DateTime? dataHoraExclusao, DateTime? dataHoraConclusao)
        {

            HistoricoTarefa dadosTarefa = new HistoricoTarefa(id, idTarefa, dataHoraExclusao, dataHoraConclusao, nome, descricao,
                                                      dataHoraTarefa, idStatusTarefa);
            dadosTarefa.SetStatusTarefa((int)EnumStatusTarefa.CONCLUIDA);
            dadosTarefa.SetDataHoraTarefa(DateTime.Now.AddDays(15));
            dadosTarefa.SetDescricao("Teste tarefa nº 3 Com ERRO!Teste tarefa nº 3 Com ERRO!Teste tarefa nº 3 Com ERRO!Teste tarefa nº 3 Com ERRO!Teste tarefa nº 3 Com ERRO!Teste tarefa nº 3 Com ERRO!Teste tarefa nº 3 Com ERRO!");
            dadosTarefa.SetNome("Teste nº 3 COM erROTeste nº 3 COM erROTeste nº 3 COM erRO");
            dadosTarefa.SetId(0);
            dadosTarefa.SetDataHoraExclusao(dataHoraExclusao);
            dadosTarefa.SetDataHoraConclusao(dataHoraConclusao);

            Action action = () => dadosTarefa.ValidateWithoutId();

            action.Should().Throw<DomainException>();
        }


        [Theory]
        [TestCase(0, 2, "Hst 1.", "Teste de Hst nº 1.", "2022-12-20", 1, null, "2022-12-20")]
        [TestCase(0, 3, "Hst 1.", "Teste de Hst nº 1.", "2022-12-20", 1, "2022-12-20", null)]
        [TestCase(0, 3, "Hst 1.", "Teste de Hst nº 1.", "2022-12-20", 1, "2022-12-20", "2022-12-20")]
        [TestCase(0, 3, "Hst 1.", "Teste de Hst nº 1.", "2022-12-20", 1, null, null)]
        public void CriarHistoricoTarefa_ParametrosValidosSemId_RetornarObjetoValido(int id, int idTarefa, string nome, string descricao, DateTime dataHoraTarefa,
                                                                       int idStatusTarefa, DateTime? dataHoraExclusao, DateTime? dataHoraConclusao)
        {


            Action action = () => new HistoricoTarefa(id, idTarefa, dataHoraExclusao, dataHoraConclusao, nome, descricao,
                                                      dataHoraTarefa, idStatusTarefa).ValidateWithoutId();

            action.Should().NotThrow<DomainException>();

            action.Target.Should().NotBeNull();
        }

        [Theory]
        [TestCase(0, 1, "Teste 1.", "T", "2022-12-20", 1, null, null)]
        [TestCase(0, 2, "Teste 1.", "Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te." +
                  "Te.Te.Te..Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.", "2022-12-20", 1, null, null)]
        [TestCase(0, 3, "T", "Teste de Tarefa nº 1.", "2022-12-20", 1, null, null)]
        [TestCase(0, 4, "Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1" +
                      "Teste 1.", "Te.", "2022-12-20", 1, null, null)]
        [TestCase(0, 5, "Teste 1", "Teste de Tarefa nº 1.", "0001-01-01", 1, null, null)]
        [TestCase(0, 6, "Te.", "Teste de Tarefa nº 1.", "2022-12-20", 5, null, null)]
        public void CriarHistoricoTarefa_ParametrosInvalidos_RetornaExcecao(int id, int idTarefa, string nome, string descricao, DateTime dataHoraTarefa,
                                                                       int idStatusTarefa, DateTime? dataHoraExclusao, DateTime? dataHoraConclusao)
        {

            Action action = () => new HistoricoTarefa(id, idTarefa, dataHoraExclusao, 
                                                      dataHoraConclusao, nome, descricao, dataHoraTarefa, idStatusTarefa).ValidateWithoutId();

            action.Should().Throw<DomainException>();
        }
    }
}
