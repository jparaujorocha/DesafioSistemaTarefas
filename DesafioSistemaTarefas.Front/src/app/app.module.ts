import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { IGerenciarTarefasService, GerenciarTarefasService } from './../app/tarefa/services/';
import { ITarefaApiCommunication, IHistoricoTarefaApiCommunication, HistoricoTarefaApiCommunication, TarefaApiCommunication } from './tarefa/APICommunication';
import { AppComponent } from './app.component';
import { TarefaModule } from './tarefa';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxMatDatetimePickerModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import {FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
 import {ConfirmationDialogService} from  './confirmation-dialog/confirmation-dialog-service';
 import {MatSnackBarModule} from '@angular/material/snack-bar';


@NgModule({
  declarations: [
    AppComponent,
    ConfirmationDialogComponent    
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    TarefaModule,
    HttpClientModule,
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatInputModule,
    NgxMatDatetimePickerModule,
    NgxMatTimepickerModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatRadioModule,
    MatSelectModule,
    FontAwesomeModule,
    MatSnackBarModule    
  ],
  providers: [
    {provide: IGerenciarTarefasService, useClass: GerenciarTarefasService},
    {provide: ITarefaApiCommunication, useClass: TarefaApiCommunication},
    {provide: IHistoricoTarefaApiCommunication, useClass: HistoricoTarefaApiCommunication},
    {provide: ConfirmationDialogService}],
  bootstrap: [AppComponent]
})
export class AppModule { }
