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
    public class HistoricoTarefaService : IHistoricoTarefaService
    {
        private readonly IHistoricoTarefaRepository _historicoTarefaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public HistoricoTarefaService(IHistoricoTarefaRepository historicoTarefaRepository, IMapper mapper, ILogger<HistoricoTarefaService> logger)
        {
            _historicoTarefaRepository = historicoTarefaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<HistoricoTarefaDto>> BuscarLista()
        {
            try
            {
                _logger.LogInformation("Start HistoricoTarefaService to BuscarHistoricoTarefas");
                var listaHistoricoTarefa = await _historicoTarefaRepository.GetAll();
                if (listaHistoricoTarefa == null || !listaHistoricoTarefa.Any())
                    return new List<HistoricoTarefaDto>();

                _logger.LogInformation("Finish HistoricoTarefaService to BuscarHistoricoTarefas");
                return _mapper.Map<IEnumerable<HistoricoTarefaDto>>(listaHistoricoTarefa);
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
                throw new ApplicationException("Erro ao buscar histórico de tarefas", innerException: ex);
            }
        }

        public async Task<HistoricoTarefaDto> BuscarPorIdTarefa(int idTarefa)
        {
            try
            {
                _logger.LogInformation("Start HistoricoTarefaService to BuscarPorIdTarefa");
                var historicoTarefa = await _historicoTarefaRepository.Get(a => a.IdTarefa == idTarefa);
                if (historicoTarefa == null || historicoTarefa.Id == 0)
                    return new HistoricoTarefaDto();

                _logger.LogInformation("Finish HistoricoTarefaService to BuscarPorIdTarefa");
                return _mapper.Map<HistoricoTarefaDto>(historicoTarefa);
            }
            catch (DomainException ex)
            {
                LoggerExtension.LogDomainExceptionError(_logger, "HistoricoTarefaService", "GetByIdTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (DataBaseException ex)
            {
                LoggerExtension.LogDatabaseExceptionError(_logger, "HistoricoTarefaService", "GetByIdTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (ApplicationException ex)
            {
                LoggerExtension.LogApplicationExceptionError(_logger, "HistoricoTarefaService", "GetByIdTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (Exception ex)
            {
                LoggerExtension.LogExceptionError(_logger, "HistoricoTarefaService", "GetByIdTarefa", ex, ex.Message);
                throw;
            }
        }

        public async Task<HistoricoTarefaDto> BuscarPorId(int id)
        {
            try
            {
                _logger.LogInformation("Start HistoricoTarefaService to BuscarPorId");

                var historicoTarefa = await _historicoTarefaRepository.GetById(id);
                if (historicoTarefa == null || historicoTarefa.Id == 0)
                    return new HistoricoTarefaDto();

                _logger.LogInformation("Finish HistoricoTarefaService to BuscarPorId");
                return _mapper.Map<HistoricoTarefaDto>(historicoTarefa);
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
                throw new ApplicationException("Erro ao buscar histórico de tarefa por Id", innerException: ex);
            }
        }

        public async Task<HistoricoTarefaDto> Inserir(HistoricoTarefaDto dadosHistoricoTarefa)
        {
            try
            {
                _logger.LogInformation("Start HistoricoTarefaService to InserirTarefa");

                if (dadosHistoricoTarefa == null)
                    throw new DomainException("Necessário informar uma tarefa do histórico para inserção.");

                var historicoTarefa = _mapper.Map<HistoricoTarefa>(dadosHistoricoTarefa);

                historicoTarefa.Id = 0;
                SetDataHoraExclusaoConclusao(historicoTarefa);
                historicoTarefa.SetDataCriacao(DateTime.Now);

                if (historicoTarefa.ValidateWithoutId())
                {
                    historicoTarefa = await _historicoTarefaRepository.Add(historicoTarefa);
                }

                _logger.LogInformation("Finish HistoricoTarefaService to InserirHistoricoTarefa");
                return _mapper.Map<HistoricoTarefaDto>(historicoTarefa);
            }
            catch (DomainException ex)
            {
                LoggerExtension.LogDomainExceptionError(_logger, "HistoricoTarefaService", "InsertHistoricoTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (DataBaseException ex)
            {
                LoggerExtension.LogDatabaseExceptionError(_logger, "HistoricoTarefaService", "InsertHistoricoTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (ApplicationException ex)
            {
                LoggerExtension.LogApplicationExceptionError(_logger, "HistoricoTarefaService", "InsertHistoricoTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (Exception ex)
            {
                LoggerExtension.LogExceptionError(_logger, "HistoricoTarefaService", "InsertHistoricoTarefa", ex, ex.Message);
                throw;
            }
        }


        public async Task<bool> Excluir(int idHistoricoTarefa)
        {
            try
            {
                _logger.LogInformation("Start HistoricoTarefaService to ExcluirHistoricoTarefa");
                var dadosHistoricoTarefa = _historicoTarefaRepository.GetById(idHistoricoTarefa).Result;

                if (dadosHistoricoTarefa == null || dadosHistoricoTarefa.Id == 0)
                    throw new DomainException("Necessário informar um registro válido para exclusão.");

                await _historicoTarefaRepository.Delete(dadosHistoricoTarefa);
                

                _logger.LogInformation("Finish HistoricoTarefaService to ExcluirHistoricoTarefa");

                return true;
            }
            catch (DomainException ex)
            {
                LoggerExtension.LogDomainExceptionError(_logger, "HistoricoTarefaService", "DeleteHistoricoTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (DataBaseException ex)
            {
                LoggerExtension.LogDatabaseExceptionError(_logger, "HistoricoTarefaService", "DeleteHistoricoTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (ApplicationException ex)
            {
                LoggerExtension.LogApplicationExceptionError(_logger, "HistoricoTarefaService", "DeleteHistoricoTarefa", ex, ex.InnerException?.Message);
                throw;
            }
            catch (Exception ex)
            {
                LoggerExtension.LogExceptionError(_logger, "HistoricoTarefaService", "DeleteHistoricoTarefa", ex, ex.Message);
                throw;
            }
        }
        private void SetDataHoraExclusaoConclusao(HistoricoTarefa historicoTarefa)
        {
            switch (historicoTarefa.IdStatusTarefa)
            {
                case (int)EnumStatusTarefa.EXCLUIDA:
                    historicoTarefa.SetDataHoraExclusao(DateTime.Now);
                    break;
                case (int)EnumStatusTarefa.CONCLUIDA:
                    historicoTarefa.SetDataHoraConclusao(DateTime.Now);
                    break;
            }
        }

    }
}
