# DesafioSistemaTarefas

A Aplicação deve adicionar um status contendo título, descrição e data da tarefa. A data da tarefa sempre deve ser maior que o dia e hora atual. Quando uma tarefa está ativa, 
ela sempre está na tabela "Tarefa", e quando é excluída ou concluída sempre vai para a tabela "HistoricoTarefa", sendo apagada da tabela "Tarefa". E quando é reativada, 
a tarefa volta para a tabela "Tarefa" e é apagada da tabela "HistoricoTarefa".

Aplicação criada com .NET 6.0, utilizando uma arquitetura  de camadas e voltada ao banco de dados SQLServer. O front-end está em angular 15 e o desenvolvimento foi realizado no visual studio 2022 e no visual code;
Para mensageria, foi utilizado o RabbitMQ e o MassTransit.

Por ser tratar de um teste e de tamaho relativamente pequeno, alguns fatos foram feitos para facilitar ou evitar o excesso de complexidade na execução do mesmo, como:

- Front-end na mesma solução do back-end.
- Arquitetura monolítica
- Controllers de mensageria(Publish e Consummer) no mesmo projeto e controllers das APIs
- Ausência de necessidade de autenticação nos metodos
- Ausência de necessidade de autenticação nos metodos
- Generelização e simplificação de erros, mensagens, exceções e tratamentos.
- Apontamento de ambiente ou endereço diretamente no metodo.
- Criação simplificada de contexto de banco de dados.

Para a execução do projeto localmente, basta seguir os passos a seguir:

1) Executar o script disponível no caminho "~\repos\DesafioSistemaTarefas\DesafioSistemaTarefas.DB\Scripts"
2) Caso necessário, alterar a connectionString para o servidor e confiogurações escolhido. O atual é ""DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DesafioSistemaTarefas;Trusted_Connection=True;MultipleActiveResultSets=true;""
3) No visual studio, realizar a execução do projeto API (preferencialmente como administrador).
4) No visual code(Ou IDE de sua preferencia), abrir a pasta do front("~\repos\DesafioSistemaTarefas\DesafioSistemaTarefas.Front") e executar no terminal "npm install" e qualquer outro comando para restauração de dependencias ou extensões
5) Ainda na IDE e no terminal de sua preferencia, digitar o comando "ng serve"

Para os serviços de mensageria, é necessário instalar o RabbitMQ localmente(https://www.rabbitmq.com/install-windows.html#chocolatey) ou apontar no código (PublishController e ConsumerController) o endereço dos servidores de escuta e publicação.