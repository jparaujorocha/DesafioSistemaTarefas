using DesafioSistemaTarefas.Domain.Enums;
using DesafioSistemaTarefas.Domain.Validations;
using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioSistemaTarefas.Domain.Entities
{
    [Table("Tarefa")]
    public class Tarefa : BaseEntity
    {
        [Column("Nome")]
        public string Nome { get; private set; }
        [Column("Descricao")]
        public string Descricao { get; private set; }
        [Column("DataHoraTarefa")]
        public DateTime DataHoraTarefa { get; private set; }
        [Column("IdStatusTarefa")]
        public int IdStatusTarefa { get; private set; }

        public Tarefa(int? id, string nome, string descricao, DateTime dataHoraTarefa, int idStatusTarefa)
        {
            SetStatusTarefa(idStatusTarefa);
            SetNome(nome);
            SetDescricao(descricao);
            SetDataHoraTarefa(dataHoraTarefa);

            if (id.HasValue && id.Value > 0)
            {
                SetId(id.Value);
            }
        }

        public void SetNome(string nome)
        {
            Nome = nome;
        }
        public void SetDescricao(string descricao)
        {
            Descricao = descricao;

        }
        public void SetDataHoraTarefa(DateTime dataHoraTarefa)
        {
            DataHoraTarefa = dataHoraTarefa;
        }

        public virtual bool ValidateWithoutId()
        {
            new TarefaValidator().ValidateAndThrow(this);

            return true;
        }
        public virtual bool ValidateWithId()
        {
            new TarefaValidator(Id).ValidateAndThrow(this);

            return true;
        }
        public void SetStatusTarefa(int idStatusTarefa)
        {
            IdStatusTarefa = Convert.ToInt32((EnumStatusTarefa)idStatusTarefa);
        }

        public string GetNomeStatusTarefa(int idStatusTarefa)
        {
            if (Enum.IsDefined(typeof(EnumStatusTarefa), idStatusTarefa))
            {
                return Enum.GetName(typeof(EnumStatusTarefa), idStatusTarefa);
            }
            return "Status Inválido";
        }
    }
}
