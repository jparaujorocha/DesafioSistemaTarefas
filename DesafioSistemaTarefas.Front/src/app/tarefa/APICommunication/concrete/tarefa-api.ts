import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';  
import { HttpHeaders } from '@angular/common/http';  
import { Observable } from 'rxjs';
import { TarefaDto } from '../../models';  
import { map } from 'rxjs/operators';
import { ITarefaApiCommunication } from './../interfaces/itarefa-api';

var headers = new HttpHeaders().set('Access-Control-Request-Headers', 'X-Requested-With, Content-Type,'+  
'Origin, Authorization, Accept, Client-Security-Token, Accept-Encoding, X-Auth-Token, content-type').set('Access-Control-Request-Origin', '*')
.set('Access-Control-Allow-Methods', 'POST, GET, OPTIONS, DELETE, PUT');
                                

@Injectable({
  providedIn: 'root'
})

export class TarefaApiCommunication implements ITarefaApiCommunication {
    url = 'https://localhost:7228/Tarefa/';  
    constructor(private http: HttpClient) {
      headers = new HttpHeaders("HTTP/1.1 200 OK");
     }

  getTarefasAtivas(): Observable<TarefaDto[]> {
   
    return this.http.get<TarefaDto[]>(this.url + "GetTarefas/");
  }
  
  getTarefaById(idTarefa: Number): Observable<TarefaDto> {
    return this.http.get<TarefaDto>(this.url + "GetTarefa/" + idTarefa);
  }
  concluirTarefa(idTarefa: Number):Observable<any> {
    return this.http.put(this.url + "UpdateConcluirTarefa/" + idTarefa, {  responseType: 'text' as 'json', headers: headers});  
  }
  excluirTarefa(idTarefa: Number): Observable<any> {
    return this.http.delete(this.url + "DeleteTarefa/" + idTarefa, { responseType: 'text', headers: headers});  
  }
  reativarTarefa(idTarefa: Number): Observable<TarefaDto> {
    return this.http.post<TarefaDto>(this.url + "PostReativarTarefa/" + idTarefa,{ responseType: 'text', headers: headers});
  }
  inserirTarefa(tarefaDto: TarefaDto): Observable<TarefaDto> {
    return this.http.post<any>(this.url + "InsertTarefa/", tarefaDto, {headers: headers});  
  }
  atualizarTarefa(tarefaDto: TarefaDto): Observable<TarefaDto> {;
    return this.http.put<TarefaDto>(this.url + "UpdateTarefa/", tarefaDto);  
  }
}
