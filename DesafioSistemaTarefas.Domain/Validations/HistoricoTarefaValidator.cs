using DesafioSistemaTarefas.Domain.Entities;
using DesafioSistemaTarefas.Domain.Exceptions;
using FluentValidation;

namespace DesafioSistemaTarefas.Domain.Validations
{
    public class HistoricoTarefaValidator : AbstractValidator<HistoricoTarefa>
    {
        public HistoricoTarefaValidator()
        {
            RuleFor(d => d.IdTarefa).GreaterThan(0).WithState(a => new DomainException("Id do histórico da tarefa inválido."));
            RuleFor(historicoTarefa => historicoTarefa.DataHoraExclusao).NotEmpty().GreaterThanOrEqualTo(DateTime.Now).WithState(a => new DomainException("Data da Exclusão do histórico da tarefa deve ser válida."));
            Include(a => new TarefaValidator(a.IdTarefa));
        }
    }
}
