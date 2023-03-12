using DesafioSistemaTarefas.Domain.Entities;
using DesafioSistemaTarefas.Domain.Enums;
using DesafioSistemaTarefas.Domain.Exceptions;
using FluentValidation;

namespace DesafioSistemaTarefas.Domain.Validations
{
    public class TarefaValidator : AbstractValidator<Tarefa>
    {
        public TarefaValidator()
        {
            RuleFor(tarefa => tarefa.Nome).NotEmpty().MinimumLength(3).MaximumLength(20).WithState(a => new DomainException("Nome obrigatório, devendo ter entre 3 e 20 caracteres.."));
            RuleFor(tarefa => tarefa.Descricao).NotEmpty().MinimumLength(10).MaximumLength(100).WithState(a => new DomainException("Decrição obrigatória, devendo ter entre 10 e 100 caracteres."));
            RuleFor(tarefa => tarefa.DataHoraTarefa).NotEmpty().GreaterThan(DateTime.Now).WithState(a => new DomainException("Data da tarefa deve ser válida e maior que a hora atual."));
            RuleFor(tarefa => (EnumStatusTarefa)tarefa.IdStatusTarefa).IsInEnum().WithState(a => new DomainException("Status da Tarefa Inválido."));
        }
        public TarefaValidator(int idTarefa)
        {
            RuleFor(tarefa => tarefa.Id).GreaterThan(0).WithState(a => new DomainException("Id da tarefa inválido."));
            _ = new TarefaValidator();
        }

    }
}
