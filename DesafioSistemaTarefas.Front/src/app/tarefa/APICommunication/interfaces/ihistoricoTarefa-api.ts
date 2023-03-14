import { Injectable } from '@angular/core';
import { HistoricoTarefaDto } from '../.././models'; 
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export abstract class IHistoricoTarefaApiCommunication {
    abstract getHistoricoTarefasConcluidas(): Observable<HistoricoTarefaDto[]>;
    abstract getHistoricoTarefasExcluidas(): Observable<HistoricoTarefaDto[]>;
    abstract getHistoricoTarefaById(idHistoricoTarefa: Number): Observable<HistoricoTarefaDto>;
    abstract getHistoricoTarefaByIdTarefa(idTarefa: Number): Observable<HistoricoTarefaDto>;
}