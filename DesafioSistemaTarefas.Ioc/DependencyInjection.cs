using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DesafioSistemaTarefas.Infra.Data.Context;
using DesafioSistemaTarefas.Domain.Inferfaces;
using DesafioSistemaTarefas.Infra.Data.Repositories;
using DesafioSistemaTarefas.Application.Mappings;
using DesafioSistemaTarefas.Application.Interfaces;
using DesafioSistemaTarefas.Application.Services;
using System.Diagnostics.CodeAnalysis;

namespace DesafioSistemaTarefas.Ioc
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });


            services.AddScoped<ITarefaRepository, TarefaRepository>();
            services.AddScoped<IHistoricoTarefaRepository, HistoricoTarefaRepository>();
            services.AddScoped<ITarefaService, TarefaService>();
            services.AddScoped<IHistoricoTarefaService, HistoricoTarefaService>();

            services.AddAutoMapper(typeof(TarefaProfile));
            services.AddAutoMapper(typeof(HistoricoTarefaProfile));

            return services;
        }
    }
}
