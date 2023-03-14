import { Injectable } from '@angular/core';
import { TarefaDto } from '../.././models'; 
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export abstract class ITarefaApiCommunication {
    abstract getTarefasAtivas(): Observable<TarefaDto[]>;
    abstract concluirTarefa(idTarefa : Number): Observable<any>;
    abstract excluirTarefa(idTarefa : Number): Observable<any>;
    abstract reativarTarefa(idTarefa : Number): Observable<TarefaDto>;
    abstract inserirTarefa(tarefaDto : TarefaDto): Observable<TarefaDto>;
    abstract atualizarTarefa(tarefaDto : TarefaDto): Observable<TarefaDto>;
    abstract getTarefaById(idTarefa: Number): Observable<TarefaDto>;
}