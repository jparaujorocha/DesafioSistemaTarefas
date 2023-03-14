export class TarefaDto {

    public TarefaDto()
    {
        
    }
    id: number = 0;
    nome: string = '';
    descricao: string = '';
    idStatusTarefa: number = 0;
    status: string = '';
    dataHoraTarefa: Date = new Date();
    dataAtualizacao: Date = new Date();
    dataCriacao: Date = new Date();
}
