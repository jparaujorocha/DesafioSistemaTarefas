USE [master]
GO
CREATE DATABASE [DesafioSistemaTarefas]
GO

USE [DesafioSistemaTarefas]
GO

Create Table StatusTarefa (
Id INT Primary Key Identity(1,1),
Nome Varchar(10) Not Null,
DataHoraCriacao Datetime Not Null,
DataHoraAtualizacao Datetime

);
GO

Create Table Tarefa (
Id INT Primary Key Identity(1,1),
IdStatusTarefa INT NOT NULL,
Nome Varchar(20) Not Null,
Descricao Varchar(100) Not Null,
DataHoraTarefa Datetime Not Null,
DataHoraCriacao Datetime Not Null,
DataHoraAtualizacao Datetime
);

GO
Create Table HistoricoTarefa(
Id INT Primary Key Identity(1,1),
IdTarefa INT NOT NULL,
IdStatusTarefa INT NOT NULL,
Nome Varchar(20) Not Null,
Descricao Varchar(100) Not Null,
DataHoraTarefa Datetime Not Null,
DataHoraExclusao Datetime,
DataHoraConclusao Datetime,
DataHoraCriacao Datetime Not Null,
DataHoraAtualizacao Datetime);

GO
Insert Into StatusTarefa (Nome, DataHoraCriacao) Values ('ATIVA', GETDATE());

GO
Insert Into StatusTarefa (Nome, DataHoraCriacao) Values ('CONCLUÍDA', GETDATE());

GO
Insert Into StatusTarefa (Nome, DataHoraCriacao) Values ('EXCLUÍDA', GETDATE());

GO
Insert Into Tarefa (Nome, Descricao, IdStatusTarefa, DataHoraTarefa, DataHoraCriacao) Values ('Realizar exercicios.', 'Realizar exercicios de pilates', 1, GETDATE() + 1, GETDATE());

GO
Insert Into Tarefa (Nome, Descricao, IdStatusTarefa, DataHoraTarefa, DataHoraCriacao) Values ('Reunião trabalho.', 'Reunião sobre novo produto', 1, GETDATE() + 2, GETDATE());

GO
Insert Into Tarefa (Nome, Descricao, IdStatusTarefa, DataHoraTarefa, DataHoraCriacao) Values ('Presente Julia.', 'Comprar presente de aniversário da Júlia', 1, GETDATE() + 3, GETDATE());

GO
Insert Into HistoricoTarefa (IdTarefa,IdStatusTarefa, Nome, Descricao, DataHoraTarefa, DataHoraExclusao, DataHoraCriacao) Values (4,3, 'Enviar E-mail.', 'Enviar email para fornecedor', GETDATE() - 3, GETDATE() - 2, GETDATE());


GO