# TrackZone - Sistema de Gestão de Motos API

## 📋 Descrição da Solução

O **TrackZone** é uma API RESTful desenvolvida em .NET 9 para gerenciamento completo de motos, usuários, operações e status. O sistema oferece:

- **Gestão de Usuários**: Controle de acesso com diferentes perfis (Admin, Gerente, Operador)
- **Gestão de Motos**: Cadastro, atualização e controle de veículos
- **Gestão de Operações**: Registro de operações realizadas (CHECK_IN, CHECK_OUT)
- **Gestão de Status**: Controle de estados das motos (PENDENTE, PRONTA, MANUTENCAO_AGENDADA, etc.)
- **Autenticação JWT**: Sistema de segurança com tokens JWT
- **Machine Learning**: Análise preditiva de status de motos usando ML.NET
- **Health Checks**: Monitoramento de saúde da aplicação
- **Versionamento**: Suporte a múltiplas versões da API (v1.0 e v2.0)
- **Testes Automatizados**: Testes unitários e de integração com xUnit

## 🎯 Objetivos Acadêmicos

### Conceitos .NET Demonstrados:
- **API RESTful**: Implementação completa com verbos HTTP adequados
- **Entity Framework Core**: ORM para acesso a dados com Oracle Database
- **Arquitetura em Camadas**: Controllers, Services, Repositories e DTOs
- **Injeção de Dependência**: Padrão IoC implementado
- **Validação de Dados**: Data Annotations e ModelState
- **Documentação**: Swagger/OpenAPI configurado com versionamento
- **Paginação**: Implementação de paginação em todos os endpoints
- **HATEOAS**: Links de navegação nos responses
- **Tratamento de Erros**: Try-catch com logging estruturado
- **Clean Architecture**: Separação clara de responsabilidades
- **Autenticação JWT**: Sistema de segurança com tokens JWT
- **Machine Learning**: ML.NET para análise preditiva
- **Health Checks**: Monitoramento de saúde da aplicação
- **Versionamento**: Suporte a múltiplas versões da API
- **Testes Automatizados**: Testes unitários e de integração

## 👥 Integrantes

- Leticia Cristina Dos Santos Passos RM: 555241
- André Rogério Vieira Pavanela Altobelli Antunes RM: 554764
- Enrico Figueiredo Del Guerra RM: 558604
- **Instituição**: FIAP - .NET

## 🏗️ Arquitetura da Solução

