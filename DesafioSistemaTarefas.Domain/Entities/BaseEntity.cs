using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioSistemaTarefas.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("DataHoraCriacao")]
        public DateTime DataCriacao { get; set; }

        [Column("DataHoraAtualizacao")]
        public DateTime? DataAtualizacao { get; set; }

        public virtual void SetDataCriacao(DateTime dataCriacao)
        {
            DataCriacao = dataCriacao;
        }
        public virtual void SetDataAtualizacao(DateTime dataAtualizacao)
        {
            DataAtualizacao = dataAtualizacao;
        }
        public virtual void SetId(int id)
        {
            Id = id;
        }
    }
}
