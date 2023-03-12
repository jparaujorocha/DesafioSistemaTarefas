using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DesafioSistemaTarefas.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    [Table("StatusHistorico")]
    public class StatusTarefa : BaseEntity
    {
        [Column("Nome")]
        public string Nome { get; }
    }
}
