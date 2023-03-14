using AutoMapper;
using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Application.Interfaces;
using DesafioSistemaTarefas.Domain.Entities;
using DesafioSistemaTarefas.Domain.Enums;
using DesafioSistemaTarefas.Domain.Exceptions;
using DesafioSistemaTarefas.Domain.Inferfaces;
using DesafioSistemaTarefas.Shared.Extensions;
using Microsoft.AspNetCore.Http;
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

        public async Task<TarefaDto> InserirTarefa(TarefaDto dadosTarefa)
        {
            try
            {
                _logger.LogInformation("Start TarefaService to InserirTarefa");

                if (dadosTarefa == null)
                    throw new DomainException("Necessário informar uma tarefa para inserção.");

                var tarefa = _mapper.Map<Tarefa>(dadosTarefa);

                tarefa.SetDataCriacao(System.DateTime.Now);
                tarefa.SetId(0);
                tarefa.SetStatusTarefa((int)EnumStatusTarefa.ATIVA);

                if (tarefa.ValidateWithoutId())
                {
                    tarefa = await _tarefaRepository.Add(tarefa);

                    _logger.LogInformation("Finish TarefaService to InserirTarefa");
                }

                return _mapper.Map<TarefaDto>(tarefa);
            }
            catch (DomainException ex)
            {
                LoggerExtension.LogDomainExceptionError(_logger, "ApiTarefa", "InsertTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (DataBaseException ex)
            {
                LoggerExtension.LogDatabaseExceptionError(_logger, "ApiTarefa", "InsertTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (ApplicationException ex)
            {
                LoggerExtension.LogApplicationExceptionError(_logger, "ApiTarefa", "InsertTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (Exception ex)
            {
                LoggerExtension.LogExceptionError(_logger, "ApiTarefa", "InsertTarefa", ex, ex.Message);
                throw;
            }
        }

        public async Task<TarefaDto> AtualizarTarefa(TarefaDto dadosTarefa)
        {
            try
            {
                _logger.LogInformation("Start TarefaService to AtualizarTarefa");

                if (dadosTarefa == null || !dadosTarefa.id.HasValue || dadosTarefa.id == 0)
                    throw new DomainException("Necessário informar uma tarefa para atualização.");

                var tarefa = _tarefaRepository.GetById(dadosTarefa.id.Value).Result;

                if (tarefa == null || tarefa.Id == 0)
                    throw new DomainException("Nenhuma tarefa encontrada para atualização.");

                dadosTarefa.dataCriacao = tarefa.DataCriacao;
                dadosTarefa.dataAtualizacao = DateTime.Now;
                dadosTarefa.idStatusTarefa = tarefa.IdStatusTarefa;

                tarefa = _mapper.Map<Tarefa>(dadosTarefa);

                tarefa.ValidateWithId();

                await _tarefaRepository.Update(tarefa);
                await _tarefaRepository.Commit();

                tarefa = _tarefaRepository.GetById(tarefa.Id).Result;

                _logger.LogInformation("Finish TarefaService to AtualizarTarefa");

                return _mapper.Map<TarefaDto>(tarefa);
            }
            catch (DomainException ex)
            {
                LoggerExtension.LogDomainExceptionError(_logger, "ApiTarefa", "UpdateTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (DataBaseException ex)
            {
                LoggerExtension.LogDatabaseExceptionError(_logger, "ApiTarefa", "UpdateTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (ApplicationException ex)
            {
                LoggerExtension.LogApplicationExceptionError(_logger, "ApiTarefa", "UpdateTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (Exception ex)
            {
                LoggerExtension.LogExceptionError(_logger, "ApiTarefa", "UpdateTarefa", ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> ExcluirTarefa(int idTarefa)
        {
            try
            {
                _logger.LogInformation("Finish TarefaService to InserirTarefa");
                var dadosTarefa = BuscarTarefaPorId(idTarefa);

                dadosTarefa.SetStatusTarefa((int)EnumStatusTarefa.EXCLUIDA);

                await EnviarTarefaParaHistoricoTarefa(dadosTarefa);
                await DeletarTarefa(idTarefa);
                _logger.LogInformation("Finish TarefaService to InserirTarefa");

                return true;
            }
            catch (DomainException ex)
            {
                LoggerExtension.LogDomainExceptionError(_logger, "ApiTarefa", "DeleteTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (DataBaseException ex)
            {
                LoggerExtension.LogDatabaseExceptionError(_logger, "ApiTarefa", "DeleteTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (ApplicationException ex)
            {
                LoggerExtension.LogApplicationExceptionError(_logger, "ApiTarefa", "DeleteTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (Exception ex)
            {
                LoggerExtension.LogExceptionError(_logger, "ApiTarefa", "DeleteTarefa", ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> ConcluirTarefa(int idTarefa)
        {
            try
            {
                _logger.LogInformation("Start TarefaService to ConcluirTarefa");

                var dadosTarefa = BuscarTarefaPorId(idTarefa);

                if (dadosTarefa == null || dadosTarefa.Id == 0)
                    throw new DomainException("Tarefa informada não encontrada na base.");

                dadosTarefa.SetStatusTarefa((int)EnumStatusTarefa.CONCLUIDA);

                await EnviarTarefaParaHistoricoTarefa(dadosTarefa);
                await DeletarTarefa(idTarefa);

                _logger.LogInformation("Finish TarefaService to ConcluirTarefa");

                return true;
            }
            catch (DomainException ex)
            {
                LoggerExtension.LogDomainExceptionError(_logger, "ApiTarefa", "UpdateConcluirTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (DataBaseException ex)
            {
                LoggerExtension.LogDatabaseExceptionError(_logger, "ApiTarefa", "UpdateConcluirTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (ApplicationException ex)
            {
                LoggerExtension.LogApplicationExceptionError(_logger, "ApiTarefa", "UpdateConcluirTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (Exception ex)
            {
                LoggerExtension.LogExceptionError(_logger, "ApiTarefa", "UpdateConcluirTarefa", ex, ex.Message);
                throw;
            }
        }
        public async Task<TarefaDto> ReativarTarefa(int idTarefa)
        {
            try
            {
                _logger.LogInformation("Start TarefaService to ReativarTarefa");
                var dadosHistoricoTarefa = _historicoTarefaService.BuscarPorIdTarefa(idTarefa).Result;

                if (dadosHistoricoTarefa == null || dadosHistoricoTarefa.id == 0)
                    throw new DomainException("Tarefa informada não encontrada na base histórica.");

                var tarefaDto = await InserirTarefa(_mapper.Map<TarefaDto>(dadosHistoricoTarefa));

                await _historicoTarefaService.Excluir(dadosHistoricoTarefa.id.Value);

                _logger.LogInformation("Finish TarefaService to ReativarTarefa");

                return tarefaDto;
            }
            catch (DomainException ex)
            {
                LoggerExtension.LogDomainExceptionError(_logger, "ApiTarefa", "PostReativarTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (DataBaseException ex)
            {
                LoggerExtension.LogDatabaseExceptionError(_logger, "ApiTarefa", "PostReativarTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (ApplicationException ex)
            {
                LoggerExtension.LogApplicationExceptionError(_logger, "ApiTarefa", "PostReativarTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (Exception ex)
            {
                LoggerExtension.LogExceptionError(_logger, "ApiTarefa", "PostReativarTarefa", ex, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<TarefaDto>> BuscarTarefas()
        {
            try
            {
                _logger.LogInformation("Start TarefaService to BuscarTarefas");

                var listaTarefas = await _tarefaRepository.GetAll();

                if (listaTarefas == null || !listaTarefas.Any())
                    return new List<TarefaDto>();

                _logger.LogInformation("Finish TarefaService to BuscarTarefas");

                return _mapper.Map<IEnumerable<TarefaDto>>(listaTarefas);

            }
            catch (DomainException ex)
            {
                LoggerExtension.LogDomainExceptionError(_logger, "ApiTarefa", "GetTarefas", ex, ex.InnerException?.Message);
                throw;
            }
            catch (DataBaseException ex)
            {
                LoggerExtension.LogDatabaseExceptionError(_logger, "ApiTarefa", "GetTarefas", ex, ex.InnerException?.Message);
                throw;
            }
            catch (ApplicationException ex)
            {
                LoggerExtension.LogApplicationExceptionError(_logger, "ApiTarefa", "GetTarefas", ex, ex.InnerException?.Message);
                throw;
            }
            catch (Exception ex)
            {
                LoggerExtension.LogExceptionError(_logger, "ApiTarefa", "GetTarefas", ex, ex.Message);
                throw;
            }
        }
        public async Task<TarefaDto> BuscarTarefa(int idTarefa)
        {
            try
            {
                _logger.LogInformation("Start TarefaService to BuscarTarefa(int idTarefa)");

                var tarefa = await _tarefaRepository.GetById(idTarefa);

                if (tarefa == null || tarefa.Id == 0)
                    return new TarefaDto();

                _logger.LogInformation("Finish TarefaService to BuscarTarefa(int idTarefa)");

                return _mapper.Map<TarefaDto>(tarefa);
            }
            catch (DomainException ex)
            {
                LoggerExtension.LogDomainExceptionError(_logger, "ApiTarefa", "GetTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (DataBaseException ex)
            {
                LoggerExtension.LogDatabaseExceptionError(_logger, "ApiTarefa", "GetTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (ApplicationException ex)
            {
                LoggerExtension.LogApplicationExceptionError(_logger, "ApiTarefa", "GetTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (Exception ex)
            {
                LoggerExtension.LogExceptionError(_logger, "ApiTarefa", "GetTarefa", ex, ex.Message);
                throw;
            }
        }
        public async Task<IEnumerable<TarefaDto>> BuscarTarefasPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                _logger.LogInformation("Start TarefaService to BuscarTarefasPorPeriodo");

                dataInicial = new DateTime(dataInicial.Year, dataInicial.Month, dataInicial.Day, 0, 0, 0);
                dataFinal = new DateTime(dataFinal.Year, dataFinal.Month, dataFinal.Day, 23, 59, 59);

                var listaTarefas = await _tarefaRepository.GetTarefasByPeriodo(dataInicial, dataFinal);

                if (listaTarefas == null || !listaTarefas.Any())
                    return new List<TarefaDto>();

                _logger.LogInformation("Finish TarefaService to BuscarTarefasPorPeriodo");

                return _mapper.Map<IEnumerable<TarefaDto>>(listaTarefas);
            }
            catch (DomainException ex)
            {
                LoggerExtension.LogDomainExceptionError(_logger, "ApiTarefa", "GetTarefasPorPeriodo", ex, ex.InnerException?.Message);
                throw;
            }
            catch (DataBaseException ex)
            {
                LoggerExtension.LogDatabaseExceptionError(_logger, "ApiTarefa", "GetTarefasPorPeriodo", ex, ex.InnerException?.Message);
                throw;
            }
            catch (ApplicationException ex)
            {
                LoggerExtension.LogApplicationExceptionError(_logger, "ApiTarefa", "GetTarefasPorPeriodo", ex, ex.InnerException?.Message);
                throw;
            }
            catch (Exception ex)
            {
                LoggerExtension.LogExceptionError(_logger, "ApiTarefa", "GetTarefasPorPeriodo", ex, ex.Message);
                throw;
            }
        }

        private async Task EnviarTarefaParaHistoricoTarefa(Tarefa tarefa)
        {
            var historicoTarefa = _mapper.Map<HistoricoTarefaDto>(tarefa);

            _ = await _historicoTarefaService.Inserir(historicoTarefa);
        }

        private Tarefa BuscarTarefaPorId(int idTarefa)
        {
            var dadosTarefa = _tarefaRepository.GetById(idTarefa).Result;

            if (dadosTarefa == null || dadosTarefa.Id == 0)
                throw new DomainException("Tarefa informada não encontrada na base.");

            return dadosTarefa;
        }

        private async Task DeletarTarefa(int idTarefa)
        {
            var dadosTarefa = BuscarTarefaPorId(idTarefa);
            if (dadosTarefa == null || dadosTarefa.Id == 0)
                throw new DomainException("Tarefa informada não encontrada na base.");
            await _tarefaRepository.Delete(dadosTarefa);
        }
    }
}