### Desenho da Arquitetura .NET

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Controllers   │ -> │    Services     │ -> │  Repositories   │
│   (API Layer)   │    │ (Business Logic)│    │  (Data Access)  │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                       │                       │
         v                       v                       v
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   DTOs/Models   │    │   AutoMapper    │    │ Entity Framework│
│ (Data Transfer) │    │  (Object Map)   │    │   Core + SQL    │
└─────────────────┘    └─────────────────┘    └─────────────────┘
```

### Fluxo de Funcionamento:

1. **Controllers**: Recebem requisições HTTP e coordenam operações
2. **Services**: Implementam regras de negócio e validações
3. **Repositories**: Gerenciam acesso e persistência de dados
4. **Entity Framework**: ORM para mapeamento objeto-relacional
5. **DTOs**: Transferência de dados entre camadas
6. **AutoMapper**: Mapeamento automático entre entidades e DTOs

### Justificativa Técnica:

A arquitetura segue os princípios **SOLID** e **Clean Architecture**:

- **Separação de Responsabilidades**: Cada camada tem uma função específica
- **Inversão de Dependência**: Services dependem de abstrações (interfaces)
- **Single Responsibility**: Cada classe tem uma única responsabilidade
- **Open/Closed**: Aberto para extensão, fechado para modificação

### Estrutura do Projeto

```
challenge-3-net/
├── Controllers/          # Controllers RESTful
├── Data/                # Contexto do Entity Framework
├── Models/              # Entidades de domínio e DTOs
│   ├── DTOs/           # Data Transfer Objects
│   └── *.cs            # Entidades de domínio
├── Repositories/        # Camada de acesso a dados
│   ├── Interfaces/     # Contratos dos repositórios
│   └── *.cs            # Implementações dos repositórios
├── Services/           # Camada de negócio
│   ├── Interfaces/     # Contratos dos serviços
│   ├── Mapping/        # Configuração do AutoMapper
│   └── *.cs            # Implementações dos serviços
└── Program.cs          # Configuração da aplicação
```

## 🚀 Tecnologias Utilizadas

### Framework e Linguagem:
- **.NET 9** - Framework principal
- **C# 12** - Linguagem de programação
- **ASP.NET Core Web API** - Framework para APIs REST

### Acesso a Dados:
- **Entity Framework Core 9** - ORM para acesso a dados
- **Oracle Database** - Banco de dados relacional
- **Code First** - Migrations para criação do banco

### Padrões e Bibliotecas:
- **AutoMapper** - Mapeamento de objetos
- **BCrypt.Net-Next** - Criptografia de senhas
- **FluentValidation** - Validação de dados
- **Swagger/OpenAPI** - Documentação interativa da API com versionamento
- **JWT Bearer Authentication** - Autenticação e autorização
- **ML.NET 4.0** - Machine Learning para análise preditiva
- **Health Checks** - Monitoramento de saúde da aplicação
- **API Versioning** - Controle de versões da API

### Testes:
- **xUnit** - Framework de testes unitários
- **Moq** - Framework de mocking para testes
- **WebApplicationFactory** - Testes de integração
- **Microsoft.NET.Test.Sdk** - SDK de testes

### Arquitetura:
- **Repository Pattern** - Padrão de acesso a dados
- **Dependency Injection** - Inversão de controle
- **DTO Pattern** - Transferência de dados
- **Clean Architecture** - Separação de responsabilidades

## 🚀 Como Executar o Projeto

### Pré-requisitos

1. **.NET 9 SDK** instalado
2. **SQL Server** (LocalDB, Express ou Developer)
3. **Visual Studio 2022** (recomendado) ou **Visual Studio Code**
4. **Git** instalado

### 🔧 Configuração do Visual Studio 2022

Para uma experiência otimizada, configure o Visual Studio:

1. **Instalar Extensões**:
   - Entity Framework Core Tools
   - Swagger/OpenAPI Tools

2. **Configurar Connection String**:
   - Abra `appsettings.json`
   - Verifique se a connection string está correta:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SistemaGestaoMotos;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

3. **Configurar Projeto de Inicialização**:
   - Clique com botão direito no projeto `challenge-3-net`
   - Selecione "Set as Startup Project"

### 1. Clone do Repositório

```bash
git clone https://github.com/lecristina/challenge3-net.git
cd challenge3-net
```

### 2. Restaurar Pacotes NuGet

```bash
dotnet restore
```

### 3. Configurar Banco de Dados

```bash
# Criar as migrações
dotnet ef migrations add InitialCreate

# Aplicar as migrações ao banco
dotnet ef database update
```

### 4. Executar a Aplicação

#### Opção A: Via Terminal
```bash
dotnet run
```

#### Opção B: Via Visual Studio 2022
1. Abra o arquivo `challenge-3-net.sln` no Visual Studio
2. Configure o projeto `challenge-3-net` como projeto de inicialização
3. Pressione **F5** ou clique em **Iniciar** (botão verde)
4. O Visual Studio irá:
   - Compilar o projeto automaticamente
   - Iniciar a aplicação
   - Abrir o navegador com o Swagger

#### Opção C: Via Visual Studio Code
1. Abra a pasta do projeto no VS Code
2. Pressione **Ctrl+Shift+P** e digite "Tasks: Run Task"
3. Selecione "build" para compilar
4. Pressione **F5** para executar com debug
5. Ou use o terminal integrado: `dotnet run`

### 5. Acessar a API

Após execução, acesse:
- **API**: https://localhost:5001 ou http://localhost:5000
- **Swagger**: https://localhost:5001/ ou http://localhost:5000/ (raiz)
- **Health Check**: https://localhost:5001/health

## 🔍 Debugging e Troubleshooting

### Visual Studio 2022 - Dicas de Debug

1. **Breakpoints**:
   - Coloque breakpoints nos Controllers para debugar requisições
   - Use F10 (Step Over) e F11 (Step Into) para navegar pelo código

2. **Output Window**:
   - Visualize logs em tempo real: `View > Output > Show output from: Web Server`

3. **Database Explorer**:
   - Conecte ao banco via `View > SQL Server Object Explorer`
   - Verifique se as tabelas foram criadas corretamente

4. **Package Manager Console**:
   - Use para comandos EF Core: `Add-Migration`, `Update-Database`

### Problemas Comuns

**Erro de Connection String**:
```bash
# Verifique se o SQL Server LocalDB está rodando
sqllocaldb info
sqllocaldb start mssqllocaldb
```

**Erro de Migrations**:
```bash
# Remova e recrie as migrations
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

