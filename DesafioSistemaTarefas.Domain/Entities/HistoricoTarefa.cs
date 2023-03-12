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

        public HistoricoTarefa(int idTarefa, DateTime? dataHoraExclusao, DateTime? dataHoraConclusao,
                               string nome, string descricao, DateTime dataHoraTarefa, int idStatusTarefa) : base(idTarefa, nome, descricao, dataHoraTarefa, idStatusTarefa)
        {
            SetIdTarefa(idTarefa);
            SetDataHoraExclusao(dataHoraExclusao);
            SetDataHoraConclusao(dataHoraConclusao);
        }
        public HistoricoTarefa(int id, int idTarefa, DateTime? dataHoraExclusao, DateTime? dataHoraConclusao,
                               string nome, string descricao, DateTime dataHoraTarefa, int idStatusTarefa) : base(idTarefa, nome, descricao, dataHoraTarefa, idStatusTarefa)
        {
            SetId(id);
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
    }
}
