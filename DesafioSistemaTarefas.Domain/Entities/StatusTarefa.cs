using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioSistemaTarefas.Domain.Entities
{
    [Table("StatusHistorico")]
    public class StatusTarefa : BaseEntity
    {
        [Column("Nome")]
        public string Nome { get; }
    }
}
