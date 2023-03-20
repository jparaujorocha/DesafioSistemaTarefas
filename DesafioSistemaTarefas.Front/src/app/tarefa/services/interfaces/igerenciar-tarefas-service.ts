import { Injectable } from '@angular/core';
import { TarefaDto, HistoricoTarefaDto } from '../../models'; 
import { Observable } from 'rxjs';
import { HttpClient, HttpResponse } from '@angular/common/http';  
@Injectable({
    providedIn: 'root'
  })
  
export abstract class  IGerenciarTarefasService {
    abstract getTarefasAtivas(): Observable<TarefaDto[]>;
    abstract getTarefasConcluidas(): Observable<HistoricoTarefaDto[]>;
    abstract getTarefasExcluidas(): Observable<HistoricoTarefaDto[]>;
    abstract concluirTarefa(idTarefa : Number): Observable<any>;
    abstract excluirTarefa(idTarefa : Number): Observable<any>;
    abstract reativarTarefa(idTarefa : Number): Observable<TarefaDto>;
    abstract inserirTarefa(tarefaDto : TarefaDto): Observable<TarefaDto>;
    abstract atualizarTarefa(tarefaDto : TarefaDto): Observable<TarefaDto>;
    abstract buscarPorIdTarefa(idTarefa : Number): Observable<TarefaDto>;
    abstract buscarTarefaNoHistorico(idTarefa : Number): Observable<HistoricoTarefaDto>;
}