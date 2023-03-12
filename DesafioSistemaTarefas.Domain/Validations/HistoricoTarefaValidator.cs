using DesafioSistemaTarefas.Domain.Entities;
using DesafioSistemaTarefas.Domain.Enums;
using DesafioSistemaTarefas.Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace DesafioSistemaTarefas.Domain.Validations
{
    public class HistoricoTarefaValidator : AbstractValidator<HistoricoTarefa>
    {
        public HistoricoTarefaValidator()
        {
            RuleFor(d => d.IdTarefa).GreaterThan(0).WithState(a => new DomainException("Id do histórico da tarefa inválido."));
            RuleFor(historicoTarefa => historicoTarefa.Nome).NotEmpty().MinimumLength(3).MaximumLength(20).WithState(a => new DomainException("Nome obrigatório, devendo ter entre 3 e 20 caracteres."));
            RuleFor(historicoTarefa => historicoTarefa.Descricao).NotEmpty().MinimumLength(10).MaximumLength(100).WithState(a => new DomainException("Decrição obrigatória, devendo ter entre 10 e 100 caracteres."));
            RuleFor(historicoTarefa => historicoTarefa.DataHoraTarefa).NotEmpty().WithState(a => new DomainException("Data da tarefa deve ser válida."));
            RuleFor(historicoTarefa => (EnumStatusTarefa)historicoTarefa.IdStatusTarefa).IsInEnum().WithState(a => new DomainException("Status da Tarefa Inválido."));
        }
        protected override void RaiseValidationException(ValidationContext<HistoricoTarefa> context, ValidationResult result)
        {
            var ex = new ValidationException(result.Errors);
            throw new DomainException(ex.Message, ex);
        }
    }
}
