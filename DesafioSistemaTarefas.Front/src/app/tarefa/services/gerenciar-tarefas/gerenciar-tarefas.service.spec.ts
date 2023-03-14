import { TestBed } from '@angular/core/testing';

import { GerenciarTarefasService } from './gerenciar-tarefas.service';

describe('GerenciarTarefasService', () => {
  let service: GerenciarTarefasService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GerenciarTarefasService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
