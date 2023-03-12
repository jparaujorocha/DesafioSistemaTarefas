using DesafioSistemaTarefas.Domain.Validations;
using DesafioSistemaTarefas.Ioc;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args); 

// Add services to the container.

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
