using DesafioSistemaTarefas.Domain.Entities;
using DesafioSistemaTarefas.Domain.Enums;
using DesafioSistemaTarefas.Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace DesafioSistemaTarefas.Test.DomainTests
{
    public class TarefaTest
    {
        [Theory]
        [TestCase(1, "Teste 1.", "Teste de Tarefa nº 1.", "2024-12-20", 1)]
        public void SetarParametrosTarefa_ParametrosValidos_RetornarObjetoValido(int idTarefa, string nome, string descricao, DateTime dataHoraTarefa,
                                                                       int idStatusTarefa)
        {

            Tarefa dadosTarefa = new Tarefa(idTarefa, nome, descricao, dataHoraTarefa, idStatusTarefa);
            dadosTarefa.SetStatusTarefa((int)EnumStatusTarefa.CONCLUIDA);
            dadosTarefa.SetDataHoraTarefa(DateTime.Now.AddDays(15));
            dadosTarefa.SetDescricao("Teste tarefa nº 2");
            dadosTarefa.SetNome("Teste nº 2");
            dadosTarefa.SetId(2);

            Action action = () => dadosTarefa.ValidateWithId();

            action.Should().NotThrow<DomainException>();

            action.Target.Should().NotBeNull();
        }
        [Theory]
        [TestCase(1, "Teste 1.", "Teste de Tarefa nº 1.", "2024-12-20", 1)]
        public void SetarParametrosTarefa_ParametrosValidosIdZero_RetornarObjetoValido(int idTarefa, string nome, string descricao, DateTime dataHoraTarefa,
                                                                       int idStatusTarefa)
        {

            Tarefa dadosTarefa = new Tarefa(idTarefa, nome, descricao, dataHoraTarefa, idStatusTarefa);
            dadosTarefa.SetStatusTarefa((int)EnumStatusTarefa.CONCLUIDA);
            dadosTarefa.SetDataHoraTarefa(DateTime.Now.AddDays(15));
            dadosTarefa.SetDescricao("Teste tarefa nº 3");
            dadosTarefa.SetNome("Teste nº 3");
            dadosTarefa.SetId(0);

            Action action = () => dadosTarefa.ValidateWithoutId();

            action.Should().NotThrow<DomainException>();

            action.Target.Should().NotBeNull();
        }
        [Theory]
        [TestCase(1, "Teste 1.", "Teste de Tarefa nº 1.", "2024-12-20", 1)]
        public void SetarTarefa_ParametrosInvalidos_RetornaExcecao(int idTarefa, string nome, string descricao, DateTime dataHoraTarefa,
                                                                       int idStatusTarefa)
        {

            Tarefa dadosTarefa = new Tarefa(idTarefa, nome, descricao, dataHoraTarefa, idStatusTarefa);
            dadosTarefa.SetStatusTarefa((int)EnumStatusTarefa.CONCLUIDA);
            dadosTarefa.SetDataHoraTarefa(DateTime.Now.AddDays(15));
            dadosTarefa.SetDescricao("Teste tarefa nº 3 Com ERRO!Teste tarefa nº 3 Com ERRO!Teste tarefa nº 3 Com ERRO!Teste tarefa nº 3 Com ERRO!Teste tarefa nº 3 Com ERRO!Teste tarefa nº 3 Com ERRO!Teste tarefa nº 3 Com ERRO!");
            dadosTarefa.SetNome("Teste nº 3 COM erROTeste nº 3 COM erROTeste nº 3 COM erRO");
            dadosTarefa.SetId(0);

            Action action = () => dadosTarefa.ValidateWithoutId();

            action.Should().Throw<DomainException>();
        }

        [Theory]
        [TestCase(1, "Teste 1.", "Teste de Tarefa nº 1.", "2024-12-20", 1)]
        public void CriarTarefa_ParametrosValidos_RetornarObjetoValido(int idTarefa, string nome, string descricao, DateTime dataHoraTarefa,
                                                                       int idStatusTarefa)
        {

            Action action = () => new Tarefa(idTarefa, nome, descricao, dataHoraTarefa, idStatusTarefa).ValidateWithId();

            action.Should().NotThrow<DomainException>();

            action.Target.Should().NotBeNull();
        }

        [TestCase(0, "Teste 1.", "Teste de Tarefa nº 1.", "2024-12-20", 1)]
        public void CriarTarefa_ParametrosValidosIdNulo_RetornarObjetoValido(int idTarefa, string nome, string descricao, DateTime dataHoraTarefa,
                                                                       int idStatusTarefa)
        {

            Action action = () => new Tarefa(idTarefa, nome, descricao, dataHoraTarefa, idStatusTarefa).ValidateWithoutId();

            action.Should().NotThrow<DomainException>();

            action.Target.Should().NotBeNull();
        }
        [Theory]
        [TestCase(1, "Teste 1.", "T", "2022-12-20", 1)]
        [TestCase(1, "Teste 1.", "Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te." +
                  "Te.Te.Te..Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.Te.", "2022-12-20", 1)]
        [TestCase(1, "T", "Teste de Tarefa nº 1.", "2022-12-20", 1)]
        [TestCase(1, "Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1Teste 1" +
                      "Teste 1.", "Te.", "2022-12-20", 1)]
        [TestCase(1, "Teste 1", "Teste de Tarefa nº 1.", "0001-01-01", 1)]
        [TestCase(1, "Te.", "Teste de Tarefa nº 1.", "2022-12-20", 5)]
        public void CriarTarefa_ParametrosInvalidos_RetornaExcecao(int idTarefa, string nome, string descricao, DateTime dataHoraTarefa,
                                                                       int idStatusTarefa)
        {

            Action action = () => new Tarefa(idTarefa, nome, descricao, dataHoraTarefa, idStatusTarefa).ValidateWithId();

            action.Should().Throw<DomainException>();

        }
    }
}