**Porta já em uso**:
```bash
# Use uma porta diferente
dotnet run --urls "https://localhost:5002;http://localhost:5001"
```

## 🧪 Testes da Aplicação via Swagger

**Acesse**: https://localhost:5001/ ou http://localhost:5000/ (raiz)

### 1. Health Check

```
GET /health
```
**Resposta esperada**:
```json
{
  "status": "Healthy",
  "timestamp": "2025-09-29T11:54:53.0169604Z",
  "environment": "Production",
  "hasConnectionString": true,
  "connectionStringLength": 193,
  "environmentVariables": {
    "dB_SERVER": "sqlserver-trackzone-net-2621119442.database.windows.net",
    "dB_DATABASE": "SistemaGestaoMotos",
    "dB_USERNAME": "admsql",
    "hasPassword": true,
    "aspnetcorE_ENVIRONMENT": "Production"
  }
}
```

---

## 📚 DOCUMENTAÇÃO COMPLETA DE ENDPOINTS

### 🔐 **ENDPOINTS DE AUTENTICAÇÃO** (`/api/v{version}/auth`)

#### 1. Login (Gerar Token JWT)
```http
POST /api/v1/auth/login
POST /api/v2/auth/login
Content-Type: application/json
```
**Body**:
```json
{
  "email": "admin@empresa.com",
  "senha": "123456"
}
```

