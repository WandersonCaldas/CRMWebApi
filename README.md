# CRMWeb API

API REST desenvolvida em ASP.NET Core com Entity Framework Core e SQL Server para fins de estudo e aprendizado de arquitetura em camadas, modelagem de dados, migrations e desenvolvimento de APIs.

## Tecnologias

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* AutoMapper
* Swagger/OpenAPI

## Estrutura da Solução

```text
CRMWeb.Api
CRMWeb.Domain
CRMWeb.Infrastructure
```

### CRMWeb.Api

Responsável por:

* Controllers
* Configuração da aplicação
* Swagger
* AutoMapper
* Injeção de dependências

### CRMWeb.Domain

Responsável por:

* Entidades
* Enums
* Requests
* Responses

### CRMWeb.Infrastructure

Responsável por:

* DbContext
* Configurações das entidades
* Migrations
* Persistência de dados

## Schema do Banco

Todas as tabelas são criadas no schema:

```sql
crm
```

Incluindo a tabela de histórico de migrations:

```sql
crm.__EFMigrationsHistory
```

## Entidades Implementadas

### Cliente

Representa pessoas físicas ou jurídicas cadastradas no CRM.

Campos principais:

* Id
* TipoPessoa
* Nome
* CpfCnpj
* Email
* Telefone
* DataCadastro
* Ativo

Relacionamentos:

* 1:N Endereços
* 1:N Contatos
* 1:N Tarefas
* 1:N Agendas

---

### Endereço

Permite o cadastro de múltiplos endereços para um cliente.

Campos principais:

* Logradouro
* Número
* Complemento
* Bairro
* Cidade
* UF
* CEP
* Principal

Relacionamento:

* N:1 Cliente

---

### Contato

Representa pessoas de contato vinculadas a um cliente.

Campos principais:

* Nome
* Cargo
* Email
* Telefone
* Principal
* Ativo

Relacionamentos:

* N:1 Cliente
* 1:N Tarefas
* 1:N Agendas

---

### Tarefa

Representa atividades relacionadas a clientes e contatos.

Campos principais:

* Título
* Descrição
* DataVencimento
* Concluida
* DataConclusao
* Ativo

Relacionamentos:

* N:1 Cliente
* N:1 Contato (opcional)

---

### Agenda

Representa compromissos, reuniões, visitas e eventos relacionados a clientes e contatos.

Campos principais:

* Título
* Descrição
* DataInicio
* DataFim
* DiaTodo
* Ativo

Relacionamentos:

* N:1 Cliente
* N:1 Contato (opcional)

## Endpoints

### Tipos de Pessoa

```http
GET /tipos-pessoa
```

Retorno:

```json
[
  {
    "id": 1,
    "descricao": "Fisica"
  },
  {
    "id": 2,
    "descricao": "Juridica"
  }
]
```

---

### Clientes

```http
POST   /clientes
GET    /clientes
GET    /clientes/{id}
PUT    /clientes/{id}
DELETE /clientes/{id}
```

---

### Endereços

```http
POST   /enderecos/cliente/{clienteId}
GET    /enderecos/{id}
GET    /enderecos/cliente/{clienteId}
PUT    /enderecos/{id}
DELETE /enderecos/{id}
```

---

### Contatos

```http
POST   /contatos/cliente/{clienteId}
GET    /contatos
GET    /contatos/{id}
GET    /contatos/cliente/{clienteId}
PUT    /contatos/{id}
DELETE /contatos/{id}
```

---

### Tarefas

```http
POST   /tarefas/cliente/{clienteId}
GET    /tarefas
GET    /tarefas/{id}
GET    /tarefas/cliente/{clienteId}
PUT    /tarefas/{id}
PATCH  /tarefas/{id}/concluir
PATCH  /tarefas/{id}/reabrir
DELETE /tarefas/{id}
```

---

### Agendas

```http
POST   /agendas
GET    /agendas
GET    /agendas/{id}
PUT    /agendas/{id}
DELETE /agendas/{id}
```

## Migrations

Criar migration:

```bash
dotnet ef migrations add NomeDaMigration --project CRMWeb.Infrastructure --startup-project CRMWeb.Api
```

Aplicar migrations:

```bash
dotnet ef database update --project CRMWeb.Infrastructure --startup-project CRMWeb.Api
```

## Objetivo do Projeto

Este projeto tem como objetivo servir como ambiente de estudo para:

* ASP.NET Core
* Entity Framework Core
* AutoMapper
* Migrations
* SQL Server
* Relacionamentos entre entidades
* Desenvolvimento de APIs REST
* Boas práticas de modelagem de dados
* Organização de tarefas e compromissos
* Gerenciamento de agenda corporativa

## Funcionalidades Implementadas

* Cadastro de clientes
* Cadastro de endereços
* Cadastro de contatos
* Controle de tarefas
* Controle de agenda
* Relacionamentos entre entidades
* Persistência com Entity Framework Core
* Migrations automatizadas
* Documentação Swagger
* Mapeamento com AutoMapper
