﻿using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Application.Interfaces;
using Moq;

namespace DesafioSistemaTarefas.Test.Mocks.Services
{
    public class MockHistoricoTarefaService : Mock<IHistoricoTarefaService>
    {
        public void MockBuscarHistoricoTarefaPorId(int idHistoricoTarefa, HistoricoTarefaDto output)
        {
            Setup(x => x.BuscarPorId(It.Is<int>(i => i == idHistoricoTarefa))).Returns(Task.FromResult(output));
        }
        public void MockBuscarHistoricoTarefaPorIdTarefa(int idTarefa, HistoricoTarefaDto output)
        {
            Setup(x => x.BuscarPorIdTarefa(It.Is<int>(i => i == idTarefa))).Returns(Task.FromResult(output));
        }
        public void MockBuscarHistoricoTarefaPorIdStatus(int idStatus, IEnumerable<HistoricoTarefaDto> output)
        {
            Setup(x => x.BuscarPorIdStatus(It.Is<int>(i => i == idStatus))).Returns(Task.FromResult(output));
        }
        public void MockBuscarHistoricoTarefas(IEnumerable<HistoricoTarefaDto> output)
        {
            Setup(x => x.BuscarLista()).Returns(Task.FromResult(output));
        }
        public void MockInserirHistoricoTarefa(HistoricoTarefaDto dadosEntrada, HistoricoTarefaDto output)
        {
            Setup(x => x.Inserir(It.Is<HistoricoTarefaDto>(a => a == dadosEntrada))).Returns(Task.FromResult(output));
        }
        public void MockExcluirHistoricoTarefa(int idHistoricoTarefa, bool output)
        {
            Setup(x => x.Excluir(It.Is<int>(a => a == idHistoricoTarefa))).Returns(Task.FromResult(output));
        }
    }
}