**Resposta** (200):
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2025-01-16T15:00:00Z",
  "tokenType": "Bearer"
}
```

#### 2. Validar Token JWT
```http
GET /api/v1/auth/validate
GET /api/v2/auth/validate
Authorization: Bearer {token}
```

**Resposta** (200):
```json
{
  "userId": "1",
  "email": "admin@empresa.com",
  "role": "ADMIN",
  "nomeFilial": "Empresa Exemplo",
  "message": "Token JWT válido."
}
```

---

### 👥 **ENDPOINTS DE USUÁRIOS** (`/api/v{version}/usuarios`)

#### 1. Listar Usuários
```http
GET /api/v1/usuarios?pageNumber=1&pageSize=10
GET /api/v2/usuarios?pageNumber=1&pageSize=10
```

#### 2. Buscar Usuário por ID
```http
GET /api/v1/usuarios/{id}
GET /api/v2/usuarios/{id}
```

#### 3. Buscar Usuário por Email
```http
GET /api/v1/usuarios/email/{email}
GET /api/v2/usuarios/email/{email}
```

#### 4. Criar Usuário
```http
POST /api/v1/usuarios
POST /api/v2/usuarios
Content-Type: application/json
```
**Body**:
```json
{
  "nomeFilial": "Nova Empresa",
  "email": "novo@empresa.com",
  "senha": "123456",
  "cnpj": "98.765.432/0001-10",
  "endereco": "Av. Principal, 456",
  "telefone": "(11) 88888-8888",
  "perfil": "ADMIN"
}
```

#### 5. Atualizar Usuário
```http
PUT /api/v1/usuarios/{id}
PUT /api/v2/usuarios/{id}
Content-Type: application/json
```

#### 6. Deletar Usuário
```http
DELETE /api/v1/usuarios/{id}
DELETE /api/v2/usuarios/{id}
```

---

### 🏍️ **ENDPOINTS DE MOTOS** (`/api/v{version}/motos`)

#### 1. Listar Motos
```http
GET /api/v1/motos?pageNumber=1&pageSize=10
GET /api/v2/motos?pageNumber=1&pageSize=10
```

#### 2. Buscar Moto por ID
```http
GET /api/v1/motos/{id}
GET /api/v2/motos/{id}
```

#### 3. Buscar Moto por Placa
```http
GET /api/v1/motos/placa/{placa}
GET /api/v2/motos/placa/{placa}
```

#### 4. Buscar Moto por Chassi
```http
GET /api/v1/motos/chassi/{chassi}
GET /api/v2/motos/chassi/{chassi}
```

#### 5. Listar Motos por Usuário
```http
GET /api/v1/motos/usuario/{usuarioId}?pageNumber=1&pageSize=10
GET /api/v2/motos/usuario/{usuarioId}?pageNumber=1&pageSize=10
```

#### 6. Criar Moto
```http
POST /api/v1/motos
POST /api/v2/motos
Content-Type: application/json
```
**Body**:
```json
{
  "placa": "XYZ5678",
  "chassi": "9BWHE21JX24067890",
  "motor": "Yamaha MT-09 900cc",
  "usuarioId": 1
}
```

---

### ⚙️ **ENDPOINTS DE OPERAÇÕES** (`/api/v{version}/operacoes`)

#### 1. Listar Operações
```http
GET /api/v1/operacoes?pageNumber=1&pageSize=10
GET /api/v2/operacoes?pageNumber=1&pageSize=10
```

#### 2. Buscar Operação por ID
```http
GET /api/v1/operacoes/{id}
GET /api/v2/operacoes/{id}
```

#### 3. Listar Operações por Moto
```http
GET /api/v1/operacoes/moto/{motoId}?pageNumber=1&pageSize=10
GET /api/v2/operacoes/moto/{motoId}?pageNumber=1&pageSize=10
```

#### 4. Listar Operações por Usuário
```http
GET /api/v1/operacoes/usuario/{usuarioId}?pageNumber=1&pageSize=10
GET /api/v2/operacoes/usuario/{usuarioId}?pageNumber=1&pageSize=10
```

#### 5. Criar Operação
```http
POST /api/v1/operacoes
POST /api/v2/operacoes
Content-Type: application/json
```
**Body**:
```json
{
  "motoId": 1,
  "tipo": "CHECK_IN",
  "usuarioId": 1,
  "observacoes": "Check-in para manutenção preventiva"
}
```

---

### 📊 **ENDPOINTS DE STATUS MOTOS** (`/api/v{version}/statusmotos`)

#### 1. Listar Status
```http
GET /api/v1/statusmotos?pageNumber=1&pageSize=10
GET /api/v2/statusmotos?pageNumber=1&pageSize=10
```

#### 2. Buscar Status por ID
```http
GET /api/v1/statusmotos/{id}
GET /api/v2/statusmotos/{id}
```

#### 3. Listar Status por Moto
```http
GET /api/v1/statusmotos/moto/{motoId}?pageNumber=1&pageSize=10
GET /api/v2/statusmotos/moto/{motoId}?pageNumber=1&pageSize=10
```

#### 4. Listar Status por Usuário
```http
GET /api/v1/statusmotos/usuario/{usuarioId}?pageNumber=1&pageSize=10
GET /api/v2/statusmotos/usuario/{usuarioId}?pageNumber=1&pageSize=10
```

#### 5. Criar Status
```http
POST /api/v1/statusmotos
POST /api/v2/statusmotos
Content-Type: application/json
```
**Body**:
```json
{
  "motoId": 1,
  "status": "PENDENTE",
  "area": "Oficina Principal",
  "usuarioId": 1
}
```

---

### 🤖 **ENDPOINTS DE MACHINE LEARNING** (`/api/v2/ml`) - **APENAS VERSÃO 2.0**

#### 1. Predição de Próximo Status
```http
POST /api/v2/ml/predict-next-status
Authorization: Bearer {token}
Content-Type: application/json
```
**Body**:
```json
{
  "motoId": 1,
  "usuarioId": 1,
  "tipoOperacao": "CHECK_IN",
  "statusAtual": "PENDENTE"
}
```

**Resposta** (200):
```json
{
  "predictedStatus": "REPARO_SIMPLES",
  "score": [0.1, 0.3, 0.6, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0]
}
```

---

### 🏥 **ENDPOINTS DE HEALTH CHECK** (`/api/v{version}/health`)

#### 1. Health Check Geral
```http
GET /api/v1/health
GET /api/v2/health
```

#### 2. Health Check do Banco de Dados
```http
GET /api/v1/health/database
GET /api/v2/health/database
```

#### 3. Health Check da Memória
```http
GET /api/v1/health/memory
GET /api/v2/health/memory
```

#### 4. Health Check Padrão (.NET)
```http
GET /health/ready
GET /health/live
```

---

### 🔍 **ENDPOINTS DE DEBUG** (Desenvolvimento)

#### 1. Dados de Debug
```http
GET /admin/data
```

#### 2. Debug de Usuários
```http
GET /debug/usuarios
```

---

## 🔐 **AUTENTICAÇÃO E AUTORIZAÇÃO**

### **Como usar JWT:**

1. **Fazer Login:**
```http
POST /api/v2/auth/login
Content-Type: application/json

