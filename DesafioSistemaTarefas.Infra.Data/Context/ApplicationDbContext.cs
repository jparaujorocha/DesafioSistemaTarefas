using DesafioSistemaTarefas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioSistemaTarefas.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<HistoricoTarefa> HistoricoTarefas { get; set; }
        public DbSet<StatusTarefa> EnumStatusHistoricos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tarefa>().UseTpcMappingStrategy()
            .ToTable("Tarefa");
            modelBuilder.Entity<HistoricoTarefa>()
            .ToTable("HistoricoTarefa").HasBaseType((Type)null);
        }
    }
}
