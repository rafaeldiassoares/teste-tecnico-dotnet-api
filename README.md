# 🚀 .NET 8 API - Sistema de Gerenciamento de Usuários, Clientes e Endereços

API REST desenvolvida em .NET 8 para o teste técnico da empresa Einstein, implementando um sistema de gerenciamento de usuários, clientes e endereços com foco em qualidade de código, arquitetura escalável e boas práticas.

## 🎯 Objetivo

Desenvolver uma API REST robusta e escalável que atenda aos requisitos do teste técnico, demonstrando proficiência em:

- **Clean Code e princípios SOLID**
- **Arquitetura em camadas**
- **Entity Framework Core**
- **Tratamento de exceções**
- **Documentação via Swagger**
- **Containerização com Docker**

## 🛠️ Tecnologias Utilizadas

### Core Technologies

- **.NET 8**: Framework moderno e robusto para desenvolvimento
- **C#**: Linguagem de programação orientada a objetos
- **Entity Framework Core 8.0.6**: ORM para acesso a dados
- **PostgreSQL 16**: Banco de dados relacional robusto e escalável

### API & Documentation

- **ASP.NET Core Minimal APIs**: Endpoints da API
- **Swashbuckle.AspNetCore**: Documentação interativa da API
- **OpenAPI/Swagger**: Especificação de API

### Database & Infrastructure

- **Npgsql.EntityFrameworkCore.PostgreSQL**: Provider PostgreSQL para EF Core
- **Docker Compose**: Orquestração de serviços

## 🏗️ Arquitetura do Projeto

O projeto segue uma arquitetura em camadas bem definidas:

```
dotnet-api/
├── Controllers/          # Camada de controle - lógica de negócio
├── Models/               # Camada de modelo - entidades do banco
├── Infra/                # Camada de infraestrutura
│   ├── ApplicationDbContext.cs    # Contexto do Entity Framework
│   └── Repository/       # Padrão Repository
│       ├── User/         # Repositórios de usuários
│       ├── Customer/     # Repositórios de clientes
│       └── Address/      # Repositórios de endereços
├── Routes/               # Configuração de rotas da API
├── Migrations/           # Migrações do banco de dados
└── Program.cs            # Configuração da aplicação
```

## 📊 Modelo de Dados

### Diagrama do Banco de Dados

```sql
users
├── id (PK, autoincrement)
├── name (varchar, obrigatório)
├── email (varchar, unique, obrigatório)
├── password (varchar, obrigatório)
├── created_at (timestamp)
└── updated_at (timestamp)

customers
├── id (PK, autoincrement)
├── name (varchar, obrigatório)
├── email (varchar, unique, obrigatório)
├── cpf (varchar, unique, obrigatório)
├── created_at (timestamp)
└── updated_at (timestamp)

addresses
├── id (PK, autoincrement)
├── customer_id (FK → customers.id)
├── address (varchar, obrigatório)
├── number (varchar, obrigatório)
├── complement (varchar, nullable)
├── zip_code (varchar, obrigatório)
├── created_at (timestamp)
└── updated_at (timestamp)
```

## 📋 Endpoints da API

### 🔑 UserController

Gerencia os usuários do sistema.

| Método | Rota          | Descrição              |
| ------ | ------------- | ---------------------- |
| POST   | `/users`      | Cadastrar novo usuário |
| PATCH  | `/users/{id}` | Atualizar usuário      |
| DELETE | `/users/{id}` | Deletar usuário        |
| GET    | `/users/{id}` | Buscar usuário por ID  |
| GET    | `/users`      | Listar usuários        |

### 👥 CustomerController

Gerencia os clientes da aplicação.

| Método | Rota              | Descrição                     |
| ------ | ----------------- | ----------------------------- |
| POST   | `/customers`      | Cadastrar novo cliente        |
| PATCH  | `/customers/{id}` | Atualizar cliente             |
| DELETE | `/customers/{id}` | Deletar cliente               |
| GET    | `/customers/{id}` | Buscar cliente por ID         |
| GET    | `/customers`      | Listar clientes com endereços |

### 🏠 CustomerAddressesController

Gerencia os endereços dos clientes.

| Método | Rota                                     | Descrição          |
| ------ | ---------------------------------------- | ------------------ |
| POST   | `/customers/{customerId}/addresses`      | Cadastrar endereço |
| PATCH  | `/customers/{customerId}/addresses/{id}` | Atualizar endereço |
| DELETE | `/customers/{customerId}/addresses/{id}` | Deletar endereço   |
| GET    | `/customers/{customerId}/addresses/{id}` | Buscar endereço    |
| GET    | `/customers/{customerId}/addresses`      | Listar endereços   |

## 🚀 Instalação e Execução

### Pré-requisitos

- .NET 8 SDK
- Docker e Docker Compose
- PostgreSQL (opcional para desenvolvimento local)

### 1. Clone o repositório

```bash
git clone git@github.com:rafaeldiassoares/teste-tecnico-dotnet-api.git
cd dotnet-api
```

### 2. Configuração do ambiente

```bash
# Copie o arquivo de exemplo (se existir)
cp appsettings.Development.json.example appsettings.Development.json

# Configure a string de conexão no appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;Database=testdb;Username=postgres;Password=12345;"
  }
}
```

### 3. Execução com Docker (Recomendado)

```bash
# Construir e executar todos os serviços
docker-compose up --build

# A API estará disponível em: http://localhost:5001
# Documentação Swagger: http://localhost:5001/swagger
```

### 4. Execução local (Desenvolvimento)

```bash
# Restaurar dependências
dotnet restore

# Executar migrações
dotnet ef database update

# Executar a aplicação
dotnet run

# A API estará disponível em: http://localhost:5001
# Documentação Swagger: http://localhost:5001/swagger
```

## 🐳 Docker

### Estrutura Docker

- **docker-compose.yml**: Orquestração dos serviços
- **PostgreSQL**: Banco de dados

### Serviços Docker

- **PostgreSQL**: Porta 5432
- **Volumes**: Persistência de dados

### Comandos Docker

```bash
# Construir e executar
docker-compose up --build

# Executar em background
docker-compose up -d

# Parar serviços
docker-compose down

# Visualizar logs
docker-compose logs -f postgres

# Acessar container
docker-compose exec postgres psql -U postgres -d testdb
```

## 📝 Estrutura de Resposta da API

### Formato Padrão

```json
{
  "id": 1,
  "name": "Nome do Usuário",
  "email": "usuario@email.com",
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-01T00:00:00Z"
}
```

### Formato de Erro

```json
{
  "message": "Erro ao criar usuário",
  "error": "Detalhes do erro"
}
```

## 🔧 Configurações

### Connection String

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;Database=testdb;Username=postgres;Password=12345;"
  }
}
```
