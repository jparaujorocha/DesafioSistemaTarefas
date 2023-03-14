using System.Diagnostics.CodeAnalysis;

namespace DesafioSistemaTarefas.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseDto
    {
        public int? id { get; set; }
        public DateTime? dataCriacao { get; set; }
        public DateTime? dataAtualizacao{ get; set; }
    }
}
