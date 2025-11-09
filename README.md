# 🏍️ TrackZone API - Sistema de Gestão de Motos

## 🚀 ADVANCED BUSINESS DEVELOPMENT WITH .NET

## Integrantes
- André Rogério Vieira Pavanela Altobelli Antunes, RM: 554764
- Enrico Figueiredo Del Guerra, RM: 558604
- Leticia Cristina Dos Santos Passos, RM: 555241

### 📋 Visão Geral do Projeto

O **TrackZone API** é uma solução completa de gestão de motos desenvolvida com **.NET 9** e **ASP.NET Core**, implementando as melhores práticas de desenvolvimento empresarial e arquitetura moderna.

### 🎯 Funcionalidades Implementadas

#### ✅ **API RESTful Completa** (25 pontos)
- **Web API** com arquitetura limpa e escalável
- **Endpoints RESTful** seguindo convenções HTTP
- **Paginação** em todos os endpoints de listagem
- **Validação de dados** com Data Annotations
- **Tratamento de erros** padronizado

#### ✅ **Health Checks** (10 pontos)
- **Health Check Geral**: Status da aplicação
- **Health Check do Banco**: Conexão Oracle
- **Health Check da Memória**: Monitoramento de recursos
- **Endpoints**: `/api/v1.0/Health`, `/api/v1.0/Health/database`, `/api/v1.0/Health/memory`

#### ✅ **Versionamento da API** (10 pontos)
- **Suporte a múltiplas versões**: v1.0 e v2.0
- **Versionamento por URL**: `/api/v1.0/` e `/api/v2.0/`
- **Backward compatibility** mantida
- **Estratégia de versionamento** documentada

#### ✅ **Segurança JWT** (25 pontos)
- **Autenticação JWT** completa
- **Autorização baseada em roles**: ADMIN, GERENTE, OPERADOR
- **Token validation** e refresh
- **Claims personalizados** para controle de acesso
- **Middleware de segurança** configurado

#### ✅ **Machine Learning com ML.NET** (25 pontos)
- **Treinamento de modelo** para previsão de status
- **Predição de status** das motos
- **Análise de padrões** nos dados
- **Informações do modelo** e métricas
- **Endpoints ML**: `/api/v2/ml/*`

#### ✅ **Testes Unitários com xUnit** (30 pontos)
- **Testes unitários** para lógica principal
- **Testes de integração** com WebApplicationFactory
- **Cobertura de testes** para serviços críticos
- **Mocks** e stubs implementados

### 🏗️ Arquitetura do Projeto

```
TrackZone API/
├── 📁 Controllers/           # Controladores da API
│   ├── AuthController.cs     # Autenticação JWT
│   ├── MotosController.cs    # Gestão de motos
│   ├── OperacoesController.cs # Operações de check-in/out
│   ├── UsuariosController.cs  # Gestão de usuários
│   ├── StatusMotosController.cs # Status das motos
│   ├── MLController.cs       # Machine Learning
│   └── HealthController.cs   # Health Checks
├── 📁 Services/              # Camada de serviços
│   ├── Auth/JwtService.cs    # Serviço JWT
│   ├── ML/MotoAnalysisService.cs # ML.NET
│   └── HealthChecks/         # Health Check services
├── 📁 Repositories/          # Camada de dados
│   ├── Interfaces/           # Contratos dos repositórios
│   └── Implementations/      # Implementações EF Core
├── 📁 Models/               # Entidades e DTOs
│   ├── Entities/            # Entidades do domínio
│   └── DTOs/                # Data Transfer Objects
├── 📁 Data/                 # Contexto do banco
│   └── ApplicationDbContext.cs # EF Core Context
├── 📁 Tests/                # Testes unitários
│   └── Unit/                # Testes com xUnit
└── 📁 Migrations/           # Migrações do banco
```

### 🛠️ Tecnologias Utilizadas

- **.NET 9** - Framework principal
- **ASP.NET Core** - Web API
- **Entity Framework Core** - ORM
- **Oracle Database** - Banco de dados
- **JWT Bearer** - Autenticação
- **ML.NET** - Machine Learning
- **xUnit** - Testes unitários
- **AutoMapper** - Mapeamento de objetos
- **Swagger/OpenAPI** - Documentação da API

### 🚀 Como Executar o Projeto

#### 📋 Pré-requisitos
- **.NET 9 SDK** instalado
- **Oracle Database** configurado
- **Visual Studio 2022** ou **VS Code**

#### 🔧 Configuração
1. **Clone o repositório**:
   ```bash
   git clone <repository-url>
   cd challenge3-net
   ```

2. **Configure a conexão com o banco**:
   ```json
   // appsettings.json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=localhost:1521/XE;User Id=trackzone;Password=senha123;"
     }
   }
   ```

3. **Execute as migrações**:
```bash
dotnet ef database update
```

4. **Execute o projeto**:
```bash
dotnet run
```

#### 🌐 Acesso à API
- **URL Base**: `https://localhost:5001` ou `http://localhost:5000`
- **Swagger UI**: `https://localhost:5001/swagger`
- **Health Check**: `https://localhost:5001/api/v1.0/Health`

### 🧪 Executando os Testes

#### 📋 Pré-requisitos
- .NET 9 SDK instalado
- Projeto compilado (`dotnet build`)
- Aplicação deve estar configurada para testes

#### 🧪 Testes Unitários
Os testes unitários testam componentes individuais isoladamente usando mocks e bancos de dados em memória.

```bash
# Executar todos os testes unitários
dotnet test

# Executar testes específicos por classe
dotnet test --filter "JwtServiceTests"
dotnet test --filter "MLServiceTests"

# Executar testes unitários apenas
dotnet test --filter "FullyQualifiedName~Unit"

# Executar com cobertura de código
dotnet test --collect:"XPlat Code Coverage"
```

**Testes Unitários Disponíveis:**
- ✅ `JwtServiceTests` - Testes do serviço JWT (geração, validação, roles)
- ✅ `MLServiceTests` - Testes do serviço ML.NET (treinamento, predição, análise)

#### 🔍 Testes de Integração
Os testes de integração usam `WebApplicationFactory` para testar a aplicação completa em um ambiente de teste real.

```bash
# Executar todos os testes de integração
dotnet test --filter "Integration"

# Executar testes de integração específicos
dotnet test --filter "AuthIntegrationTests"
dotnet test --filter "MotoIntegrationTests"
dotnet test --filter "HealthCheckIntegrationTests"

# Executar testes de integração apenas
dotnet test --filter "FullyQualifiedName~Integration"
```

**Testes de Integração Disponíveis:**
- ✅ `AuthIntegrationTests` - Testes de autenticação (login, validação, user-info)
- ✅ `MotoIntegrationTests` - Testes de endpoints de motos (GET, POST, paginação)
- ✅ `HealthCheckIntegrationTests` - Testes de health checks (health, ready, live, database)

#### 📊 Relatório de Cobertura
```bash
# Gerar relatório de cobertura completo
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults

# Gerar relatório com detalhes
dotnet test --collect:"XPlat Code Coverage" --settings:coverlet.runsettings --results-directory ./TestResults
```

#### 🎯 Estrutura dos Testes
```
Tests/
├── Unit/
│   ├── JwtServiceTests.cs      # Testes unitários do JWT
│   └── MLServiceTests.cs       # Testes unitários do ML.NET
└── Integration/
    ├── AuthIntegrationTests.cs      # Testes de integração de autenticação
    ├── MotoIntegrationTests.cs     # Testes de integração de motos
    └── HealthCheckIntegrationTests.cs # Testes de health checks
```

#### ✅ Verificação de Testes
Para garantir que todos os testes estão passando:

```bash
# Executar todos os testes e verificar resultados
dotnet test --verbosity normal

# Executar com detalhes de falhas
dotnet test --verbosity detailed
```

