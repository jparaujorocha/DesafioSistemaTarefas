namespace DesafioSistemaTarefas.Application.DTOs
{
    public abstract class BaseDto
    {
        public int? Id { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAtualizacao{ get; set; }
    }
}
