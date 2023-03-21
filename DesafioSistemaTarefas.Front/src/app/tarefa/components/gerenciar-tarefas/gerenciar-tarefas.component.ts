import { Component, OnInit, NgZone, ViewChild } from '@angular/core';
import { Injectable } from '@angular/core';
import { IGerenciarTarefasService } from '../../services' ;
import { MatTabChangeEvent } from '@angular/material/tabs';
import { HttpClient, HttpResponse } from '@angular/common/http';  
import { ConfirmationDialogService } from './../../../confirmation-dialog/confirmation-dialog-service';
import {MatSnackBar} from '@angular/material/snack-bar';
import { FormBuilder, Validators, FormControl, FormGroup  } from '@angular/forms';  
import { TarefaDto, HistoricoTarefaDto } from '../../models'; 

export interface PeriodicElement {
  nome: string;
}

@Injectable({
  providedIn: 'root'
})
@Component({
  selector: 'app-gerenciar-tarefas',
  templateUrl: './gerenciar-tarefas.component.html',
  styleUrls: ['./gerenciar-tarefas.component.css']
})

export class GerenciarTarefasComponent implements OnInit {
  displayedColumns: string[] = [];
  displayedColumnsConcluida: string[] = [];
  displayedColumnsExcluida: string[] = [];
  public message: string = "";
  public tarefa: TarefaDto = {} as TarefaDto
  public historicoTarefa: HistoricoTarefaDto = {} as HistoricoTarefaDto; 
  public tarefasAtivas: TarefaDto[] = []; 
  public historicoTarefasConcluidas: HistoricoTarefaDto[] = [];  
  public historicoTarefasExcluidas: HistoricoTarefaDto[] = [];  
  formTarefa: FormGroup;
  formTarefaEdicao: FormGroup;
  indexAtualAba: number = 0;
  formularioEdicaoVisivel: boolean = false;
  formularioSomenteLeitura: boolean = false;

