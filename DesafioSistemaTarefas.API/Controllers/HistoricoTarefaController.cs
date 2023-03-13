using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Application.Interfaces;
using DesafioSistemaTarefas.Domain.Exceptions;
using DesafioSistemaTarefas.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DesafioSistemaTarefas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoTarefaController : ControllerBase
    {
        private readonly IHistoricoTarefaService _historicoTarefaService;
        private readonly ILogger _logger;
        public HistoricoTarefaController(IHistoricoTarefaService historicoTarefaService, ILogger<HistoricoTarefaController> logger)
        {
            _historicoTarefaService = historicoTarefaService;
            _logger = logger;
        }

        [Route("GetHistoricoTarefas")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoricoTarefaDto>>> Get()
        {
            try
            {
                _logger.LogInformation("Start ApiHistoricoTarefa to GetHistoricoTarefas");

                var listaHistoricoTarefas = await _historicoTarefaService.BuscarLista();
                if (listaHistoricoTarefas == null || !listaHistoricoTarefas.Any())
                {
                    _logger.LogWarning("Finish ApiHistoricoTarefa to GetHistoricoTarefas: Nenhum Historico da Tarefa Encontrado.");

                    return NotFound("Nenhum Historico da Tarefa Encontrado.");
                }

                _logger.LogInformation("Finish ApiHistoricoTarefa to GetHistoricoTarefas");

                return Ok(listaHistoricoTarefas);
            }
            catch (DomainException ex)
            {
                LoggerExtension.LogDomainExceptionError(_logger, "ApiHistoricoTarefa", "GetHistoricoTarefas", ex, ex.InnerException?.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "DomainError ao recuperar historico da tarefa.");
            }
            catch (DataBaseException ex)
            {
                LoggerExtension.LogDatabaseExceptionError(_logger, "ApiHistoricoTarefa", "GetHistoricoTarefas", ex, ex.InnerException?.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "DbError ao recuperar historico da tarefa.");
            }
            catch (ApplicationException ex)
            {
                LoggerExtension.LogApplicationExceptionError(_logger, "ApiHistoricoTarefa", "GetHistoricoTarefas", ex, ex.InnerException?.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "ApplicationError ao recuperar historico da tarefa.");
            }
            catch (Exception ex)
            {
                LoggerExtension.LogExceptionError(_logger, "ApiHistoricoTarefa", "GetHistoricoTarefas", ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar historico da tarefa.");
            }
        }

        [Route("GetHistoricoTarefa/{id}")]
        [HttpGet]
        public async Task<ActionResult<HistoricoTarefaDto>> Get(int id)
        {
            try
            {
                _logger.LogInformation("Start ApiHistoricoTarefa to GetHistoricoTarefa");
                var historicoTarefa = await _historicoTarefaService.BuscarPorId(id);
                if (historicoTarefa == null || !historicoTarefa.Id.HasValue || historicoTarefa.Id == 0)
                {
                    _logger.LogWarning("Finish ApiHistoricoTarefa to GetHistoricoTarefa: Histórico da tarefa não encontrado.");
                    return NotFound("Histórico da tarefa não encontrado.");
                }
                _logger.LogInformation("Finish ApiHistoricoTarefa to GetHistoricoTarefa");
                return Ok(historicoTarefa);
            }
            catch (DomainException ex)
            {
                LoggerExtension.LogDomainExceptionError(_logger, "ApiHistoricoTarefa", "GetHistoricoTarefa", ex, ex.InnerException?.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "DomainError ao recuperar historico da tarefa.");
            }
            catch (DataBaseException ex)
            {
                LoggerExtension.LogDatabaseExceptionError(_logger, "ApiHistoricoTarefa", "GetHistoricoTarefa", ex, ex.InnerException?.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "DbError ao recuperar historico da tarefa.");
            }
            catch (ApplicationException ex)
            {
                LoggerExtension.LogApplicationExceptionError(_logger, "ApiHistoricoTarefa", "GetHistoricoTarefa", ex, ex.InnerException?.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "ApplicationError ao recuperar historico da tarefa.");
            }
            catch (Exception ex)
            {
                LoggerExtension.LogExceptionError(_logger, "ApiHistoricoTarefa", "GetHistoricoTarefa", ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar historico da tarefa.");
            }
        }

        [Route("GetByIdTarefa/{idTarefa}")]
        [HttpGet]
        public async Task<ActionResult<HistoricoTarefaDto>> GetByIdTarefa(int idTarefa)
        {
            try
            {
                _logger.LogInformation("Start ApiHistoricoTarefa to GetByIdTarefa");
                var historicoTarefa = await _historicoTarefaService.BuscarPorIdTarefa(idTarefa);

                if (historicoTarefa == null || !historicoTarefa.Id.HasValue || historicoTarefa.Id == 0)
                {
                    _logger.LogWarning("Finish ApiHistoricoTarefa to GetByIdTarefa: Histórico da tarefa não encontrado.");
                    return NotFound("Histórico da tarefa não encontrado.");
                }
                _logger.LogInformation("Finish ApiHistoricoTarefa to GetByIdTarefa");
                return Ok(historicoTarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar historico da tarefa por id tarefa.");
            }
        }

        [Route("InsertHistoricoTarefa")]
        [HttpPost]
        public async Task<ActionResult<HistoricoTarefaDto>> Post([FromBody] HistoricoTarefaDto historicoTarefaDto)
        {
            try
            {
                _logger.LogInformation("Start ApiHistoricoTarefa to InsertHistoricoTarefa");

                if (historicoTarefaDto == null)
                {
                    _logger.LogWarning("Finish ApiHistoricoTarefa to InsertHistoricoTarefa: Dados do histórico da tarefa inválidos.");
                    return BadRequest("Dados do histórico da tarefa inválidos.");
                }

                historicoTarefaDto = await _historicoTarefaService.Inserir(historicoTarefaDto);

                _logger.LogInformation("Finish ApiHistoricoTarefa to InsertHistoricoTarefa");
                return StatusCode(StatusCodes.Status201Created, historicoTarefaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir historico da tarefa.");
            }
        }

        [Route("DeleteHistoricoTarefa/{idHistoricoTarefa}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int idHistoricoTarefa)
        {
            try
            {
                _logger.LogInformation("Finish ApiHistoricoTarefa to DeleteHistoricoTarefa");

                if (idHistoricoTarefa == 0)
                {
                    _logger.LogWarning("Finish ApiHistoricoTarefa to DeleteHistoricoTarefa: Histórico da Tarefa enviado para exclusão inválido.");
                    return BadRequest("Histórico da Tarefa enviado para exclusão inválido.");
                }

                await _historicoTarefaService.Excluir(idHistoricoTarefa);

                _logger.LogInformation("Finish ApiHistoricoTarefa to DeleteHistoricoTarefa");
                return Ok("Historico Apagado com Sucesso.");

            }
            catch (Exception ex)
            {             
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar historico da tarefa.");
            }
        }
    }
}
