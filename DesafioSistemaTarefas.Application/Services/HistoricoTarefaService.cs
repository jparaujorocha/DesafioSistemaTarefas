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

        public IEnumerable<HistoricoTarefaDto> BuscarHistoricoTarefas()
        {
            try
            {
                _logger.LogInformation("Start HistoricoTarefaService to BuscarHistoricoTarefas");
                var listaHistoricoTarefa = _historicoTarefaRepository.GetAll().Result;
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

        public HistoricoTarefaDto BuscarPorIdTarefa(int idTarefa)
        {
            try
            {
                _logger.LogInformation("Start HistoricoTarefaService to BuscarPorIdTarefa");
                var historicoTarefa = _historicoTarefaRepository.Get(a => a.IdTarefa == idTarefa);
                if (historicoTarefa == null || historicoTarefa.Id == 0)
                    return new HistoricoTarefaDto();

                _logger.LogInformation("Finish HistoricoTarefaService to BuscarPorIdTarefa");
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
                throw new ApplicationException("Erro ao buscar histórico de tarefa por IdTarefa", innerException: ex);
            }
        }

        public HistoricoTarefaDto BuscarPorId(int id)
        {
            try
            {
                _logger.LogInformation("Start HistoricoTarefaService to BuscarPorId");

                var historicoTarefa = _historicoTarefaRepository.GetById(id);
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

        public HistoricoTarefaDto InserirHistoricoTarefa(HistoricoTarefaDto dadosHistoricoTarefa)
        {
            try
            {
                _logger.LogInformation("Start HistoricoTarefaService to InserirTarefa");
                if (dadosHistoricoTarefa == null)
                    throw new Exception("Necessário informar uma tarefa do histórico para inserção.");

                dadosHistoricoTarefa.Id = 0;

                var historicoTarefa = _mapper.Map<HistoricoTarefa>(dadosHistoricoTarefa);

                SetDataHoraExclusaoConclusao(historicoTarefa);
                historicoTarefa.SetDataCriacao(DateTime.Now);

                _ = historicoTarefa.ValidateWithoutId();

                historicoTarefa = _historicoTarefaRepository.Add(historicoTarefa).Result;
                _historicoTarefaRepository.Commit();

                _logger.LogInformation("Finish HistoricoTarefaService to InserirHistoricoTarefa");
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

        public void ExcluirHistoricoTarefa(int idHistoricoTarefa)
        {
            try
            {
                _logger.LogInformation("Start HistoricoTarefaService to ExcluirHistoricoTarefa");
                var dadosHistoricoTarefa = _historicoTarefaRepository.GetById(idHistoricoTarefa).Result;

                if (dadosHistoricoTarefa == null || dadosHistoricoTarefa.Id == 0)
                    throw new Exception("Necessário informar uma tarefa do histórico para exclusão.");

                _historicoTarefaRepository.Delete(dadosHistoricoTarefa);
                _historicoTarefaRepository.Commit();

                _logger.LogInformation("Finish HistoricoTarefaService to ExcluirHistoricoTarefa");
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
                throw new ApplicationException("Erro ao excluir histórico de tarefa", innerException: ex);
            }
        }

    }
}
