# API REST .NET 8

Esta é uma API REST desenvolvida em .NET 8 com Entity Framework Core e PostgreSQL.

## Estrutura do Projeto

### Entidades

#### User

- `id` (int, auto-incremento)
- `name` (string, obrigatório)
- `email` (string, obrigatório, único)
- `password` (string, obrigatório)
- `created_at` (DateTime, padrão: now())
- `updated_at` (DateTime, atualizado automaticamente)

#### Customer

- `id` (int, auto-incremento)
- `name` (string, obrigatório)
- `email` (string, obrigatório, único)
- `cpf` (string, obrigatório, único)
- `created_at` (DateTime, padrão: now())
- `updated_at` (DateTime, atualizado automaticamente)
- `addresses` (relação 1:N com Address)

#### Address

- `id` (int, auto-incremento)
- `customer_id` (int, chave estrangeira para Customer)
- `address` (string, obrigatório)
- `number` (string, obrigatório)
- `complement` (string)
- `zip_code` (string, obrigatório)
- `created_at` (DateTime, padrão: now())
- `updated_at` (DateTime, atualizado automaticamente)
- `customer` (relação N:1 com Customer)

## Endpoints da API

### Users

- `POST /users/login` - Fazer login
- `POST /users` - Criar novo usuário
- `PATCH /users/{id}` - Atualizar usuário
- `DELETE /users/{id}` - Deletar usuário
- `GET /users/{id}` - Buscar usuário por ID
- `GET /users` - Listar todos os usuários

### Customers

- `GET /customers` - Listar todos os clientes
- `POST /customers` - Criar novo cliente
- `PATCH /customers/{id}` - Atualizar cliente
- `DELETE /customers/{id}` - Deletar cliente
- `GET /customers/{id}` - Buscar cliente por ID

### Addresses

- `GET /addresses` - Listar todos os endereços
- `GET /addresses/{id}` - Buscar endereço por ID
- `POST /addresses` - Criar novo endereço
- `PUT /addresses/{id}` - Atualizar endereço
- `DELETE /addresses/{id}` - Deletar endereço

### Customer Addresses

- `GET /customers/{customerId}/addresses` - Listar endereços de um cliente
- `POST /customers/{customerId}/addresses` - Criar endereço para um cliente
- `GET /customers/{customerId}/addresses/{id}` - Buscar endereço específico de um cliente
- `PATCH /customers/{customerId}/addresses/{id}` - Atualizar endereço de um cliente
- `DELETE /customers/{customerId}/addresses/{id}` - Deletar endereço de um cliente

## Configuração

### Pré-requisitos

- .NET 8 SDK
- PostgreSQL
- Docker (opcional)

### Configuração do Banco de Dados

1. Configure a string de conexão no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;Database=testdb;User=postgres;Password=12345;"
  }
}
```

2. Execute as migrações:

```bash
dotnet ef database update
```

### Executando o Projeto

1. Restaure as dependências:

```bash
dotnet restore
```

2. Execute o projeto:

```bash
dotnet run
```

3. Acesse a documentação Swagger em: `https://localhost:7001/swagger`

## Docker

Para executar com Docker:

```bash
docker-compose up -d
```

## Estrutura de Pastas

```
dotnet-api/
├── Models/                 # Entidades do banco de dados
├── Controllers/            # Controllers da aplicação
├── Infra/                  # Infraestrutura
│   ├── ApplicationDbContext.cs
│   └── Repository/         # Repositórios
├── Routes/                 # Endpoints da API
├── Migrations/             # Migrações do Entity Framework
├── Program.cs              # Configuração da aplicação
└── appsettings.json        # Configurações
```

## Tecnologias Utilizadas

- .NET 8
- Entity Framework Core
- PostgreSQL
- Swagger/OpenAPI
- Minimal APIs
