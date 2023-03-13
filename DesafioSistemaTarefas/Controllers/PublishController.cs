using DesafioSistemaTarefas.Application.DTOs;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DesafioSistemaTarefas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly ILogger _logger;


        public PublishController(IBus bus, ILogger<PublishController> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        [Route("SendTarefasQueue")]
        [HttpPost]
        public async Task<IActionResult> EnviarParaInsertTarefa(TarefaDto dadosTarefa)
        {
            if (dadosTarefa != null)
            {
                Uri uri = new Uri("rabbitmq://localhost/tarefasQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(dadosTarefa);
                return Ok();
            }
            return BadRequest();
        }

        [Route("SendTarefaDeadLetterQueue")]
        [HttpPost]
        public async Task<IActionResult> EnviarParaDeadLetter(TarefaDto dadosTarefa)
        {
            if (dadosTarefa != null)
            {
                Uri uri = new Uri("rabbitmq://localhost/tarefasDeadLetterQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(dadosTarefa);
                return Ok();
            }
            return BadRequest();
        }
    }
}
