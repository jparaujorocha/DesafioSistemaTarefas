using DesafioSistemaTarefas.Domain.Validations;
using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioSistemaTarefas.Domain.Entities
{
    [Table("HistoricoTarefa")]
    public class HistoricoTarefa : Tarefa
    {
        [Column("DataHoraExclusao")]
        public DateTime? DataHoraExclusao { get; private set; }
        [Column("DataHoraConclusao")]
        public DateTime? DataHoraConclusao { get; private set; }
        [Column("IdTarefa")]
        public int IdTarefa { get; private set; }

        public HistoricoTarefa(int? id, int idTarefa, DateTime? dataHoraExclusao, DateTime? dataHoraConclusao,
                               string nome, string descricao, DateTime dataHoraTarefa, int idStatusTarefa) : base(idTarefa, nome, descricao, dataHoraTarefa, idStatusTarefa)
        {
            if (id.HasValue && id.Value > 0)
            {
                SetId(id.Value);
            }
            SetIdTarefa(idTarefa);
            SetDataHoraExclusao(dataHoraExclusao);
            SetDataHoraConclusao(dataHoraConclusao);
        }

        private void SetIdTarefa(int idTarefa)
        {
            IdTarefa = idTarefa;
        }

        public void SetDataHoraConclusao(DateTime? dataHoraConclusao)
        {
            DataHoraConclusao = dataHoraConclusao;
        }

        public void SetDataHoraExclusao(DateTime? dataHoraExclusao)
        {
            DataHoraExclusao = dataHoraExclusao;
        }

        public override bool ValidateWithoutId()
        {
            new HistoricoTarefaValidator().ValidateAndThrow(this);

            return true;
        }
    }
}
