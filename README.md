# User Management System API

API REST desenvolvida em .NET para gerenciamento de usuários, utilizando PostgreSQL como banco de dados e Entity Framework Core para acesso aos dados.

O projeto segue uma arquitetura simples baseada em Repository Pattern, separando responsabilidades entre Controller, Repository e Model, além de implementar Soft Delete para manter histórico de dados.

---

## Tecnologias Utilizadas

- .NET / ASP.NET Core

- Entity Framework Core

- PostgreSQL

- Swagger (documentação da API)

- CORS habilitado para integração com frontend

- Repository Pattern

---


## Estrutura do Projeto
```
UserManagementSystem
│
├── Controllers
│   └── UsersController.cs
│
├── Models
│   └── User.cs
│
├── Repository
│   ├── Interface
│   │   └── IUserRepository.cs
│   └── UserRepository.cs
│
├── Data
│   └── AppDbContext.cs
│
└── Program.cs
```

## Modelo de Usuário

```csharp
public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public Guid UserType { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
```

## Campos

| Campo | Tipo | Descrição |
|------|------|-----------|
| Id | Guid | Identificador único do usuário |
| Name | string | Nome do usuário |
| Email | string | Email do usuário (único) |
| UserType | Guid | Tipo de usuário |
| CreatedAt | DateTime | Data de criação |
| UpdatedAt | DateTime | Data de atualização |
| DeletedAt | DateTime | Data de exclusão lógica |

---

## Funcionalidades

- Criar usuário
- Listar usuários
- Buscar usuário por ID
- Atualizar usuário parcialmente
- Deletar usuário (Soft Delete)
- Validação de email único

---

## Endpoints da API

### Criar Usuário

**POST**

```
/users
```

### Body

```json
{
  "name": "Lucas",
  "email": "lucas@email.com",
  "userType": "GUID_DO_TIPO"
}
```
### Respostas

| Status | Descrição |
|------|-----------|
| 201 | Usuário criado |
| 409 | Email já existe |

---

## Listar Usuários

**GET**
```
/users
```
Retorna todos os usuários que **não foram deletados**.

### Resposta

```json
[
  {
    "id": "guid",
    "name": "Lucas",
    "email": "lucas@email.com",
    "userType": "guid",
    "createdAt": "2026-03-16T00:00:00",
    "updatedAt": null,
    "deletedAt": null
  }
]
```

---

## Buscar Usuário por ID

**GET**
```
/users/{id}
```

### Respostas

| Status | Descrição |
|------|-----------|
| 200 | Usuário encontrado |
| 404 | Usuário não encontrado |

---

## Atualizar Usuário

**PATCH**
```
/users/{id}
```
Atualiza parcialmente os dados do usuário.

### Body
```json
{
  "name": "Novo Nome",
  "email": "novo@email.com"
}
```
### Regras

- Email deve ser único  
- Campos não enviados não são alterados  

### Respostas

| Status | Descrição |
|------|-----------|
| 200 | Usuário atualizado |
| 404 | Usuário não encontrado |
| 409 | Email já utilizado |

---

## Deletar Usuário

**DELETE**
```
/users/{id}
```
Realiza **Soft Delete**, marcando o campo **DeletedAt**.

### Respostas

| Status | Descrição |
|------|-----------|
| 204 | Usuário deletado |
| 404 | Usuário não encontrado |

---

## Soft Delete

Ao deletar um usuário, ele **não é removido do banco**, apenas recebe um valor no campo:

DeletedAt

Assim:

- Usuários deletados **não aparecem nas consultas**
- O histórico de dados **é preservado**

---

## Configuração do Banco de Dados

A conexão com PostgreSQL é definida no **appsettings.json**.

Exemplo:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=UserDb;Username=postgres;Password=postgres"
}
```
---

## Documentação Swagger

Após iniciar o projeto, acesse:

```
https://localhost:7082/swagger
```
---

## Integração com Frontend

CORS está habilitado para:

```
http://localhost:3000
```

Permitindo integração com aplicações frontend como **React** ou **Next.js**.