#### 📝 Notas Importantes sobre Testes
1. **Testes de Integração** requerem que o projeto esteja configurado corretamente
2. **WebApplicationFactory** cria uma instância de teste da aplicação
3. **Testes de Integração** podem fazer requisições HTTP reais para a API
4. **Testes Unitários** usam mocks e bancos em memória para isolamento
5. Os testes de integração testam autenticação JWT real, então precisam de um usuário válido no banco

---

## 🧪 Guia Completo de Testes - TrackZone API

### 📋 Índice
1. [Pré-requisitos](#pré-requisitos-1)
2. [Iniciando a API](#iniciando-a-api-1)
3. [PASSO 1: Login e Autenticação](#passo-1-login-e-autenticação-1)
4. [PASSO 2: Testando Endpoints v1.0](#passo-2-testando-endpoints-v10-1)
5. [PASSO 3: Testando Endpoints v2.0](#passo-3-testando-endpoints-v20-1)
6. [PASSO 4: Testando Health Checks](#passo-4-testando-health-checks-1)
7. [PASSO 5: Testando Machine Learning (v2.0)](#passo-5-testando-machine-learning-v20-1)

---

### 🔧 Pré-requisitos

1. **.NET 9 SDK** instalado
2. **Postman**, **Insomnia** ou **curl** para fazer requisições
3. **API rodando** (execute `dotnet run` na pasta do projeto)
4. **URL Base**: `https://localhost:5001` ou `http://localhost:5000`

---

### 🚀 Iniciando a API

#### 1. Abra o terminal na pasta do projeto:
```powershell
cd C:\Users\crist\Desktop\TUDO\ablablabla\challenge3-net
```

#### 2. Execute a API:
```powershell
dotnet run
```

#### 3. Aguarde a mensagem:
```
Now listening on: https://localhost:5001
Now listening on: http://localhost:5000
```

#### 4. Acesse o Swagger (opcional):
```
https://localhost:5001/swagger
```

---

### 🔐 PASSO 1: Login e Autenticação

#### ⚠️ IMPORTANTE: O AuthController está na v2.0!

#### 1.1 - Fazer Login

**Endpoint:** `POST /api/v2.0/Auth/login`

**URL Completa:** `https://localhost:5001/api/v2.0/Auth/login`

**Headers:**
```
Content-Type: application/json
```

**Body (JSON):**
```json
{
  "email": "ala@example.com",
  "senha": "123456"
}
```

**Exemplo com curl (PowerShell):**
```powershell
$body = @{
    email = "ala@example.com"
    senha = "123456"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/Auth/login" `
    -Method POST `
    -ContentType "application/json" `
    -Body $body `
    -SkipCertificateCheck
```

**Resposta Esperada (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "tokenType": "Bearer",
  "expiresIn": 3600,
  "message": "Login realizado com sucesso"
}
```

**⚠️ GUARDE O TOKEN!** Você vai precisar dele para os próximos passos.

---

#### 1.2 - Validar Token

**Endpoint:** `POST /api/v2.0/Auth/validate-token`

**URL Completa:** `https://localhost:5001/api/v2.0/Auth/validate-token`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
Content-Type: application/json
```

**Exemplo com curl (PowerShell):**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/Auth/validate-token" `
    -Method POST `
    -Headers @{
        "Authorization" = "Bearer $token"
        "Content-Type" = "application/json"
    } `
    -SkipCertificateCheck
```

**Resposta Esperada (200 OK):**
```json
{
  "isValid": true,
  "usuario": {
    "id": 53,
    "nome": "Ala",
    "email": "ala@example.com",
    "perfil": "GERENTE"
  }
}
```

---

#### 1.3 - Obter Informações do Usuário

**Endpoint:** `GET /api/v2.0/Auth/user-info`

**URL Completa:** `https://localhost:5001/api/v2.0/Auth/user-info`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
```

**Exemplo com curl (PowerShell):**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/Auth/user-info" `
    -Method GET `
    -Headers @{
        "Authorization" = "Bearer $token"
    } `
    -SkipCertificateCheck
```

**Resposta Esperada (200 OK):**
```json
{
  "id": 53,
  "nome": "Ala",
  "email": "ala@example.com",
  "perfil": "GERENTE",
  "isAdmin": false,
  "isManagerOrAdmin": true,
  "isOperador": true
}
```

---

### 📦 PASSO 2: Testando Endpoints v1.0

#### ⚠️ IMPORTANTE: Todos os endpoints abaixo precisam do token JWT no header!

**Header obrigatório para todos:**
```
Authorization: Bearer SEU_TOKEN_AQUI
```

---

#### 2.1 - Health Checks (v1.0)

##### 2.1.1 - Health Check Geral
**Endpoint:** `GET /api/v1.0/Health`

**URL Completa:** `https://localhost:5001/api/v1.0/Health`

**Exemplo:**
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/v1.0/Health" `
    -Method GET `
    -SkipCertificateCheck
```

**Resposta Esperada:**
```json
{
  "status": "Healthy",
  "timestamp": "2024-01-15T16:45:00Z",
  "uptime": "02:30:15",
  "version": "1.0.0"
}
```

##### 2.1.2 - Health Check do Banco
**Endpoint:** `GET /api/v1.0/Health/database`

**URL Completa:** `https://localhost:5001/api/v1.0/Health/database`

**Exemplo:**
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/v1.0/Health/database" `
    -Method GET `
    -SkipCertificateCheck
```

##### 2.1.3 - Health Check da Memória
**Endpoint:** `GET /api/v1.0/Health/memory`

**URL Completa:** `https://localhost:5001/api/v1.0/Health/memory`

**Exemplo:**
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/v1.0/Health/memory" `
    -Method GET `
    -SkipCertificateCheck
```

---

#### 2.2 - Motos (v1.0)

##### 2.2.1 - Listar Motos
**Endpoint:** `GET /api/v1.0/Motos?pageNumber=1&pageSize=10`

**URL Completa:** `https://localhost:5001/api/v1.0/Motos?pageNumber=1&pageSize=10`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
```

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v1.0/Motos?pageNumber=1&pageSize=10" `
    -Method GET `
    -Headers @{
        "Authorization" = "Bearer $token"
    } `
    -SkipCertificateCheck
```

**Resposta Esperada:**
```json
{
  "data": [
    {
      "id": 1,
      "placa": "ABC-1234",
      "chassi": "CHASSI123456789",
      "motor": "Motor 1.0",
      "usuarioId": 53,
      "dataCriacao": "2024-01-15T10:30:00Z"
    }
  ],
  "totalCount": 1,
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 1,
  "hasPreviousPage": false,
  "hasNextPage": false
}
```

##### 2.2.2 - Buscar Moto por ID
**Endpoint:** `GET /api/v1.0/Motos/{id}`

**URL Completa:** `https://localhost:5001/api/v1.0/Motos/1`

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v1.0/Motos/1" `
    -Method GET `
    -Headers @{
        "Authorization" = "Bearer $token"
    } `
    -SkipCertificateCheck
```

##### 2.2.3 - Criar Moto
**Endpoint:** `POST /api/v1.0/Motos`

**URL Completa:** `https://localhost:5001/api/v1.0/Motos`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
Content-Type: application/json
```

**Body (JSON):**
```json
{
  "placa": "DEF-5678",
  "chassi": "CHASSI567890123",
  "motor": "Motor 3.0",
  "usuarioId": 53
}
```

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"
$body = @{
    placa = "DEF-5678"
    chassi = "CHASSI567890123"
    motor = "Motor 3.0"
    usuarioId = 53
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/v1.0/Motos" `
    -Method POST `
    -Headers @{
        "Authorization" = "Bearer $token"
        "Content-Type" = "application/json"
    } `
    -Body $body `
    -SkipCertificateCheck
```

##### 2.2.4 - Atualizar Moto
**Endpoint:** `PUT /api/v1.0/Motos/{id}`

**URL Completa:** `https://localhost:5001/api/v1.0/Motos/1`

**Body (JSON):**
```json
{
  "placa": "XYZ-9999",
  "chassi": "CHASSI999999999",
  "motor": "Motor Atualizado",
  "usuarioId": 53
}
```

##### 2.2.5 - Deletar Moto
**Endpoint:** `DELETE /api/v1.0/Motos/{id}`

**URL Completa:** `https://localhost:5001/api/v1.0/Motos/1`

---

#### 2.3 - Usuários (v1.0)

##### 2.3.1 - Listar Usuários
**Endpoint:** `GET /api/v1.0/Usuarios?pageNumber=1&pageSize=10`

**URL Completa:** `https://localhost:5001/api/v1.0/Usuarios?pageNumber=1&pageSize=10`

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v1.0/Usuarios?pageNumber=1&pageSize=10" `
    -Method GET `
    -Headers @{
        "Authorization" = "Bearer $token"
    } `
    -SkipCertificateCheck
```

##### 2.3.2 - Buscar Usuário por ID
**Endpoint:** `GET /api/v1.0/Usuarios/{id}`

**URL Completa:** `https://localhost:5001/api/v1.0/Usuarios/53`

##### 2.3.3 - Criar Usuário
**Endpoint:** `POST /api/v1.0/Usuarios`

**Body (JSON):**
```json
{
  "nome": "João Silva",
  "email": "joao@empresa.com",
  "senha": "123456",
  "perfil": "OPERADOR",
  "cnpj": "12.345.678/0001-90",
  "telefone": "(11) 88888-8888",
  "endereco": "Rua Nova, 456",
  "nomeFilial": "Filial Norte"
}
```

##### 2.3.4 - Atualizar Usuário
**Endpoint:** `PUT /api/v1.0/Usuarios/{id}`

##### 2.3.5 - Deletar Usuário
**Endpoint:** `DELETE /api/v1.0/Usuarios/{id}`

---

#### 2.4 - Operações (v1.0)

##### 2.4.1 - Listar Operações
**Endpoint:** `GET /api/v1.0/Operacoes?pageNumber=1&pageSize=10`

**URL Completa:** `https://localhost:5001/api/v1.0/Operacoes?pageNumber=1&pageSize=10`

##### 2.4.2 - Buscar Operação por ID
**Endpoint:** `GET /api/v1.0/Operacoes/{id}`

##### 2.4.3 - Criar Operação
**Endpoint:** `POST /api/v1.0/Operacoes`

**Body (JSON):**
```json
{
  "tipoOperacao": 0,
  "descricao": "Check-in da moto para entrega",
  "motoId": 1,
  "usuarioId": 53
}
```

**⚠️ Valores para tipoOperacao:**
- `0` = CHECK_IN
- `1` = CHECK_OUT

##### 2.4.4 - Atualizar Operação
**Endpoint:** `PUT /api/v1.0/Operacoes/{id}`

##### 2.4.5 - Deletar Operação
**Endpoint:** `DELETE /api/v1.0/Operacoes/{id}`

---

#### 2.5 - Status das Motos (v1.0)

##### 2.5.1 - Listar Status
**Endpoint:** `GET /api/v1.0/StatusMotos?pageNumber=1&pageSize=10`

**URL Completa:** `https://localhost:5001/api/v1.0/StatusMotos?pageNumber=1&pageSize=10`

##### 2.5.2 - Buscar Status por ID
**Endpoint:** `GET /api/v1.0/StatusMotos/{id}`

##### 2.5.3 - Status Atual da Moto
**Endpoint:** `GET /api/v1.0/StatusMotos/moto/{motoId}/atual`

**URL Completa:** `https://localhost:5001/api/v1.0/StatusMotos/moto/1/atual`

##### 2.5.4 - Histórico de Status da Moto
**Endpoint:** `GET /api/v1.0/StatusMotos/moto/{motoId}/historico`

**URL Completa:** `https://localhost:5001/api/v1.0/StatusMotos/moto/1/historico`

##### 2.5.5 - Status por Tipo
**Endpoint:** `GET /api/v1.0/StatusMotos/tipo/{tipo}`

**URL Completa:** `https://localhost:5001/api/v1.0/StatusMotos/tipo/DISPONIVEL`

**Valores possíveis para tipo:**
- `DISPONIVEL`
- `EM_USO`
- `EM_MANUTENCAO`
- `INDISPONIVEL`

##### 2.5.6 - Criar Status
**Endpoint:** `POST /api/v1.0/StatusMotos`

**Body (JSON):**
```json
{
  "status": "EM_MANUTENCAO",
  "descricao": "Moto em manutenção preventiva",
  "motoId": 1
}
```

##### 2.5.7 - Atualizar Status
**Endpoint:** `PUT /api/v1.0/StatusMotos/{id}`

##### 2.5.8 - Deletar Status
**Endpoint:** `DELETE /api/v1.0/StatusMotos/{id}`

---

### 🚀 PASSO 3: Testando Endpoints v2.0

#### ⚠️ IMPORTANTE: Todos os endpoints abaixo precisam do token JWT no header!

**Header obrigatório para todos:**
```
Authorization: Bearer SEU_TOKEN_AQUI
```

---

#### 3.1 - Health Checks (v2.0)

Os mesmos endpoints da v1.0, mas usando `/api/v2.0/Health`:

- `GET /api/v2.0/Health`
- `GET /api/v2.0/Health/database`
- `GET /api/v2.0/Health/memory`

---

#### 3.2 - Motos (v2.0)

Todos os endpoints de motos da v1.0, mas usando `/api/v2.0/Motos`:

- `GET /api/v2.0/Motos?pageNumber=1&pageSize=10`
- `GET /api/v2.0/Motos/{id}`
- `POST /api/v2.0/Motos`
- `PUT /api/v2.0/Motos/{id}`
- `DELETE /api/v2.0/Motos/{id}`

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/Motos?pageNumber=1&pageSize=10" `
    -Method GET `
    -Headers @{
        "Authorization" = "Bearer $token"
    } `
    -SkipCertificateCheck
```

---

#### 3.3 - Usuários (v2.0)

Todos os endpoints de usuários da v1.0, mas usando `/api/v2.0/Usuarios`:

- `GET /api/v2.0/Usuarios?pageNumber=1&pageSize=10`
- `GET /api/v2.0/Usuarios/{id}`
- `POST /api/v2.0/Usuarios`
- `PUT /api/v2.0/Usuarios/{id}`
- `DELETE /api/v2.0/Usuarios/{id}`

---

#### 3.4 - Operações (v2.0)

Todos os endpoints de operações da v1.0, mas usando `/api/v2.0/Operacoes`:

- `GET /api/v2.0/Operacoes?pageNumber=1&pageSize=10`
- `GET /api/v2.0/Operacoes/{id}`
- `POST /api/v2.0/Operacoes`
- `PUT /api/v2.0/Operacoes/{id}`
- `DELETE /api/v2.0/Operacoes/{id}`

---

#### 3.5 - Status das Motos (v2.0)

Todos os endpoints de status da v1.0, mas usando `/api/v2.0/StatusMotos`:

- `GET /api/v2.0/StatusMotos?pageNumber=1&pageSize=10`
- `GET /api/v2.0/StatusMotos/{id}`
- `GET /api/v2.0/StatusMotos/moto/{motoId}/atual`
- `GET /api/v2.0/StatusMotos/moto/{motoId}/historico`
- `GET /api/v2.0/StatusMotos/tipo/{tipo}`
- `POST /api/v2.0/StatusMotos`
- `PUT /api/v2.0/StatusMotos/{id}`
- `DELETE /api/v2.0/StatusMotos/{id}`

---

### 🤖 PASSO 4: Testando Machine Learning (v2.0)

#### ⚠️ IMPORTANTE: Todos os endpoints ML precisam do token JWT no header!

**Header obrigatório para todos:**
```
Authorization: Bearer SEU_TOKEN_AQUI
```

---

#### 4.1 - Treinar Modelo

**Endpoint:** `POST /api/v2.0/ml/train-model`

**URL Completa:** `https://localhost:5001/api/v2.0/ml/train-model`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
Content-Type: application/json
```

**Body (JSON):**
```json
{
  "trainingData": [
    {
      "motoId": 1,
      "status": "DISPONIVEL",
      "dataCriacao": "2024-01-15T00:00:00Z"
    },
    {
      "motoId": 2,
      "status": "EM_USO",
      "dataCriacao": "2024-01-15T01:00:00Z"
    }
  ]
}
```

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"
$body = @{
    trainingData = @(
        @{
            motoId = 1
            status = "DISPONIVEL"
            dataCriacao = "2024-01-15T00:00:00Z"
        },
        @{
            motoId = 2
            status = "EM_USO"
            dataCriacao = "2024-01-15T01:00:00Z"
        }
    )
} | ConvertTo-Json -Depth 10

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/ml/train-model" `
    -Method POST `
    -Headers @{
        "Authorization" = "Bearer $token"
        "Content-Type" = "application/json"
    } `
    -Body $body `
    -SkipCertificateCheck
```

**Resposta Esperada:**
```json
{
  "success": true,
  "message": "Modelo treinado com sucesso",
  "accuracy": 0.95,
  "trainingTime": "00:00:30"
}
```

---

#### 4.2 - Prever Status

**Endpoint:** `POST /api/v2.0/ml/predict-status`

**URL Completa:** `https://localhost:5001/api/v2.0/ml/predict-status`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
Content-Type: application/json
```

**Body (JSON):**
```json
{
  "motoId": 1,
  "features": {
    "tempoUso": 120,
    "manutencoes": 2,
    "operacoes": 15
  }
}
```

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"
$body = @{
    motoId = 1
    features = @{
        tempoUso = 120
        manutencoes = 2
        operacoes = 15
    }
} | ConvertTo-Json -Depth 10

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/ml/predict-status" `
    -Method POST `
    -Headers @{
        "Authorization" = "Bearer $token"
        "Content-Type" = "application/json"
    } `
    -Body $body `
    -SkipCertificateCheck
```

**Resposta Esperada:**
```json
{
  "predictedStatus": "DISPONIVEL",
  "confidence": 0.95,
  "features": {
    "tempoUso": 120,
    "manutencoes": 2,
    "operacoes": 15
  }
}
```

---

#### 4.3 - Analisar Padrões

**Endpoint:** `GET /api/v2.0/ml/analyze-patterns`

**URL Completa:** `https://localhost:5001/api/v2.0/ml/analyze-patterns`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
```

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/ml/analyze-patterns" `
    -Method GET `
    -Headers @{
        "Authorization" = "Bearer $token"
    } `
    -SkipCertificateCheck
```

**Resposta Esperada:**
```json
{
  "patterns": [
    {
      "pattern": "string",
      "frequency": 0,
      "description": "string"
    }
  ],
  "insights": [
    {
      "insight": "string",
      "confidence": 0.95
    }
  ]
}
```

---

#### 4.4 - Informações do Modelo

**Endpoint:** `GET /api/v2.0/ml/model-info`

**URL Completa:** `https://localhost:5001/api/v2.0/ml/model-info`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
```

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/ml/model-info" `
    -Method GET `
    -Headers @{
        "Authorization" = "Bearer $token"
    } `
    -SkipCertificateCheck
```

**Resposta Esperada:**
```json
{
  "modelName": "string",
  "version": "string",
  "lastTraining": "2024-01-01T00:00:00Z",
  "accuracy": 0.95,
  "features": ["string"]
}
```

---

### 🏥 PASSO 5: Testando Health Checks

#### 5.1 - Health Check Geral (sem autenticação)

**Endpoint:** `GET /health`

**URL Completa:** `https://localhost:5001/health`

**Exemplo:**
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/health" `
    -Method GET `
    -SkipCertificateCheck
```

---

#### 5.2 - Health Check do Banco (sem autenticação)

**Endpoint:** `GET /health/database`

**URL Completa:** `https://localhost:5001/health/database`

**Exemplo:**
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/health/database" `
    -Method GET `
    -SkipCertificateCheck
```

---

#### 5.3 - Health Check Ready (sem autenticação)

**Endpoint:** `GET /health/ready`

**URL Completa:** `https://localhost:5001/health/ready`

**Exemplo:**
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/health/ready" `
    -Method GET `
    -SkipCertificateCheck
```

---

#### 5.4 - Health Check Live (sem autenticação)

**Endpoint:** `GET /health/live`

**URL Completa:** `https://localhost:5001/health/live`

**Exemplo:**
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/health/live" `
    -Method GET `
    -SkipCertificateCheck
```

---

### 📝 Resumo dos Endpoints

#### 🔐 Autenticação (v2.0 apenas)
- `POST /api/v2.0/Auth/login` - Fazer login
- `POST /api/v2.0/Auth/validate-token` - Validar token
- `GET /api/v2.0/Auth/user-info` - Informações do usuário

#### 🏥 Health Checks
- `GET /api/v1.0/Health` - Health check geral (v1.0)
- `GET /api/v1.0/Health/database` - Health check do banco (v1.0)
- `GET /api/v1.0/Health/memory` - Health check da memória (v1.0)
- `GET /api/v2.0/Health` - Health check geral (v2.0)
- `GET /api/v2.0/Health/database` - Health check do banco (v2.0)
- `GET /api/v2.0/Health/memory` - Health check da memória (v2.0)
- `GET /health` - Health check geral (sem versão)
- `GET /health/database` - Health check do banco (sem versão)
- `GET /health/ready` - Health check ready
- `GET /health/live` - Health check live

#### 🏍️ Motos (v1.0 e v2.0)
- `GET /api/v{version}/Motos` - Listar motos
- `GET /api/v{version}/Motos/{id}` - Buscar moto por ID
- `POST /api/v{version}/Motos` - Criar moto
- `PUT /api/v{version}/Motos/{id}` - Atualizar moto
- `DELETE /api/v{version}/Motos/{id}` - Deletar moto

#### 👥 Usuários (v1.0 e v2.0)
- `GET /api/v{version}/Usuarios` - Listar usuários
- `GET /api/v{version}/Usuarios/{id}` - Buscar usuário por ID
- `POST /api/v{version}/Usuarios` - Criar usuário
- `PUT /api/v{version}/Usuarios/{id}` - Atualizar usuário
- `DELETE /api/v{version}/Usuarios/{id}` - Deletar usuário

#### 📊 Operações (v1.0 e v2.0)
- `GET /api/v{version}/Operacoes` - Listar operações
- `GET /api/v{version}/Operacoes/{id}` - Buscar operação por ID
- `POST /api/v{version}/Operacoes` - Criar operação
- `PUT /api/v{version}/Operacoes/{id}` - Atualizar operação
- `DELETE /api/v{version}/Operacoes/{id}` - Deletar operação

#### 📈 Status das Motos (v1.0 e v2.0)
- `GET /api/v{version}/StatusMotos` - Listar status
- `GET /api/v{version}/StatusMotos/{id}` - Buscar status por ID
- `GET /api/v{version}/StatusMotos/moto/{motoId}/atual` - Status atual da moto
- `GET /api/v{version}/StatusMotos/moto/{motoId}/historico` - Histórico de status
- `GET /api/v{version}/StatusMotos/tipo/{tipo}` - Status por tipo
- `POST /api/v{version}/StatusMotos` - Criar status
- `PUT /api/v{version}/StatusMotos/{id}` - Atualizar status
- `DELETE /api/v{version}/StatusMotos/{id}` - Deletar status

#### 🤖 Machine Learning (v2.0 apenas)
- `POST /api/v2.0/ml/train-model` - Treinar modelo
- `POST /api/v2.0/ml/predict-status` - Prever status
- `GET /api/v2.0/ml/analyze-patterns` - Analisar padrões
- `GET /api/v2.0/ml/model-info` - Informações do modelo

---

### ⚠️ Dicas Importantes

1. **Token JWT**: O token expira em 1 hora (3600 segundos). Se receber erro 401, faça login novamente.

2. **HTTPS**: Se estiver usando `https://localhost:5001`, pode precisar usar `-SkipCertificateCheck` no PowerShell ou aceitar o certificado auto-assinado.

3. **Swagger**: Acesse `https://localhost:5001/swagger` para testar os endpoints diretamente no navegador.

4. **Credenciais de Teste**:
   - Email: `ala@example.com`
   - Senha: `123456`
   - Perfil: `GERENTE`

5. **Códigos de Status HTTP**:
   - `200` - Sucesso
   - `201` - Criado com sucesso
   - `204` - Sem conteúdo (deletado)
   - `400` - Requisição inválida
   - `401` - Não autorizado (token inválido ou ausente)
   - `403` - Proibido (sem permissão)
   - `404` - Não encontrado
   - `500` - Erro interno do servidor

---

### 🎯 Ordem Recomendada de Testes

1. ✅ **Iniciar a API** (`dotnet run`)
2. ✅ **Fazer Login** (`POST /api/v2.0/Auth/login`)
3. ✅ **Guardar o Token**
4. ✅ **Testar Health Checks** (sem token)
5. ✅ **Testar Endpoints v1.0** (com token)
6. ✅ **Testar Endpoints v2.0** (com token)
7. ✅ **Testar Machine Learning** (com token)

---

### 📞 Suporte

Se encontrar algum problema:

1. Verifique se a API está rodando
2. Verifique se o token está válido
3. Verifique os logs da aplicação
4. Verifique a conexão com o banco de dados
5. Acesse o Swagger para testar diretamente: `https://localhost:5001/swagger`

---

**Boa sorte nos testes! 🚀**

### 🔐 Autenticação e Segurança

#### 🎫 JWT Token
```bash
# Login para obter token
curl -X POST "https://localhost:5001/api/v1.0/Auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "ala@example.com",
    "senha": "123456"
  }'
```

#### 🔑 Uso do Token
```bash
# Usar token nas requisições
curl -X GET "https://localhost:5001/api/v1.0/Motos" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
```

### 🤖 Machine Learning

#### 🧠 Treinamento do Modelo
```bash
curl -X POST "https://localhost:5001/api/v2/ml/train-model" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "trainingData": [
      {
        "motoId": 1,
        "status": "DISPONIVEL",
        "dataCriacao": "2024-01-15T00:00:00Z"
      }
    ]
  }'
```

#### 🔮 Predição de Status
```bash
curl -X POST "https://localhost:5001/api/v2/ml/predict-status" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "motoId": 1,
    "features": {
      "tempoUso": 120,
      "manutencoes": 2,
      "operacoes": 15
    }
  }'
```

### 📊 Health Checks

#### 🏥 Status da Aplicação
```bash
# Health check geral
curl -X GET "https://localhost:5001/api/v1.0/Health"

# Health check do banco
curl -X GET "https://localhost:5001/api/v1.0/Health/database"

# Health check da memória
curl -X GET "https://localhost:5001/api/v1.0/Health/memory"
```

### 📈 Versionamento da API

#### 🔄 Versões Disponíveis
- **v1.0**: Versão estável com funcionalidades básicas
- **v2.0**: Versão com funcionalidades avançadas e ML

#### 📝 Exemplo de Uso
```bash
# API v1.0
curl -X GET "https://localhost:5001/api/v1.0/Motos"

# API v2.0
curl -X GET "https://localhost:5001/api/v2.0/Motos"
```

### 🎯 Perfis de Usuário

| Perfil | Permissões | Endpoints |
|--------|------------|-----------|
| **ADMIN** | Acesso total | Todos os endpoints |
| **GERENTE** | Operações + Relatórios | Motos, Operações, Status, ML |
| **OPERADOR** | Operações básicas | Operações limitadas |

### 📋 Documentação Completa dos Endpoints

### 🔐 Autenticação

#### Login
- **Endpoint**: `POST /api/v1.0/Auth/login`
- **Descrição**: Realiza login do usuário e retorna token JWT
- **Body**:
```json
{
  "email": "ala@example.com",
  "senha": "123456"
}
```
- **Resposta**:
```json
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjUzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImFsYSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFsYUBleGFtcGxlLmNvbSIsInBlcmZpbCI6IkdFUkVOVEUiLCJjbnBqIjoiOTguNzY1LjAwMC8wMDAxLTEwIiwianRpIjoiM2FkNmE3MDgtNjViZi00N2U0LWJiYWUtYTM4Zjk2Mzk3MjUzIiwiaWF0IjoxNzYxNjU5NjQ0LCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiR0VSRU5URSIsIk9QRVJBRE9SIl0sImV4cCI6MTc2MTY2MzI0NCwiaXNzIjoiVHJhY2tab25lQVBJIiwiYXVkIjoiVHJhY2tab25lVXNlcnMifQ.Sqsv8feSsJCZKZL4AXj_zjdnCdiKrMcvZSZaou72fMQ",
    "usuario": {
      "id": 53,
      "nome": "Ala",
      "email": "ala@example.com",
      "perfil": "GERENTE"
    }
  }
  ```

#### Validar Token
- **Endpoint**: `POST /api/v1.0/Auth/validate-token`
- **Descrição**: Valida se o token JWT é válido
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Resposta**:
  ```json
  {
    "isValid": true,
    "usuario": {
      "id": 53,
      "nome": "Ala",
      "email": "ala@example.com",
      "perfil": "GERENTE"
    }
  }
  ```

#### Informações do Usuário
- **Endpoint**: `GET /api/v1.0/Auth/user-info`
- **Descrição**: Retorna informações do usuário logado
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Resposta**:
```json
{
  "id": 53,
    "nome": "Ala",
  "email": "ala@example.com",
  "perfil": "GERENTE",
    "isAdmin": false,
    "isManagerOrAdmin": true,
    "isOperador": true
  }
  ```

### 🏍️ Motos

#### Listar Motos
- **Endpoint**: `GET /api/v1.0/Motos?pageNumber=1&pageSize=10`
- **Descrição**: Lista todas as motos com paginação
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Query Parameters**:
  - `pageNumber` (int): Número da página (padrão: 1)
  - `pageSize` (int): Tamanho da página (padrão: 10)
- **Resposta**:
```json
{
    "data": [
    {
      "id": 1,
        "placa": "ABC-1234",
        "chassi": "CHASSI123456789",
        "motor": "Motor 1.0",
        "usuarioId": 53,
        "dataCriacao": "2024-01-15T10:30:00Z"
      },
      {
        "id": 2,
        "placa": "XYZ-9876",
        "chassi": "CHASSI987654321",
        "motor": "Motor 2.0",
        "usuarioId": 53,
        "dataCriacao": "2024-01-15T11:45:00Z"
      }
    ],
    "totalCount": 2,
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 1,
  "hasPreviousPage": false,
    "hasNextPage": false
  }
  ```

#### Buscar Moto por ID
- **Endpoint**: `GET /api/v1.0/Motos/1`
- **Descrição**: Busca uma moto específica por ID
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Resposta**:
```json
{
  "id": 1,
    "placa": "ABC-1234",
    "chassi": "CHASSI123456789",
    "motor": "Motor 1.0",
    "usuarioId": 53,
    "dataCriacao": "2024-01-15T10:30:00Z"
  }
  ```

#### Criar Moto
- **Endpoint**: `POST /api/v1.0/Motos`
- **Descrição**: Cria uma nova moto
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Body**:
  ```json
  {
    "placa": "DEF-5678",
    "chassi": "CHASSI567890123",
    "motor": "Motor 3.0",
    "usuarioId": 53
  }
  ```
- **Resposta**:
  ```json
  {
    "id": 3,
    "placa": "DEF-5678",
    "chassi": "CHASSI567890123",
    "motor": "Motor 3.0",
    "usuarioId": 53,
    "dataCriacao": "2024-01-15T14:20:00Z"
  }
  ```

#### Atualizar Moto
- **Endpoint**: `PUT /api/v1.0/Motos/{id}`
- **Descrição**: Atualiza uma moto existente
- **Headers**: `Authorization: Bearer {token}`
- **Body**:
```json
{
    "placa": "string",
    "chassi": "string",
    "motor": "string",
    "usuarioId": 0
  }
  ```
- **Resposta**:
```json
{
    "id": 0,
    "placa": "string",
    "chassi": "string",
    "motor": "string",
    "usuarioId": 0,
    "dataCriacao": "2024-01-01T00:00:00Z"
  }
  ```

#### Deletar Moto
- **Endpoint**: `DELETE /api/v1.0/Motos/{id}`
- **Descrição**: Deleta uma moto
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**: `204 No Content`

### 📊 Operações

#### Listar Operações
- **Endpoint**: `GET /api/v1.0/Operacoes`
- **Descrição**: Lista todas as operações com paginação
- **Headers**: `Authorization: Bearer {token}`
- **Query Parameters**:
  - `pageNumber` (int): Número da página (padrão: 1)
  - `pageSize` (int): Tamanho da página (padrão: 10)
- **Resposta**:
  ```json
  {
    "data": [
      {
        "id": 0,
        "tipoOperacao": "CHECK_IN",
        "descricao": "string",
        "motoId": 0,
        "usuarioId": 0,
        "dataCriacao": "2024-01-01T00:00:00Z"
      }
    ],
    "totalCount": 0,
    "pageNumber": 0,
    "pageSize": 0,
    "totalPages": 0,
    "hasPreviousPage": true,
    "hasNextPage": true
  }
  ```

#### Buscar Operação por ID
- **Endpoint**: `GET /api/v1.0/Operacoes/{id}`
- **Descrição**: Busca uma operação específica por ID
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**:
```json
{
    "id": 0,
    "tipoOperacao": "CHECK_IN",
    "descricao": "string",
    "motoId": 0,
    "usuarioId": 0,
    "dataCriacao": "2024-01-01T00:00:00Z"
  }
  ```

#### Criar Operação
- **Endpoint**: `POST /api/v1.0/Operacoes`
- **Descrição**: Cria uma nova operação
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Body**:
```json
{
    "tipoOperacao": 0,
    "descricao": "Check-in da moto para entrega",
    "motoId": 1,
    "usuarioId": 53
  }
  ```
- **Resposta**:
  ```json
  {
    "id": 1,
    "tipoOperacao": "CHECK_IN",
    "descricao": "Check-in da moto para entrega",
    "motoId": 1,
    "usuarioId": 53,
    "dataCriacao": "2024-01-15T15:30:00Z"
  }
  ```

#### Atualizar Operação
- **Endpoint**: `PUT /api/v1.0/Operacoes/{id}`
- **Descrição**: Atualiza uma operação existente
- **Headers**: `Authorization: Bearer {token}`
- **Body**:
  ```json
  {
    "tipoOperacao": 0,
    "descricao": "string",
    "motoId": 0,
    "usuarioId": 0
  }
  ```
- **Resposta**:
  ```json
  {
    "id": 0,
    "tipoOperacao": "CHECK_IN",
    "descricao": "string",
    "motoId": 0,
    "usuarioId": 0,
    "dataCriacao": "2024-01-01T00:00:00Z"
  }
  ```

#### Deletar Operação
- **Endpoint**: `DELETE /api/v1.0/Operacoes/{id}`
- **Descrição**: Deleta uma operação
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**: `204 No Content`

### 👥 Usuários

#### Listar Usuários
- **Endpoint**: `GET /api/v1.0/Usuarios?pageNumber=1&pageSize=10`
- **Descrição**: Lista todos os usuários com paginação
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Query Parameters**:
  - `pageNumber` (int): Número da página (padrão: 1)
  - `pageSize` (int): Tamanho da página (padrão: 10)
- **Resposta**:
  ```json
  {
    "data": [
      {
        "id": 53,
        "nome": "Ala",
        "email": "ala@example.com",
        "perfil": "GERENTE",
        "cnpj": "98.765.000/0001-10",
        "telefone": "(11) 99999-9999",
        "endereco": "Rua das Flores, 123",
        "nomeFilial": "Filial Central",
        "dataCriacao": "2024-01-10T08:00:00Z"
      }
    ],
    "totalCount": 1,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 1,
    "hasPreviousPage": false,
    "hasNextPage": false
  }
  ```

#### Buscar Usuário por ID
- **Endpoint**: `GET /api/v1.0/Usuarios/53`
- **Descrição**: Busca um usuário específico por ID
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Resposta**:
  ```json
  {
    "id": 53,
    "nome": "Ala",
    "email": "ala@example.com",
    "perfil": "GERENTE",
    "cnpj": "98.765.000/0001-10",
    "telefone": "(11) 99999-9999",
    "endereco": "Rua das Flores, 123",
    "nomeFilial": "Filial Central",
    "dataCriacao": "2024-01-10T08:00:00Z"
  }
  ```

#### Criar Usuário
- **Endpoint**: `POST /api/v1.0/Usuarios`
- **Descrição**: Cria um novo usuário
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Body**:
  ```json
  {
    "nome": "João Silva",
    "email": "joao@empresa.com",
    "senha": "123456",
    "perfil": "OPERADOR",
    "cnpj": "12.345.678/0001-90",
    "telefone": "(11) 88888-8888",
    "endereco": "Rua Nova, 456",
    "nomeFilial": "Filial Norte"
  }
  ```
- **Resposta**:
```json
{
    "id": 54,
    "nome": "João Silva",
    "email": "joao@empresa.com",
    "perfil": "OPERADOR",
    "cnpj": "12.345.678/0001-90",
    "telefone": "(11) 88888-8888",
    "endereco": "Rua Nova, 456",
    "nomeFilial": "Filial Norte",
    "dataCriacao": "2024-01-15T16:00:00Z"
  }
  ```

#### Atualizar Usuário
- **Endpoint**: `PUT /api/v1.0/Usuarios/{id}`
- **Descrição**: Atualiza um usuário existente
- **Headers**: `Authorization: Bearer {token}`
- **Body**:
  ```json
  {
    "nome": "string",
    "email": "string",
    "senha": "string",
    "perfil": "ADMIN",
    "cnpj": "string",
    "telefone": "string",
    "endereco": "string",
    "nomeFilial": "string"
  }
  ```
- **Resposta**:
  ```json
  {
    "id": 0,
    "nome": "string",
    "email": "string",
    "perfil": "ADMIN",
    "cnpj": "string",
    "telefone": "string",
    "endereco": "string",
    "nomeFilial": "string",
    "dataCriacao": "2024-01-01T00:00:00Z"
  }
  ```

#### Deletar Usuário
- **Endpoint**: `DELETE /api/v1.0/Usuarios/{id}`
- **Descrição**: Deleta um usuário
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**: `204 No Content`

### 📈 Status das Motos

#### Listar Status das Motos
- **Endpoint**: `GET /api/v1.0/StatusMotos?pageNumber=1&pageSize=10`
- **Descrição**: Lista todos os status das motos com paginação
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Query Parameters**:
  - `pageNumber` (int): Número da página (padrão: 1)
  - `pageSize` (int): Tamanho da página (padrão: 10)
- **Resposta**:
```json
{
    "data": [
      {
        "id": 1,
        "status": "DISPONIVEL",
        "descricao": "Moto disponível para uso",
  "motoId": 1,
        "dataCriacao": "2024-01-15T09:00:00Z"
      },
      {
        "id": 2,
        "status": "EM_USO",
        "descricao": "Moto em uso para entrega",
        "motoId": 2,
        "dataCriacao": "2024-01-15T10:15:00Z"
      }
    ],
    "totalCount": 2,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 1,
    "hasPreviousPage": false,
    "hasNextPage": false
  }
  ```

#### Buscar Status por ID
- **Endpoint**: `GET /api/v1.0/StatusMotos/{id}`
- **Descrição**: Busca um status específico por ID
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**:
  ```json
  {
    "id": 0,
    "status": "DISPONIVEL",
    "descricao": "string",
    "motoId": 0,
    "dataCriacao": "2024-01-01T00:00:00Z"
  }
  ```

#### Status Atual da Moto
- **Endpoint**: `GET /api/v1.0/StatusMotos/moto/1/atual`
- **Descrição**: Busca o status atual de uma moto específica
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Resposta**:
  ```json
  {
    "id": 1,
    "status": "DISPONIVEL",
    "descricao": "Moto disponível para uso",
    "motoId": 1,
    "dataCriacao": "2024-01-15T09:00:00Z"
  }
  ```

#### Histórico de Status da Moto
- **Endpoint**: `GET /api/v1.0/StatusMotos/moto/1/historico`
- **Descrição**: Busca o histórico de status de uma moto específica
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Resposta**:
  ```json
  [
    {
      "id": 1,
      "status": "DISPONIVEL",
      "descricao": "Moto disponível para uso",
      "motoId": 1,
      "dataCriacao": "2024-01-15T09:00:00Z"
    },
    {
      "id": 3,
      "status": "EM_USO",
      "descricao": "Moto em uso para entrega",
      "motoId": 1,
      "dataCriacao": "2024-01-15T11:30:00Z"
    },
    {
      "id": 4,
      "status": "DISPONIVEL",
      "descricao": "Moto retornou e está disponível",
      "motoId": 1,
      "dataCriacao": "2024-01-15T14:45:00Z"
    }
  ]
  ```

#### Status por Tipo
- **Endpoint**: `GET /api/v1.0/StatusMotos/tipo/DISPONIVEL`
- **Descrição**: Busca status por tipo específico
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Resposta**:
```json
  [
{
      "id": 1,
      "status": "DISPONIVEL",
      "descricao": "Moto disponível para uso",
  "motoId": 1,
      "dataCriacao": "2024-01-15T09:00:00Z"
    },
    {
      "id": 4,
      "status": "DISPONIVEL",
      "descricao": "Moto retornou e está disponível",
      "motoId": 1,
      "dataCriacao": "2024-01-15T14:45:00Z"
    }
  ]
  ```

#### Criar Status
- **Endpoint**: `POST /api/v1.0/StatusMotos`
- **Descrição**: Cria um novo status
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Body**:
  ```json
  {
    "status": "EM_MANUTENCAO",
    "descricao": "Moto em manutenção preventiva",
    "motoId": 1
  }
  ```
- **Resposta**:
```json
{
    "id": 5,
    "status": "EM_MANUTENCAO",
    "descricao": "Moto em manutenção preventiva",
  "motoId": 1,
    "dataCriacao": "2024-01-15T16:30:00Z"
  }
  ```

#### Atualizar Status
- **Endpoint**: `PUT /api/v1.0/StatusMotos/{id}`
- **Descrição**: Atualiza um status existente
- **Headers**: `Authorization: Bearer {token}`
- **Body**:
```json
{
    "status": "DISPONIVEL",
    "descricao": "string",
    "motoId": 0
  }
  ```
- **Resposta**:
  ```json
  {
    "id": 0,
    "status": "DISPONIVEL",
    "descricao": "string",
    "motoId": 0,
    "dataCriacao": "2024-01-01T00:00:00Z"
  }
  ```

#### Deletar Status
- **Endpoint**: `DELETE /api/v1.0/StatusMotos/{id}`
- **Descrição**: Deleta um status
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**: `204 No Content`

### 🤖 Machine Learning

#### Treinar Modelo
- **Endpoint**: `POST /api/v2/ml/train-model`
- **Descrição**: Treina o modelo de machine learning
- **Headers**: `Authorization: Bearer {token}`
- **Body**:
```json
{
    "trainingData": [
      {
        "motoId": 0,
        "status": "DISPONIVEL",
        "dataCriacao": "2024-01-01T00:00:00Z"
      }
    ]
  }
  ```
- **Resposta**:
  ```json
  {
    "success": true,
    "message": "Modelo treinado com sucesso",
    "accuracy": 0.95,
    "trainingTime": "00:00:30"
  }
  ```

#### Prever Status
- **Endpoint**: `POST /api/v2/ml/predict-status`
- **Descrição**: Preve o status de uma moto usando ML
- **Headers**: `Authorization: Bearer {token}`
- **Body**:
```json
{
    "motoId": 0,
    "features": {
      "tempoUso": 0,
      "manutencoes": 0,
      "operacoes": 0
    }
  }
  ```
- **Resposta**:
  ```json
  {
    "predictedStatus": "DISPONIVEL",
    "confidence": 0.95,
    "features": {
      "tempoUso": 0,
      "manutencoes": 0,
      "operacoes": 0
  }
}
```

#### Analisar Padrões
- **Endpoint**: `GET /api/v2/ml/analyze-patterns`
- **Descrição**: Analisa padrões nos dados das motos
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**:
```json
{
    "patterns": [
      {
        "pattern": "string",
        "frequency": 0,
        "description": "string"
      }
    ],
    "insights": [
      {
        "insight": "string",
        "confidence": 0.95
      }
    ]
  }
  ```

#### Informações do Modelo
- **Endpoint**: `GET /api/v2/ml/model-info`
- **Descrição**: Retorna informações sobre o modelo ML
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**:
  ```json
  {
    "modelName": "string",
    "version": "string",
    "lastTraining": "2024-01-01T00:00:00Z",
    "accuracy": 0.95,
    "features": ["string"]
  }
  ```

### 🏥 Health Checks

#### Health Check Geral
- **Endpoint**: `GET /api/v1.0/Health`
- **Descrição**: Verifica a saúde geral da aplicação
- **Resposta**:
```json
{
  "status": "Healthy",
    "timestamp": "2024-01-15T16:45:00Z",
    "uptime": "02:30:15",
    "version": "1.0.0"
  }
  ```

#### Health Check do Banco
- **Endpoint**: `GET /api/v1.0/Health/database`
- **Descrição**: Verifica a saúde da conexão com o banco
- **Resposta**:
  ```json
  {
    "status": "Healthy",
    "database": "Oracle",
    "connectionTime": "00:00:02",
    "timestamp": "2024-01-15T16:45:00Z"
  }
  ```

#### Health Check da Memória
- **Endpoint**: `GET /api/v1.0/Health/memory`
- **Descrição**: Verifica o uso de memória da aplicação
- **Resposta**:
  ```json
  {
    "status": "Healthy",
    "memoryUsage": {
      "total": 8589934592,
      "used": 4294967296,
      "free": 4294967296,
      "percentage": 50.0
    },
    "timestamp": "2024-01-15T16:45:00Z"
  }
  ```

### 🌤️ Weather Forecast

#### Previsão do Tempo
- **Endpoint**: `GET /api/v1.0/WeatherForecast`
- **Descrição**: Retorna previsão do tempo (endpoint de exemplo)
- **Resposta**:
  ```json
  [
    {
      "date": "2024-01-15T00:00:00Z",
      "temperatureC": 25,
      "temperatureF": 77,
      "summary": "Ensolarado"
    },
    {
      "date": "2024-01-16T00:00:00Z",
      "temperatureC": 22,
      "temperatureF": 72,
      "summary": "Parcialmente nublado"
    },
    {
      "date": "2024-01-17T00:00:00Z",
      "temperatureC": 28,
      "temperatureF": 82,
      "summary": "Ensolarado"
    }
  ]
  ```

## 🔒 Autenticação e Autorização

### Perfis de Usuário
- **ADMIN**: Acesso total ao sistema
- **GERENTE**: Acesso a operações e relatórios
- **OPERADOR**: Acesso básico às operações

### Headers de Autenticação
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjUzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImFsYSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFsYUBleGFtcGxlLmNvbSIsInBlcmZpbCI6IkdFUkVOVEUiLCJjbnBqIjoiOTguNzY1LjAwMC8wMDAxLTEwIiwianRpIjoiM2FkNmE3MDgtNjViZi00N2U0LWJiYWUtYTM4Zjk2Mzk3MjUzIiwiaWF0IjoxNzYxNjU5NjQ0LCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiR0VSRU5URSIsIk9QRVJBRE9SIl0sImV4cCI6MTc2MTY2MzI0NCwiaXNzIjoiVHJhY2tab25lQVBJIiwiYXVkIjoiVHJhY2tab25lVXNlcnMifQ.Sqsv8feSsJCZKZL4AXj_zjdnCdiKrMcvZSZaou72fMQ
```

### Códigos de Status
- `200`: Sucesso
- `201`: Criado com sucesso
- `204`: Sem conteúdo (deletado)
- `400`: Requisição inválida
- `401`: Não autorizado
- `403`: Proibido
- `404`: Não encontrado
- `500`: Erro interno do servidor

## 📝 Notas Importantes

### Valores Válidos para Tipo de Operação
- `0` = CHECK_IN
- `1` = CHECK_OUT

### Credenciais de Teste
- **Email**: `ala@example.com`
- **Senha**: `123456`
- **Perfil**: GERENTE
- **ID do Usuário**: `53`
- **CNPJ**: `98.765.000/0001-10`

---

## 🚀 Desenvolvimento e Deploy

### 🔧 Configuração de Desenvolvimento

#### 📦 Dependências do Projeto
```xml
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
<PackageReference Include="Oracle.EntityFrameworkCore" Version="9.0.0" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="9.0.0" />
<PackageReference Include="Microsoft.ML" Version="3.0.1" />
<PackageReference Include="AutoMapper" Version="13.0.1" />
<PackageReference Include="xunit" Version="2.9.2" />
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="9.0.0" />
```

#### 🗄️ Configuração do Banco de Dados
```sql
-- Script de criação das tabelas Oracle
CREATE TABLE usuarios (
    id NUMBER(19) PRIMARY KEY,
    nome VARCHAR2(100) NOT NULL,
    email VARCHAR2(100) UNIQUE NOT NULL,
    senha_hash VARCHAR2(255) NOT NULL,
    perfil VARCHAR2(20) NOT NULL,
    cnpj VARCHAR2(20),
    telefone VARCHAR2(20),
    endereco VARCHAR2(200),
    nome_filial VARCHAR2(100),
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE motos (
    id NUMBER(19) PRIMARY KEY,
    placa VARCHAR2(10) UNIQUE NOT NULL,
    chassi VARCHAR2(50) UNIQUE NOT NULL,
    motor VARCHAR2(100),
    usuario_id NUMBER(19) REFERENCES usuarios(id),
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE operacoes (
    id NUMBER(19) PRIMARY KEY,
    tipo_operacao VARCHAR2(20) NOT NULL,
    descricao VARCHAR2(1000),
    moto_id NUMBER(19) REFERENCES motos(id),
    usuario_id NUMBER(19) REFERENCES usuarios(id),
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT ck_tipo_operacao CHECK (tipo_operacao IN ('CHECK_IN', 'CHECK_OUT'))
);

CREATE TABLE status_motos (
    id NUMBER(19) PRIMARY KEY,
    status VARCHAR2(20) NOT NULL,
    descricao VARCHAR2(1000),
    moto_id NUMBER(19) REFERENCES motos(id),
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

### 🧪 Estrutura dos Testes

#### 📁 Organização dos Testes
```
Tests/
├── Unit/
│   ├── JwtServiceTests.cs      # Testes do serviço JWT
│   ├── MotoServiceTests.cs     # Testes do serviço de motos
│   ├── OperacaoServiceTests.cs # Testes do serviço de operações
│   └── MLServiceTests.cs       # Testes do ML.NET
├── Integration/
│   ├── AuthIntegrationTests.cs # Testes de integração de auth
│   ├── MotoIntegrationTests.cs # Testes de integração de motos
│   └── DatabaseTests.cs        # Testes do banco de dados
└── Helpers/
    ├── TestDataBuilder.cs      # Builder para dados de teste
    └── WebApplicationFactory.cs # Factory para testes de integração
```

#### 🎯 Exemplo de Teste Unitário
```csharp
[Fact]
public void GenerateToken_ValidUser_ReturnsValidToken()
{
    // Arrange
    var user = new Usuario { Id = 1, Email = "test@test.com", Perfil = PerfilUsuario.ADMIN };
    
    // Act
    var token = _jwtService.GenerateToken(user);
    
    // Assert
    Assert.NotNull(token);
    Assert.True(_jwtService.ValidateToken(token));
}
```

### 📊 Métricas e Monitoramento

#### 📈 Health Checks Detalhados
- **Uptime**: Tempo de execução da aplicação
- **Memory Usage**: Uso de memória em tempo real
- **Database Connection**: Status da conexão Oracle
- **Response Time**: Tempo de resposta dos endpoints

#### 🔍 Logs e Observabilidade
- **Structured Logging** com Serilog
- **Correlation IDs** para rastreamento de requisições
- **Performance Counters** para métricas de performance
- **Error Tracking** com detalhes de exceções

### 🚀 Deploy e Produção

#### 🐳 Docker Support
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["challenge-3-net.csproj", "."]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "challenge-3-net.dll"]
```

#### ☁️ Azure Deployment
```yaml
# azure-pipelines.yml
trigger:
- main

pool:
  vmImage: 'ubuntu-latest'

variables:
  buildConfiguration: 'Release'

steps:
- task: DotNetCoreCLI@2
  displayName: 'Restore packages'
  inputs:
    command: 'restore'
    projects: '**/*.csproj'

- task: DotNetCoreCLI@2
  displayName: 'Build project'
  inputs:
    command: 'build'
    projects: '**/*.csproj'
    arguments: '--configuration $(buildConfiguration)'

- task: DotNetCoreCLI@2
  displayName: 'Run tests'
  inputs:
    command: 'test'
    projects: '**/*Tests.csproj'
    arguments: '--configuration $(buildConfiguration) --collect:"XPlat Code Coverage"'

- task: DotNetCoreCLI@2
  displayName: 'Publish project'
  inputs:
    command: 'publish'
    projects: '**/*.csproj'
    arguments: '--configuration $(buildConfiguration) --output $(Build.ArtifactStagingDirectory)'
```

### 📚 Documentação Adicional

#### 🔗 Links Úteis
- **Swagger UI**: `/swagger` - Documentação interativa da API
- **Health Dashboard**: `/health` - Status da aplicação
- **API Documentation**: `/api-docs` - Documentação OpenAPI

#### 📖 Recursos de Aprendizado
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [ML.NET Documentation](https://docs.microsoft.com/en-us/dotnet/machine-learning/)
- [JWT Authentication](https://jwt.io/)

### 🎯 Próximos Passos

#### 🔮 Melhorias Futuras
- **Rate Limiting** para controle de requisições
- **Caching** com Redis para performance
- **Message Queues** para processamento assíncrono
- **Microservices** para escalabilidade
- **GraphQL** para consultas flexíveis

#### 📊 Monitoramento Avançado
- **Application Insights** para telemetria
- **Prometheus** para métricas customizadas
- **Grafana** para dashboards
- **ELK Stack** para logs centralizados

---

## 🏆 Conclusão

O **TrackZone API** representa uma implementação completa e profissional de uma API RESTful utilizando **.NET 9**, demonstrando:

✅ **Arquitetura Limpa** com separação de responsabilidades  
✅ **Segurança Robusta** com JWT e autorização baseada em roles  
✅ **Machine Learning** integrado com ML.NET  
✅ **Testes Abrangentes** com xUnit e WebApplicationFactory  
✅ **Documentação Completa** com Swagger e exemplos práticos  
✅ **Health Checks** para monitoramento em produção  
✅ **Versionamento** para evolução da API  

Este projeto atende a todos os requisitos do **ADVANCED BUSINESS DEVELOPMENT WITH .NET** e está pronto para produção! 🚀
