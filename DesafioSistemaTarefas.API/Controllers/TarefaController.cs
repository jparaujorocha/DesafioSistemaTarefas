using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Application.Interfaces;
using DesafioSistemaTarefas.Domain.Exceptions;
using DesafioSistemaTarefas.Shared.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DesafioSistemaTarefas.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;
        private readonly ILogger _logger;

        public TarefaController(ITarefaService tarefaService, ILogger<TarefaController> logger)
        {
            _tarefaService = tarefaService;
            _logger = logger;
        }

        [Route("GetTarefas")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaDto>>> GetTarefas()
        {
            try
            {
                _logger.LogInformation("Start ApiTarefa to GetTarefas");

                var listaTarefas = await _tarefaService.BuscarTarefas();
                if (listaTarefas == null || !listaTarefas.Any())
                {
                    _logger.LogWarning("Finish ApiTarefa to GetTarefas: Nenhuma Tarefa Encontrada.");

                    return NotFound("Nenhuma Tarefa Encontrada.");
                }

                _logger.LogInformation("Finish ApiTarefa to GetTarefas");

                return Ok(listaTarefas);
            }
            catch (Exception ex)
            {             
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar tarefas.");
            }
        }

        [Route("GetTarefasPorPeriodo")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaDto>>> GetTarefasPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                _logger.LogInformation("Start ApiTarefa to GetTarefasPorPeriodo");

                var listaTarefas = await _tarefaService.BuscarTarefasPorPeriodo(dataInicial, dataFinal);
                if (listaTarefas == null || !listaTarefas.Any())
                {
                    _logger.LogWarning("Finish ApiTarefa to GetTarefasPorPeriodo: Nenhuma Tarefa Encontrada.");
                    return NotFound("Nenhuma Tarefa Encontrada.");
                }

                _logger.LogInformation("Finish ApiTarefa to GetTarefasPorPeriodo");

                return Ok(listaTarefas);

            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar tarefas por período.");
            }
        }

        [Route("GetTarefa/{id}")]
        [HttpGet]
        public async Task<ActionResult<TarefaDto>> GetTarefa(int id)
        {
            try
            {
                _logger.LogInformation("Start ApiTarefa to GetTarefa");

                var tarefa = await _tarefaService.BuscarTarefa(id);
                if (tarefa == null || tarefa.id == 0)
                {
                    _logger.LogWarning("Finish ApiTarefa to GetTarefa: Tarefa não encontrada.");
                    return NotFound("Tarefa não encontrada.");
                }
                _logger.LogInformation("Finish ApiTarefa to GetTarefa");
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar tarefa.");
            }
        }

        [Route("InsertTarefa")]
        [HttpPost]
        public async Task<ActionResult<TarefaDto>> InsertTarefa([FromBody] TarefaDto tarefaDto)
        {
            try
            {
                _logger.LogInformation("Start ApiTarefa to InsertTarefa");

                if (tarefaDto == null)
                {
                    _logger.LogWarning("Finish ApiTarefa to GetTarefa: Dados Inválidos.");
                    return BadRequest("Dados Inválidos.");
                }
                tarefaDto = await _tarefaService.InserirTarefa(tarefaDto);

                _logger.LogInformation("Finish ApiTarefa to InsertTarefa");

                return StatusCode(StatusCodes.Status201Created, tarefaDto);
            }
            catch (Exception ex)
            {             
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir tarefa.");
            }
        }

        [Route("UpdateTarefa")]
        [HttpPut]
        public async Task<ActionResult<TarefaDto>> UpdateTarefa([FromBody] TarefaDto tarefaDto)
        {
            try
            {
                _logger.LogInformation("Start ApiTarefa to UpdateTarefa");
                if (tarefaDto == null || tarefaDto.id == 0)
                {
                    _logger.LogWarning("Finish ApiTarefa to UpdateTarefa: Tarefa enviada para atualização inválida.");
                    return BadRequest("Tarefa enviada para atualização inválida.");
                }

                tarefaDto = await _tarefaService.AtualizarTarefa(tarefaDto);

                _logger.LogInformation("Finish ApiTarefa to UpdateTarefa");

                return Ok(tarefaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar tarefa.");
            }
        }

        [Route("DeleteTarefa/{idTarefa}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteTarefa(int idTarefa)
        {
            try
            {
                _logger.LogInformation("Start ApiTarefa to DeleteTarefa");
                if (idTarefa == 0)
                {
                    _logger.LogWarning("Finish ApiTarefa to DeleteTarefa: Tarefa enviada para exclusão inválida.");
                    return BadRequest("Tarefa enviada para exclusão inválida.");
                }

                await _tarefaService.ExcluirTarefa(idTarefa);

                _logger.LogInformation("Finish ApiTarefa to DeleteTarefa");

                return Ok("Tarefa excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar tarefa.");
            }
        }

        [Route("UpdateConcluirTarefa/{idTarefa}")]
        [HttpPut]
        public async Task<ActionResult> UpdateConcluirTarefa(int idTarefa)
        {
            try
            {
                _logger.LogInformation("Start ApiTarefa to UpdateConcluirTarefa");

                if (idTarefa == 0)
                {
                    _logger.LogWarning("Finish ApiTarefa to UpdateConcluirTarefa:Tarefa enviada para conclusão inválida.");
                    return BadRequest("Tarefa enviada para conclusão inválida.");
                }

                await _tarefaService.ConcluirTarefa(idTarefa);

                _logger.LogInformation("Finish ApiTarefa to UpdateConcluirTarefa");

                return Ok("Tarefa concluída com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao concluir tarefa.");
            }
        }

        [Route("PostReativarTarefa/{idTarefa}")]
        [HttpPost]
        public async Task<ActionResult<TarefaDto>> PostRestaurarTarefa(int idTarefa)
        {
            try
            {
                _logger.LogInformation("Start ApiTarefa to PostRestaurarTarefa");

                if (idTarefa == 0)
                {
                    _logger.LogWarning("Finish ApiTarefa to UpdateConcluirTarefa:Tarefa enviada para reativação inválida.");
                    return BadRequest("Tarefa enviada para reativação inválida.");
                }

                _logger.LogInformation("Finish ApiTarefa to PostRestaurarTarefa");

                var tarefaDto = await _tarefaService.ReativarTarefa(idTarefa);

                return StatusCode(StatusCodes.Status201Created, tarefaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao reativar tarefa.");
            }
        }
    }
}
