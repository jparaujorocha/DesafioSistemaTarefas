import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TarefaDto, HistoricoTarefaDto } from '../../models';  
import { IGerenciarTarefasService } from './../interfaces/igerenciar-tarefas-service';
import { ITarefaApiCommunication,IHistoricoTarefaApiCommunication } from '././../../APICommunication';
import { HttpClient, HttpResponse } from '@angular/common/http';  

@Injectable({
  providedIn: 'root'
})

export class GerenciarTarefasService implements IGerenciarTarefasService {
  
  constructor(private tarefaApiCommunication : ITarefaApiCommunication,
              private historicoTarefaApiCommunication : IHistoricoTarefaApiCommunication){}
  getTarefasAtivas(): Observable<TarefaDto[]> {
    return this.tarefaApiCommunication.getTarefasAtivas();
  }
  getTarefasConcluidas(): Observable<HistoricoTarefaDto[]> {
    return this.historicoTarefaApiCommunication.getHistoricoTarefasConcluidas();
  }
  getTarefasExcluidas(): Observable<HistoricoTarefaDto[]> {
    return this.historicoTarefaApiCommunication.getHistoricoTarefasExcluidas();
  }
  concluirTarefa(idTarefa: Number): Observable<any> {
    return this.tarefaApiCommunication.concluirTarefa(idTarefa);
  }
  excluirTarefa(idTarefa: Number): Observable<any> {
    return this.tarefaApiCommunication.excluirTarefa(idTarefa);
  }
  reativarTarefa(idTarefa: Number): Observable<TarefaDto> {
    return this.tarefaApiCommunication.reativarTarefa(idTarefa);
  }
  inserirTarefa(tarefaDto: TarefaDto): Observable<TarefaDto> {
    return this.tarefaApiCommunication.inserirTarefa(tarefaDto);
  }
  atualizarTarefa(tarefaDto: TarefaDto): Observable<TarefaDto> {
    return this.tarefaApiCommunication.atualizarTarefa(tarefaDto);
  }
  buscarPorIdTarefa(idTarefa: Number): Observable<TarefaDto> {
    return this.tarefaApiCommunication.getTarefaById(idTarefa);      
  }
  buscarTarefaNoHistorico(idTarefa: Number): Observable<HistoricoTarefaDto> {
    return this.historicoTarefaApiCommunication.getHistoricoTarefaByIdTarefa(idTarefa);         
  }
}
