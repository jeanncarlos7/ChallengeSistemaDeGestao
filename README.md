# Sistema de Gestão

## Alunos: 
Felipe Torlai RM 550263
Felipe Pinheiro RM 550244
Gabriel Girami RM 98017
Gustavo Vinhola RM 98826
Jean Carlos RM 550430 

## Visão Geral
Este projeto é um sistema de gestão que permite a administração de produtos, tarefas, usuários e avaliações. 
Ele foi desenvolvido com o intuito de criar uma solução escalável e eficiente para o gerenciamento de dados em diferentes módulos.
O sistema inclui integração com bancos de dados relacionais (Oracle) e NoSQL (MongoDB), além de oferecer uma API RESTful para interagir com os diferentes modelos.

## Tecnologias Utilizadas:
C# com ASP.NET Core
Entity Framework Core (para integração com Oracle)
MongoDB.Driver (para integração com MongoDB)
Swagger/OpenAPI (para documentação da API)
Microsoft Extensions para gerenciamento de dependências e configurações

## Estrutura do Projeto
Camadas Principais:
  Models: Representam as entidades do sistema como Produto, Tarefa, Usuario, Avaliacao, Cliente.
  Repositorios: Contém interfaces e implementações dos repositórios para gerenciar operações de banco de dados (CRUD).
  Controllers: Controladores que expõem a API e lidam com as solicitações HTTP.
  Services: Serviços que encapsulam a lógica de negócios e comunicação com MongoDB.
  ConfiguracaoSingleton: Implementa o padrão Singleton para gerenciar a configuração central do sistema.
  Settings: Gerencia configurações específicas como conexões ao banco de dados.
  
## Funcionalidades
1. Gerenciamento de Produtos
  Listar Produtos: Retorna todos os produtos disponíveis no sistema.
  Adicionar Produto: Cadastra um novo produto.
  Atualizar Produto: Atualiza informações de um produto existente.
  Deletar Produto: Remove um produto.

2. Gerenciamento de Tarefas
  Listar Tarefas: Retorna todas as tarefas cadastradas.
  Adicionar Tarefa: Adiciona uma nova tarefa ao sistema.
  Atualizar Tarefa: Atualiza uma tarefa existente.
  Deletar Tarefa: Remove uma tarefa.

3. Gerenciamento de Usuários
  Listar Usuários: Retorna todos os usuários cadastrados.
  Adicionar Usuário: Cadastra um novo usuário no sistema.
  Atualizar Usuário: Atualiza as informações de um usuário existente.
  Deletar Usuário: Remove um usuário do sistema.

4. Avaliações de Usuários
  Listar Avaliações: Retorna todas as avaliações feitas no sistema.
  Adicionar Avaliação: Cadastra uma nova avaliação relacionada a um usuário.
  Atualizar Avaliação: Atualiza uma avaliação existente.
  Deletar Avaliação: Remove uma avaliação.

5. Gerenciamento de Clientes (MongoDB)
  Listar Clientes: Retorna todos os clientes cadastrados no MongoDB.
  Adicionar Cliente: Adiciona um novo cliente à base MongoDB.
  Atualizar Cliente: Atualiza informações de um cliente existente.
  Deletar Cliente: Remove um cliente do sistema MongoDB.

## Instalação e Configuração
Pré-requisitos:
  .NET SDK 7.0 ou superior: Para rodar e compilar a API.
  Visual Studio Code: IDE para desenvolvimento e execução do projeto.
  SQL Server ou Oracle: Servidor de banco de dados relacional utilizado no projeto.
  Docker (Opcional): Para rodar o banco de dados em containers Docker, caso prefira.
  Postman ou similar: Para testar as requisições HTTP da API.

## Passos para rodar o projeto:
1. Clone o repositório:
git clone https://github.com/jeanncarlos7/ChallengeSistemaDeGestao.git 

## Testes
Para rodar os testes, siga os passos abaixo:

1. Instalar pacotes de teste: Certifique-se de que você tem as bibliotecas de testes instaladas:
dotnet add package NUnit
dotnet add package NUnit3TestAdapter
dotnet add package Microsoft.NET.Test.Sdk

2. Executar testes:
dotnet test

## Contribuição
Sinta-se à vontade para contribuir com melhorias para o projeto. Para isso, siga o fluxo abaixo:

  Faça um Fork do projeto.
  Crie uma nova branch para sua funcionalidade (git checkout -b feature/nova-funcionalidade).
  Commit suas alterações (git commit -m 'Adiciona nova funcionalidade').
  Faça o Push para o branch (git push origin feature/nova-funcionalidade).
  Abra um Pull Request.

  ### Princípios SOLID
- **SRP (Princípio da Responsabilidade Única)**: Cada serviço implementa uma única responsabilidade, como `AuthService` para autenticação.
- **OCP (Princípio Aberto-Fechado)**: A aplicação utiliza interfaces para desacoplar implementações específicas.
  
## Licença
Este projeto está licenciado sob a MIT License - veja o arquivo LICENSE para mais detalhes.