  constructor(private formbuilder: FormBuilder, private iGerenciarTarefasService : IGerenciarTarefasService, 
              private confirmationDialogService: ConfirmationDialogService,private _snackBar: MatSnackBar)
  {
    this.formTarefaEdicao = this.formbuilder.group({
      tituloTarefa: [null, [Validators.required, Validators.minLength(10), Validators.maxLength(20)]],
      descricaoTarefa: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(100)]],
      dataHoraTarefa:[{ value: '', disabled: false }, Validators.required],
      idTarefa:[{ value: 0, disabled: false }, Validators.required]
    });

       this.formTarefa = this.formbuilder.group({
      tituloTarefa: [null, [Validators.required, Validators.minLength(10), Validators.maxLength(20)]],
      descricaoTarefa: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(100)]],
      dataHoraTarefa:[{ value: '', disabled: false }, Validators.required],
    });
  }
    
  ngOnInit(): void {
    this.carregarNomesColunas(0);    
    }

  public carregarTarefasAtivas() {  
    try{
      this.iGerenciarTarefasService.getTarefasAtivas().subscribe((resultado) => {     
        this.tarefasAtivas = resultado as TarefaDto[];
        
    }); 
  }
  catch(err)
  {
    this.message = 'Erro durante o processo de carga de tarefas.'
  }
  finally
  {
    if(this.message.length > 0)
    {
this.exibirMensagem(this.message, "Fechar");
this.message = '';
    }
  }
  }

  public carregarTarefasConcluidas() {  
    try{
      this.iGerenciarTarefasService.getTarefasConcluidas().subscribe(resultado => {

        this.historicoTarefasConcluidas = resultado;
    }); 
  }
  catch(err)
  {
    this.message = 'Erro durante o processo de carga de tarefas.'
  }
  finally
  {
    if(this.message.length > 0)
    {
this.exibirMensagem(this.message, "Fechar");
this.message = '';
    }
  }
  }

  public carregarTarefasExcluidas() {  
    try{
      this.iGerenciarTarefasService.getTarefasExcluidas().subscribe(resultado => {
      
        this.historicoTarefasExcluidas = resultado;
    }); 
  }
  catch(err)
  {
    this.message = 'Erro durante o processo de carga de tarefas.'
  }
  finally
  {
    if(this.message.length > 0)
    {
this.exibirMensagem(this.message, "Fechar");
this.message = '';
    }
  }
  }

  public concluirTarefa(id: Number) {  
    try{
      this.iGerenciarTarefasService.concluirTarefa(id).subscribe(resultado => {
      
        this.message = "Sucesso ao concluir";
        this.carregarNomesColunas(0);
    }); 
  }
  catch(err)
  {
    this.message = 'Erro durante o processo de carga de investimentos.'
  }
  finally
  {
    if(this.message.length > 0)
    {
this.exibirMensagem(this.message, "Fechar");
this.message = '';
    }
  }
  }
  
  public excluirTarefa(idTarefa: Number) {  
    try{
      this.iGerenciarTarefasService.excluirTarefa(idTarefa).subscribe(resultado => {
        this.message = "Sucesso ao excluir";
        this.carregarNomesColunas(0);
    }); 
  }
  catch(err)
  {
    this.message = 'Erro durante o processo de carga de investimentos.'
  }
  finally
  {
    if(this.message.length > 0)
    {
this.exibirMensagem(this.message, "Fechar");
this.message = '';
    }
  }
  }
  
  public reativarTarefa(idTarefa: Number) {  
    try{
      this.iGerenciarTarefasService.reativarTarefa(idTarefa).subscribe((resultado) => {
      
        this.message = "Sucesso ao reativar";
        this.carregarNomesColunas(this.indexAtualAba);
    }); 
  }
  catch(err)
  {
    this.message = 'Erro durante o processo de carga de investimentos.'
  }
  finally
  {
    if(this.message.length > 0)
    {
this.exibirMensagem(this.message, "Fechar");
this.message = '';
    }
  }
  }
  
  public visualizarHistoricoTarefa(idTarefa: Number) {  
    try{
      this.iGerenciarTarefasService.buscarTarefaNoHistorico(idTarefa).subscribe(resultado => {
        
        this.tarefa = resultado;
        this.formTarefaEdicao = this.formbuilder.group({
          tituloTarefa: [{value:this.tarefa.nome, disabled: true}, [Validators.required, Validators.minLength(10), Validators.maxLength(20)]],
          descricaoTarefa: [{value: this.tarefa.descricao, disabled: true}, [Validators.required, Validators.minLength(5), Validators.maxLength(100),]],
          dataHoraTarefa:[{ value: this.tarefa.dataHoraTarefa, disabled: true }, Validators.required],
          idTarefa:[{ value: this.tarefa.id, disabled: true }],
        });

        
        this.formularioEdicaoVisivel = true;
    }); 
  }
  catch(err)
  {
    this.message = 'Erro durante o processo de carga de investimentos.'
  }
  finally
  {
    if(this.message.length > 0)
    {
this.exibirMensagem(this.message, "Fechar");
this.message = '';
    }
  }
  }

  public visualizarTarefa(idTarefa: Number) {  
    try{
      this.iGerenciarTarefasService.buscarPorIdTarefa(idTarefa).subscribe(resultado => {
        this.tarefa = resultado;
        this.formTarefaEdicao = this.formbuilder.group({
          tituloTarefa: [{value:this.tarefa.nome, disabled: true}, [Validators.required, Validators.minLength(10), Validators.maxLength(20)]],
          descricaoTarefa: [{value: this.tarefa.descricao, disabled: true}, [Validators.required, Validators.minLength(5), Validators.maxLength(100)]],
          dataHoraTarefa:[{ value: this.tarefa.dataHoraTarefa, disabled: true }, Validators.required],
          idTarefa:[{ value: this.tarefa.id, disabled: true }]
        });

        this.formularioEdicaoVisivel = true;
      });
  }
  catch(err)
  {
    this.message = 'Erro durante o processo de carga de investimentos.'
  }
  finally
  {
    if(this.message.length > 0)
    {
this.exibirMensagem(this.message, "Fechar");
this.message = '';
    }
  }
  }

  public editarTarefa(idTarefa: Number) {  
    try{
      
      this.iGerenciarTarefasService.buscarPorIdTarefa(idTarefa).subscribe(resultado => {
        this.tarefa = resultado;
        this.formTarefaEdicao = this.formbuilder.group({
          tituloTarefa: [this.tarefa.nome, [Validators.required, Validators.minLength(10), Validators.maxLength(20)]],
          descricaoTarefa: [this.tarefa.descricao, [Validators.required, Validators.minLength(5), Validators.maxLength(100)]],
          dataHoraTarefa:[{ value: this.tarefa.dataHoraTarefa, disabled: false }, Validators.required],
          idTarefa:[{ value: this.tarefa.id, disabled: false }],
        });

        this.formularioEdicaoVisivel = true;
      });
  }
  catch(err)
  {
    this.message = 'Erro durante o processo de carga de investimentos.'
  }
  finally
  {
    if(this.message.length > 0)
    {
this.exibirMensagem(this.message, "Fechar");
this.message = '';
    }
  }
  }

  public inserirTarefa = (formTarefa : any) => {  
    try{
      this.tarefa = new TarefaDto();
      this.tarefa.nome = formTarefa.tituloTarefa;
      this.tarefa.descricao = formTarefa.descricaoTarefa;
      this.tarefa.dataHoraTarefa = formTarefa.dataHoraTarefa;

      this.iGerenciarTarefasService.inserirTarefa(this.tarefa).subscribe(resultado => {
                
      this.message = "Sucesso ao inserir";
        this.carregarNomesColunas(0);
    }); 
  }
  catch(err)
  {
    this.message = 'Erro durante o processo de carga de investimentos.'
  }
  finally
  {
    if(this.message.length > 0)
    {
this.exibirMensagem(this.message, "Fechar");
this.message = '';
    }
  }
  }
  public ocultarFormularioEdicao(){
    this.formularioEdicaoVisivel = false;
  }
  public atualizarTarefa = (formTarefaEdicao : any) => {  
    
    this.tarefa = new TarefaDto();
    this.tarefa.id = formTarefaEdicao.idTarefa;
    this.tarefa.nome = formTarefaEdicao.tituloTarefa;
    this.tarefa.descricao = formTarefaEdicao.descricaoTarefa;
    this.tarefa.dataHoraTarefa = formTarefaEdicao.dataHoraTarefa;
    try{
      this.iGerenciarTarefasService.atualizarTarefa(this.tarefa).subscribe(resultado => {
      
      this.formularioEdicaoVisivel = false;

      this.message = "Sucesso ao atualizar";

      this.carregarNomesColunas(this.indexAtualAba);
    }); 
  }
  catch(err)
  {
    this.message = 'Erro durante o processo de carga de investimentos.'
  }
  finally
  {
    if(this.message.length > 0)
    {
this.exibirMensagem(this.message, "Fechar");
this.message = '';
    }
  }

  }
  carregarDadosAba(changeEvent: MatTabChangeEvent) {
    this.indexAtualAba = changeEvent.index;
    this.carregarNomesColunas(changeEvent.index);
    if(changeEvent.index == 0)
    {      
      this.carregarTarefasAtivas();
    }
    else if(changeEvent.index == 1)
    {      
      this.carregarTarefasConcluidas();
    }
    else if(changeEvent.index == 2)
    {      
      this.carregarTarefasExcluidas();
    }
 }
 
private carregarNomesColunas(indexColuna: Number){
  
  if(indexColuna == 0)
  {    
    this.displayedColumns = ['nome', 'dataHoraTarefa', 'status', 'option']
    this.carregarTarefasAtivas();
  }
  else if(indexColuna == 1)
  {    
    this.displayedColumnsConcluida = ['nome', 'dataHoraTarefa', 'status','dataHoraConclusao', 'option']      
    this.carregarTarefasConcluidas();
  }
  else if(indexColuna == 2)
  {
    this.displayedColumnsExcluida = ['nome', 'dataHoraTarefa', 'status', 'dataHoraExclusao',  'option']      
    this.carregarTarefasExcluidas();    
  }
 } 
 
 exibirMensagem(message: string, action: string) {
  this._snackBar.open(message, action);
}
}