{
  "email": "admin@empresa.com",
  "senha": "123456"
}
```

2. **Usar Token em Requisições:**
```http
GET /api/v2/usuarios
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### **Roles e Permissões:**
- **ADMIN**: Acesso total a todos os endpoints
- **GERENTE**: Acesso a relatórios e operações + ML.NET
- **OPERADOR**: Acesso limitado às operações básicas

### **Endpoints Protegidos:**
- Todos os endpoints da **v2.0** requerem autenticação JWT
- Endpoint de **ML.NET** requer role ADMIN ou GERENTE
- Endpoints da **v1.0** são públicos (sem autenticação)

---

## 📋 **CÓDIGOS DE STATUS HTTP**

| Código | Descrição | Uso |
|--------|-----------|-----|
| 200 | OK | Operação realizada com sucesso |
| 201 | Created | Recurso criado com sucesso |
| 204 | No Content | Operação realizada sem retorno (DELETE) |
| 400 | Bad Request | Dados inválidos ou parâmetros incorretos |
| 404 | Not Found | Recurso não encontrado |
| 409 | Conflict | Conflito (ex: email/CNPJ duplicado) |
| 500 | Internal Server Error | Erro interno do servidor |

---

## 🔗 **VALORES DE ENUM**

### Perfil Usuario:
- `0` = ADMIN
- `1` = GERENTE  
- `2` = OPERADOR

### Status Moto:
- `0` = DISPONIVEL
- `1` = EM_USO
- `2` = MANUTENCAO
- `3` = INDISPONIVEL
- `4` = PENDENTE
- `5` = REPARO_SIMPLES
- `6` = DANOS_ESTRUTURAIS
- `7` = MOTOR_DEFEITUOSO
- `8` = MANUTENCAO_AGENDADA
- `9` = PRONTA
- `10` = SEM_PLACA
- `11` = ALUGADA
- `12` = AGUARDANDO_ALUGUEL

### Tipo Operação:
- `0` = ENTREGA
- `1` = COLETA
- `2` = MANUTENCAO
- `3` = TRANSFERENCIA
- `4` = CHECK_IN
- `5` = CHECK_OUT

---

## 🎯 Roteiro de Teste no Swagger

### **Passo 1**: Verificar Health Check
1. Acesse: `/health`
2. Confirme status "Healthy"

### **Passo 2**: Testar CRUD Usuários
1. **GET** `/api/usuarios` - Listar existentes
2. **POST** `/api/usuarios` - Criar novo usuário
3. **GET** `/api/usuarios/{id}` - Buscar o criado
4. **PUT** `/api/usuarios/{id}` - Atualizar dados
5. **DELETE** `/api/usuarios/{id}` - Deletar teste

### **Passo 3**: Testar CRUD Motos
1. **GET** `/api/motos` - Listar existentes
2. **POST** `/api/motos` - Criar nova moto
3. **GET** `/api/motos/{id}` - Buscar a criada
4. **PUT** `/api/motos/{id}` - Atualizar dados
5. **DELETE** `/api/motos/{id}` - Deletar teste

### **Passo 4**: Testar CRUD Operações
1. **GET** `/api/operacoes` - Listar existentes
2. **POST** `/api/operacoes` - Criar nova operação
3. **GET** `/api/operacoes/{id}` - Buscar a criada
4. **PUT** `/api/operacoes/{id}` - Atualizar dados
5. **DELETE** `/api/operacoes/{id}` - Deletar teste

### **Passo 5**: Testar CRUD Status Motos
1. **GET** `/api/statusmotos` - Listar existentes
2. **POST** `/api/statusmotos` - Criar novo status
3. **GET** `/api/statusmotos/{id}` - Buscar o criado
4. **PUT** `/api/statusmotos/{id}` - Atualizar dados
5. **DELETE** `/api/statusmotos/{id}` - Deletar teste

### **Passo 6**: Validação no Banco de Dados
Após cada operação CRUD, execute no Azure SQL:

```sql
-- Verificar usuários
SELECT * FROM Usuarios;

-- Verificar motos
SELECT * FROM Motos;

-- Verificar operações
SELECT * FROM Operacoes;

-- Verificar status das motos
SELECT * FROM StatusMotos;
```

