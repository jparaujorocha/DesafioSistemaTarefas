import { TarefaDto } from "./tarefaDto";

export class HistoricoTarefaDto extends TarefaDto {

    public HistoricoTarefaDto()
    {
        
    }
    IdTarefa: number = 0;
    DataHoraExclusao: Date = {} as Date;
    DataHoraConclusao: Date = {} as Date;
}