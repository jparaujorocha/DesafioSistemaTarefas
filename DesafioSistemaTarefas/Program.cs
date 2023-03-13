using DesafioSistemaTarefas.API.Controllers;
using DesafioSistemaTarefas.Domain.Validations;
using DesafioSistemaTarefas.Ioc;
using FluentValidation;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ConsumerController>();

    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("tarefasQueue", ep =>
        {
            ep.PrefetchCount = 10;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<ConsumerController>(provider);
        });
    }));
});

builder.Services.Configure<MassTransitHostOptions>(options =>
{
    options.WaitUntilStarted = true;
    options.StartTimeout = TimeSpan.FromSeconds(30);
    options.StopTimeout = TimeSpan.FromMinutes(1);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining(typeof(TarefaValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(HistoricoTarefaValidator));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