## 📊 Valores de Enum

### Perfil Usuario:
- `0` = ADMIN
- `1` = GERENTE
- `2` = OPERADOR

### Status Moto:
- `0` = DISPONIVEL
- `1` = ALUGADA
- `2` = MANUTENCAO
- `3` = VENDIDA

### Tipo Operação:
- `0` = VENDA
- `1` = ALUGUEL
- `2` = MANUTENCAO
- `3` = DEVOLUCAO

### Status Operação:
- `0` = PENDENTE
- `1` = CONCLUIDA
- `2` = CANCELADA

## � Links Importantes

### Repositório e Execução Local:
- **GitHub Repository**: https://github.com/lecristina/challenge3-net
- **Swagger Local**: https://localhost:5001/swagger
- **Health Check Local**: https://localhost:5001/health

## 📁 Estrutura de Arquivos

### Arquivos Principais:
- `Program.cs` - Configuração da aplicação e injeção de dependência
- `appsettings.json` - Configurações da aplicação
- `challenge-3-net.csproj` - Arquivo de projeto .NET

### Scripts e Testes:
- `teste_automatico.ps1` - Script PowerShell para testes automatizados
- `script_bd.sql` - DDL completo do banco de dados

## 🧪 Testes Automatizados

### Executar Testes:
```bash
# Executar todos os testes
dotnet test

# Executar testes com cobertura
dotnet test --collect:"XPlat Code Coverage"

# Executar testes específicos
dotnet test --filter "Category=Integration"

# Executar testes unitários
dotnet test --filter "Category=Unit"

# Executar testes de integração
dotnet test --filter "Category=Integration"
```

### Tipos de Testes:
- **Testes Unitários**: Validação de lógica de negócio (JwtService, MotoAnalysisService, Controllers)
- **Testes de Integração**: Validação de endpoints completos com WebApplicationFactory
- **Testes de Performance**: Validação de tempo de resposta
- **Testes de Autenticação**: Validação de JWT e autorização
- **Testes de ML**: Validação de funcionalidades de Machine Learning

### Estrutura de Testes:
```
Tests/
├── Unit/                    # Testes unitários
│   ├── JwtServiceTests.cs
│   ├── MotoAnalysisServiceTests.cs
│   └── AuthControllerTests.cs
└── Integration/             # Testes de integração
    └── IntegrationTests.cs
```

## 📋 Checklist de Entrega

### Requisitos Básicos:
- ✅ API RESTful implementada em .NET 9
- ✅ Mínimo 3 entidades principais (Usuários, Motos, Operações, Status)
- ✅ Endpoints CRUD completos com boas práticas REST
- ✅ Paginação implementada em todos os endpoints
- ✅ HATEOAS implementado
- ✅ Status codes adequados (200, 201, 400, 404, 409, 500)
- ✅ Swagger/OpenAPI configurado com documentação completa
- ✅ Validação de dados com Data Annotations
- ✅ Arquitetura em camadas (Controllers, Services, Repositories)
- ✅ Injeção de dependência implementada

### Funcionalidades Avançadas:
- ✅ Health Checks implementados (10 pts)
- ✅ Versionamento da API (v1.0 e v2.0) (10 pts)
- ✅ Segurança JWT implementada (25 pts)
- ✅ ML.NET para análise preditiva (25 pts)
- ✅ Testes unitários com xUnit (30 pts)
- ✅ Testes de integração com WebApplicationFactory
- ✅ Documentação Swagger atualizada
- ✅ README atualizado com instruções de testes
- ✅ Entity Framework Core com Code First
- ✅ DTOs para transferência de dados
- ✅ Tratamento de erros e logging
- ✅ README.md completo com exemplos de uso

## 🏆 Resultados Alcançados

Este projeto demonstra:

1. **Domínio do .NET 9** - Uso das funcionalidades mais recentes
2. **Arquitetura Limpa** - Separação clara de responsabilidades
3. **Boas Práticas REST** - Implementação correta dos verbos HTTP
4. **Padrões de Design** - Repository, DTO, Dependency Injection
5. **Documentação Completa** - Swagger com exemplos detalhados
6. **Código Limpo** - Estrutura organizada e bem comentada

**Desenvolvido para FIAP - Advanced Business Development with .NET**  
**Turma**: 3º Sprint - 2025