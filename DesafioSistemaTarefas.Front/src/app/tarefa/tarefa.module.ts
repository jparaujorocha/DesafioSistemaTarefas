import { NgModule, LOCALE_ID} from '@angular/core';
import { CommonModule } from '@angular/common';
import { GerenciarTarefasComponent } from './components';
import { IGerenciarTarefasService, GerenciarTarefasService } from './services';
import { ITarefaApiCommunication, IHistoricoTarefaApiCommunication, HistoricoTarefaApiCommunication, TarefaApiCommunication } from './APICommunication';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule  } from '@angular/material/card';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatToolbarModule   } from '@angular/material/toolbar';
import {MatSelectModule} from '@angular/material/select';
import { MatTableModule } from '@angular/material/table'  
import { NgxMatDatetimePickerModule, NgxMatTimepickerModule,
  NgxMatNativeDateModule,  } from '@angular-material-components/datetime-picker';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDatetimepickerModule, MatNativeDatetimeModule } from "@mat-datetimepicker/core";
import {MatTabsModule} from '@angular/material/tabs';


   
import { Pipe } from "@angular/core";
import {
  ReactiveFormsModule,
  FormsModule
} from "@angular/forms";



@NgModule({
  declarations: [
    GerenciarTarefasComponent
  ],
  imports: [
    CommonModule ,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatMenuModule,
    MatDatepickerModule,
    MatIconModule,
    MatCardModule,
    MatSidenavModule,
    MatTooltipModule,
    MatToolbarModule,
    ReactiveFormsModule,
    FormsModule,
    MatSelectModule,
    MatTableModule,
    NgxMatTimepickerModule,
    NgxMatDatetimePickerModule,
    NgxMatNativeDateModule,    
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDatetimeModule,
    MatDatetimepickerModule,
    MatTabsModule
  ],
  exports: [GerenciarTarefasComponent, MatTableModule],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    {provide: IGerenciarTarefasService, useClass: GerenciarTarefasService},
    {provide: ITarefaApiCommunication, useClass: TarefaApiCommunication},
    {provide: IHistoricoTarefaApiCommunication, useClass: HistoricoTarefaApiCommunication}
  ]
})
export class TarefaModule { }
