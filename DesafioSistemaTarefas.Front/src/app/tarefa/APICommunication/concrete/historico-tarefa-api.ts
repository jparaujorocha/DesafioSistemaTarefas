import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';  
import { HttpHeaders } from '@angular/common/http';  
import { Observable } from 'rxjs';
import { HistoricoTarefaDto } from '../../models';  
import { map } from 'rxjs/operators';
import { IHistoricoTarefaApiCommunication } from './../interfaces/ihistoricoTarefa-api';


@Injectable({
  providedIn: 'root'
})

export class HistoricoTarefaApiCommunication implements IHistoricoTarefaApiCommunication {

    url = 'https://localhost:7228/HistoricoTarefa/';  
    constructor(private http: HttpClient) { }

    getHistoricoTarefasConcluidas(): Observable<HistoricoTarefaDto[]> {
        return this.http.get<HistoricoTarefaDto[]>(this.url + "GetByStatus/" + 2);
    }
    getHistoricoTarefasExcluidas(): Observable<HistoricoTarefaDto[]> {
        return this.http.get<HistoricoTarefaDto[]>(this.url + "GetByStatus/" + 3);
    }
    getHistoricoTarefaById(idHistoricoTarefa: Number): Observable<HistoricoTarefaDto> {
        return this.http.get<HistoricoTarefaDto>(this.url + "GetHistoricoTarefa/" + idHistoricoTarefa);
    }
    getHistoricoTarefaByIdTarefa(idTarefa: Number): Observable<HistoricoTarefaDto> {
        
        return this.http.get<HistoricoTarefaDto>(this.url + "GetByIdTarefa/" + idTarefa);
    }
}
