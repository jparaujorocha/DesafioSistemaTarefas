using AutoMapper;
using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Application.Interfaces;
using DesafioSistemaTarefas.Domain.Entities;
using DesafioSistemaTarefas.Domain.Enums;
using DesafioSistemaTarefas.Domain.Exceptions;
using DesafioSistemaTarefas.Domain.Inferfaces;
using Microsoft.Extensions.Logging;

namespace DesafioSistemaTarefas.Application.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly IHistoricoTarefaService _historicoTarefaService;
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public TarefaService(IHistoricoTarefaService historicoTarefaService, ITarefaRepository tarefaRepository,
                             IMapper mapper, ILogger<TarefaService> logger)
        {
            _historicoTarefaService = historicoTarefaService;
            _tarefaRepository = tarefaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public TarefaDto InserirTarefa(TarefaDto dadosTarefa)
        {
            try
            {
                _logger.LogInformation("Start TarefaService to InserirTarefa");

                if (dadosTarefa == null)
                    throw new Exception("Necessário informar uma tarefa para inserção.");

                var tarefa = _mapper.Map<Tarefa>(dadosTarefa);

                tarefa.SetDataCriacao(System.DateTime.Now);
                tarefa.SetId(0);
                tarefa.SetStatusTarefa((int)EnumStatusTarefa.ATIVA);

                var validacao = tarefa.ValidateWithoutId();
                if (!validacao.IsValid)
                {
                    throw new Exception("Erro: " + validacao.Errors[0].ErrorMessage);
                }

                tarefa = _tarefaRepository.Add(tarefa).Result;

                _tarefaRepository.Commit();

                _logger.LogInformation("Finish TarefaService to InserirTarefa");

                return _mapper.Map<TarefaDto>(tarefa);
            }
            catch (DomainException ex)
            {
                throw;
            }
            catch (DataBaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao inserir tarefa", innerException: ex);
            }
        }

        public TarefaDto AtualizarTarefa(TarefaDto dadosTarefa)
        {
            try
            {
                _logger.LogInformation("Start TarefaService to AtualizarTarefa");

                if (dadosTarefa == null || !dadosTarefa.Id.HasValue || dadosTarefa.Id == 0)
                    throw new Exception("Necessário informar uma tarefa para atualização.");

                var tarefa = _tarefaRepository.GetById(dadosTarefa.Id.Value).Result;

                if (tarefa == null || tarefa.Id == 0)
                    throw new Exception("Nenhuma tarefa encontrada para atualização.");

                dadosTarefa.DataCriacao = tarefa.DataCriacao;
                dadosTarefa.DataAtualizacao = DateTime.Now;

                tarefa = _mapper.Map<Tarefa>(dadosTarefa);

                tarefa.ValidateWithId();

                _tarefaRepository.Update(tarefa);

                tarefa = _tarefaRepository.GetById(tarefa.Id).Result;

                _logger.LogInformation("Finish TarefaService to AtualizarTarefa");

                return _mapper.Map<TarefaDto>(tarefa);
            }
            catch (DomainException ex)
            {
                throw;
            }
            catch (DataBaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao atualizar tarefa", innerException: ex);
            }
        }

        public void ExcluirTarefa(int idTarefa)
        {
            try
            {
                _logger.LogInformation("Finish TarefaService to InserirTarefa");
                var dadosTarefa = BuscarTarefaPorId(idTarefa);

                dadosTarefa.SetStatusTarefa((int)EnumStatusTarefa.EXCLUIDA);

                EnviarTarefaParaHistoricoTarefa(dadosTarefa);
                DeletarTarefa(idTarefa);
                _logger.LogInformation("Finish TarefaService to InserirTarefa");
            }
            catch (DomainException ex)
            {
                throw;
            }
            catch (DataBaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao excluir tarefa", innerException: ex);
            }
        }

        public void ConcluirTarefa(int idTarefa)
        {
            try
            {
                _logger.LogInformation("Start TarefaService to ConcluirTarefa");
                var dadosTarefa = BuscarTarefaPorId(idTarefa);

                dadosTarefa.SetStatusTarefa((int)EnumStatusTarefa.CONCLUIDA);

                EnviarTarefaParaHistoricoTarefa(dadosTarefa);
                DeletarTarefa(idTarefa);
                _logger.LogInformation("Finish TarefaService to ConcluirTarefa");
            }
            catch (DomainException ex)
            {
                throw;
            }
            catch (DataBaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao concluir tarefa", innerException: ex);
            }
        }
        public TarefaDto ReativarTarefa(int idTarefa)
        {
            try
            {
                _logger.LogInformation("Start TarefaService to ReativarTarefa");
                var dadosHistoricoTarefa = _historicoTarefaService.BuscarPorIdTarefa(idTarefa);

                if (dadosHistoricoTarefa == null || dadosHistoricoTarefa.Id == 0)
                    throw new Exception("Tarefa informada não encontrada na base histórica.");

                var tarefaDto = InserirTarefa(_mapper.Map<TarefaDto>(dadosHistoricoTarefa));

                _historicoTarefaService.ExcluirHistoricoTarefa(dadosHistoricoTarefa.Id.Value);
                _logger.LogInformation("Finish TarefaService to ReativarTarefa");

                return tarefaDto;
            }
            catch (DomainException ex)
            {
                throw;
            }
            catch (DataBaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao reativar tarefa", innerException: ex);
            }
        }

        public IEnumerable<TarefaDto> BuscarTarefas()
        {
            try
            {
                _logger.LogInformation("Start TarefaService to BuscarTarefas");

                var listaTarefas = _tarefaRepository.GetAll().Result;

                if (listaTarefas == null || !listaTarefas.Any())
                    return new List<TarefaDto>();

                _logger.LogInformation("Finish TarefaService to BuscarTarefas");

                return _mapper.Map<IEnumerable<TarefaDto>>(listaTarefas);

            }
            catch (DomainException ex)
            {
                throw;
            }
            catch (DataBaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao buscar tarefas", innerException: ex);
            }
        }
        public TarefaDto BuscarTarefa(int idTarefa)
        {
            try
            {
                _logger.LogInformation("Start TarefaService to BuscarTarefa(int idTarefa)");

                var tarefa = _tarefaRepository.GetById(idTarefa);

                if (tarefa == null || tarefa.Id == 0)
                    return new TarefaDto();

                _logger.LogInformation("Finish TarefaService to BuscarTarefa(int idTarefa)");

                return _mapper.Map<TarefaDto>(tarefa);
            }
            catch (DomainException ex)
            {
                throw;
            }
            catch (DataBaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao buscar tarefa", innerException: ex);
            }
        }

        public IEnumerable<TarefaDto> BuscarTarefasPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                _logger.LogInformation("Start TarefaService to BuscarTarefasPorPeriodo");

                dataInicial = new DateTime(dataInicial.Year, dataInicial.Month, dataInicial.Day, 0, 0, 0);
                dataFinal = new DateTime(dataFinal.Year, dataFinal.Month, dataFinal.Day, 23, 59, 59);

                var listaTarefas = _tarefaRepository.GetTarefasByPeriodo(dataInicial, dataFinal).Result;

                if (listaTarefas == null || !listaTarefas.Any())
                    return new List<TarefaDto>();

                _logger.LogInformation("Finish TarefaService to BuscarTarefasPorPeriodo");

                return _mapper.Map<IEnumerable<TarefaDto>>(listaTarefas);
            }

            catch (DomainException ex)
            {
                throw;
            }
            catch (DataBaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao buscar tarefa por período", innerException: ex);
            }
        }

        private void EnviarTarefaParaHistoricoTarefa(Tarefa tarefa)
        {
            var historicoTarefa = _mapper.Map<HistoricoTarefaDto>(tarefa);

            _ = _historicoTarefaService.InserirHistoricoTarefa(historicoTarefa);
        }

        private Tarefa BuscarTarefaPorId(int idTarefa)
        {
            var dadosTarefa = _tarefaRepository.GetById(idTarefa).Result;

            if (dadosTarefa == null || dadosTarefa.Id == 0)
                throw new Exception("Tarefa informada não encontrada na base.");

            return dadosTarefa;
        }

        private void DeletarTarefa(int idTarefa)
        {
            var dadosTarefa = BuscarTarefaPorId(idTarefa);
            if (dadosTarefa == null || dadosTarefa.Id == 0)
                throw new Exception("Tarefa informada não encontrada na base.");
            _tarefaRepository.Delete(dadosTarefa);
            _tarefaRepository.Commit();
        }
    }
}
