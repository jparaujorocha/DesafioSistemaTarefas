using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Application.Interfaces;
using DesafioSistemaTarefas.Application.Services;
using MassTransit;
using System.Net.Http.Headers;

namespace DesafioSistemaTarefas.API.Controllers
{
    public class ConsumerController : IConsumer<TarefaDto>
    {
        private readonly ILogger<TarefaDto> logger;
        private readonly ITarefaService _tarefaService;
        public ConsumerController(ILogger<TarefaDto> logger, ITarefaService tarefaService)
        {
            this.logger = logger;
            _tarefaService = tarefaService;
        }

        public async Task Consume(ConsumeContext<TarefaDto> context)
        {
            try
            {
                logger.LogInformation($"Nova mensagem recebida. IdTarefa:" +
                    $" {context.Message.Id}");

                await _tarefaService.InserirTarefa(context.Message);

                logger.LogInformation($"Mensagem enviada para Aplicação. IdTarefa:" +
                    $" {context.Message.Id}");
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao consumir mensagem da fila de tarefas. IdTarefa:" +
                    $" {context.Message.Id}.", ex);

                SendToDeadLetter(context);
            }
        }

        private async Task SendToDeadLetter(ConsumeContext<TarefaDto> context)
        {
            try
            {
                logger.LogInformation($"Enviando mensagem para dead-letter.");

                HttpClient client = new HttpClient();


                client.BaseAddress = new Uri("http://localhost:7228/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "api/publish/SendTarefaDeadLetterQueue", context.Message);
                response.EnsureSuccessStatusCode();

                // return URI of the created resource.
                if (response.IsSuccessStatusCode)
                    logger.LogInformation($"Mensagem enviada para dead-letter.");
                else
                    logger.LogError("Erro ao enviar mensagem para dead-letter.", response, context);

            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao enviar mensagem para dead-letter.", ex, context.Message);
            }
        }

    }
}
