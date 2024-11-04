# Sistema de Gestão

## Alunos: 

- Felipe Torlai RM 550263
- Felipe Pinheiro RM 550244
- Gabriel Girami RM 98017
- Gustavo Vinhola RM 98826
- Jean Carlos RM 550430

## Introdução

- O gerenciamento eficaz de dados e a capacidade de oferecer recomendações personalizadas são diferenciais importantes em um ambiente competitivo. O Sistema de Gestão foi criado com o objetivo de atender a essa necessidade, combinando a praticidade do gerenciamento de tarefas e avaliações. Integrado com APIs externas, como o ViaCEP, o sistema oferece uma experiência de usuário otimizada, agilizando processos como busca de endereços e avaliação de clientes. Além disso, ele permite que a equipe tome decisões baseadas em dados, utilizando um banco de dados relacional e um banco NoSQL para flexibilizar o armazenamento e a recuperação de informações.

## Visão Geral

- O Sistema de Gestão é uma aplicação desenvolvida para fornecer funcionalidades de gerenciamento de tarefas, avaliações de usuários e recomendações de produtos ou serviços.
- Ele foi desenvolvido com o intuito de criar uma solução escalável e eficiente para o gerenciamento de dados em diferentes módulos.
- O sistema inclui integração com bancos de dados relacionais (Oracle) e NoSQL (MongoDB), além de oferecer uma API RESTful para interagir com os diferentes modelos.
- A aplicação inclui integração com serviços externos, como o ViaCEP, para buscar dados de endereço a partir de CEPs brasileiros.

## Tecnologias Utilizadas:

- ASP.NET Core: Estrutura principal do back-end.
- Entity Framework Core: Gerenciamento de banco de dados relacional.
- MongoDB: Para armazenamento de dados não-relacionais.
- ML.NET: Para implementação de Machine Learning, incluindo modelos de recomendação e análise de sentimento.
- xUnit: Para testes unitários, de integração e de sistema.
- HttpClient: Para integração com a API ViaCEP.
- Swagger: Para documentação e visualização da API.
- Moq (para mocks)

## Estrutura do Projeto

- Models: Representam as entidades do sistema como Produto, Tarefa, Usuario, Avaliacao, Cliente.
- Repositorios: Contém interfaces e implementações dos repositórios para gerenciar operações de banco de dados (CRUD).
- Controllers: Controladores que expõem a API e lidam com as solicitações HTTP.
- Services: Serviços que encapsulam a lógica de negócios e comunicação com MongoDB.
- ConfiguracaoSingleton: Implementa o padrão Singleton para gerenciar a configuração central do sistema.
- Settings: Gerencia configurações específicas como conexões ao banco de dados.
  
## Funcionalidades

- Gerenciamento de Tarefas: Cadastro, atualização e exclusão de tarefas.
- Gerenciamento de Usuários e Avaliações: Criação e atualização de usuários com avaliações detalhadas.
- Integração com API Externa (ViaCEP): Consulta de endereços por CEP via API externa.

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
- .NET SDK 7.0 ou superior: Para rodar e compilar a API.
- Visual Studio Code: IDE para desenvolvimento e execução do projeto.
- SQL Server ou Oracle: Servidor de banco de dados relacional utilizado no projeto.
- Docker (Opcional): Para rodar o banco de dados em containers Docker, caso prefira.
- Postman ou similar: Para testar as requisições HTTP da API.
- MongoDB.
- Dependências listadas no Program.cs.

## Pastas Importantes

- Controllers: Define os controladores para rotas da API.
- Models: Modelos de dados para a aplicação.
- Repositorios: Repositórios para interações com o banco de dados.
- Settings: Configurações da aplicação, incluindo configurações de banco de dados.
- Tests: Contém os testes unitários, de integração e de sistema para diferentes funcionalidades.

## Testes

- A solução contém testes unitários, de integração e de sistema usando o xUnit. 

## Testes Específicos

- Testes Unitários: Para testar unidades individuais de código.
- Testes de Integração: Validam interações entre componentes, como o UsuarioRepository.
- Testes de Sistema: Testam o comportamento da aplicação como um todo, simulando a comunicação entre vários componentes.


### Princípios SOLID

- **SRP (Princípio da Responsabilidade Única)**: Cada serviço implementa uma única responsabilidade, como `AuthService` para autenticação.
- **OCP (Princípio Aberto-Fechado)**: A aplicação utiliza interfaces para desacoplar implementações específicas.

## Conclusão

- O Sistema de Gestão representa uma solução completa para empresas que buscam melhorar a eficiência no gerenciamento de dados e a interação com clientes. Com sua arquitetura modular e integrações externas, o sistema facilita a execução de processos de forma prática e intuitiva. A implementação de Machine Learning para recomendações personalizadas contribui para a criação de experiências mais significativas para os usuários, permitindo que o sistema evolua com base nos dados e necessidades dos clientes. Essa solução posiciona a organização para atuar de maneira mais estratégica e baseada em dados, aumentando a competitividade e a satisfação do cliente.
  
## Licença

- Este projeto está licenciado sob a MIT License - veja o arquivo LICENSE para mais detalhes.



