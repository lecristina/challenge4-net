# TrackZone - Sistema de Gestão de Motos API

## 📋 Descrição da Solução

O **TrackZone** é uma API RESTful desenvolvida em .NET 9 para gerenciamento completo de motos, usuários, operações e status. O sistema oferece:

- **Gestão de Usuários**: Controle de acesso com diferentes perfis (Admin, Gerente, Operador)
- **Gestão de Motos**: Cadastro, atualização e controle de veículos
- **Gestão de Operações**: Registro de operações realizadas (Venda, Aluguel, Manutenção, Devolução)
- **Gestão de Status**: Controle de estados das motos (Disponível, Alugada, Manutenção, Vendida)

## 🎯 Objetivos Acadêmicos

### Conceitos .NET Demonstrados:
- **API RESTful**: Implementação completa com verbos HTTP adequados
- **Entity Framework Core**: ORM para acesso a dados com SQL Server
- **Arquitetura em Camadas**: Controllers, Services, Repositories e DTOs
- **Injeção de Dependência**: Padrão IoC implementado
- **Validação de Dados**: Data Annotations e ModelState
- **Documentação**: Swagger/OpenAPI configurado
- **Paginação**: Implementação de paginação em todos os endpoints
- **HATEOAS**: Links de navegação nos responses
- **Tratamento de Erros**: Try-catch com logging estruturado
- **Clean Architecture**: Separação clara de responsabilidades

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
- **SQL Server** - Banco de dados relacional
- **Code First** - Migrations para criação do banco

### Padrões e Bibliotecas:
- **AutoMapper** - Mapeamento de objetos
- **BCrypt.Net-Next** - Criptografia de senhas
- **FluentValidation** - Validação de dados
- **Swagger/OpenAPI** - Documentação interativa da API

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

### 🔧 **ENDPOINTS DE SISTEMA**

#### Health Check
```http
GET /health
```
**Descrição**: Verifica o status da aplicação e conectividade com o banco de dados.

#### Health Check do Banco
```http
GET /health/database
```
**Descrição**: Verifica especificamente a conectividade e migrações do banco de dados.

#### Dados do Sistema
```http
GET /admin/data
```
**Descrição**: Retorna contadores de registros em cada tabela do sistema.

#### Debug Usuários
```http
GET /debug/usuarios
```
**Descrição**: Endpoint de debug para verificar dados de usuários (apenas desenvolvimento).

#### Weather Forecast (Template)
```http
GET /WeatherForecast
```
**Descrição**: Endpoint de exemplo do template .NET (pode ser removido em produção).

---

### 👥 **ENDPOINTS DE USUÁRIOS** (`/api/usuarios`)

#### 1. Listar Usuários
```http
GET /api/usuarios?pageNumber=1&pageSize=10
```
**Parâmetros**:
- `pageNumber` (opcional): Número da página (padrão: 1)
- `pageSize` (opcional): Tamanho da página (padrão: 10, máximo: 100)

**Resposta**:
```json
{
  "items": [
    {
      "id": 1,
      "nomeFilial": "Empresa Exemplo",
      "email": "contato@empresa.com",
      "cnpj": "12.345.678/0001-90",
      "endereco": "Rua das Flores, 123",
      "telefone": "(11) 99999-9999",
      "perfil": 0,
      "dataCriacao": "2025-01-16T10:00:00Z",
      "dataAtualizacao": "2025-01-16T10:00:00Z",
      "links": []
    }
  ],
  "pageNumber": 1,
  "pageSize": 10,
  "totalItems": 1,
  "totalPages": 1,
  "hasPreviousPage": false,
  "hasNextPage": false,
  "links": []
}
```

#### 2. Buscar Usuário por ID
```http
GET /api/usuarios/{id}
```
**Parâmetros**:
- `id`: ID do usuário (long)

**Resposta** (200):
```json
{
  "id": 1,
  "nomeFilial": "Empresa Exemplo",
  "email": "contato@empresa.com",
  "cnpj": "12.345.678/0001-90",
  "endereco": "Rua das Flores, 123",
  "telefone": "(11) 99999-9999",
  "perfil": 0,
  "dataCriacao": "2025-01-16T10:00:00Z",
  "dataAtualizacao": "2025-01-16T10:00:00Z",
  "links": []
}
```

#### 3. Criar Usuário
```http
POST /api/usuarios
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
  "perfil": 1
}
```

**Resposta** (201):
```json
{
  "id": 2,
  "nomeFilial": "Nova Empresa",
  "email": "novo@empresa.com",
  "cnpj": "98.765.432/0001-10",
  "endereco": "Av. Principal, 456",
  "telefone": "(11) 88888-8888",
  "perfil": 1,
  "dataCriacao": "2025-01-16T10:30:00Z",
  "dataAtualizacao": "2025-01-16T10:30:00Z",
  "links": []
}
```

#### 4. Atualizar Usuário
```http
PUT /api/usuarios/{id}
Content-Type: application/json
```
**Body**:
```json
{
  "nomeFilial": "Empresa Atualizada",
  "email": "atualizado@empresa.com",
  "cnpj": "98.765.432/0001-10",
  "endereco": "Av. Principal, 456 - Atualizada",
  "telefone": "(11) 77777-7777",
  "perfil": 2
}
```

#### 5. Deletar Usuário
```http
DELETE /api/usuarios/{id}
```
**Resposta** (204): No Content

---

### 🏍️ **ENDPOINTS DE MOTOS** (`/api/motos`)

#### 1. Listar Motos
```http
GET /api/motos?pageNumber=1&pageSize=10
```

#### 2. Buscar Moto por ID
```http
GET /api/motos/{id}
```

#### 3. Buscar Moto por Placa
```http
GET /api/motos/placa/{placa}
```
**Exemplo**: `GET /api/motos/placa/ABC1234`

#### 4. Buscar Moto por Chassi
```http
GET /api/motos/chassi/{chassi}
```
**Exemplo**: `GET /api/motos/chassi/9BWHE21JX24067890`

#### 5. Listar Motos por Usuário
```http
GET /api/motos/usuario/{usuarioId}?pageNumber=1&pageSize=10
```

#### 6. Criar Moto
```http
POST /api/motos
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

**Resposta** (201):
```json
{
  "id": 1,
  "placa": "XYZ5678",
  "chassi": "9BWHE21JX24067890",
  "motor": "Yamaha MT-09 900cc",
  "usuarioId": 1,
  "nomeFilial": "Empresa Exemplo",
  "dataCriacao": "2025-01-16T11:00:00Z",
  "dataAtualizacao": "2025-01-16T11:00:00Z",
  "links": []
}
```

#### 7. Atualizar Moto
```http
PUT /api/motos/{id}
Content-Type: application/json
```
**Body**:
```json
{
  "placa": "XYZ5678",
  "chassi": "9BWHE21JX24067890",
  "motor": "Yamaha MT-09 900cc - Atualizada",
  "usuarioId": 2
}
```

#### 8. Deletar Moto
```http
DELETE /api/motos/{id}
```

---

### ⚙️ **ENDPOINTS DE OPERAÇÕES** (`/api/operacoes`)

#### 1. Listar Operações
```http
GET /api/operacoes?pageNumber=1&pageSize=10
```

#### 2. Buscar Operação por ID
```http
GET /api/operacoes/{id}
```

#### 3. Criar Operação
```http
POST /api/operacoes
Content-Type: application/json
```
**Body**:
```json
{
  "tipoOperacao": 0,
  "descricao": "Entrega da moto para cliente empresarial - Período de 30 dias",
  "motoId": 1,
  "usuarioId": 1
}
```

**Resposta** (201):
```json
{
  "id": 1,
  "tipoOperacao": 0,
  "descricao": "Entrega da moto para cliente empresarial - Período de 30 dias",
  "dataOperacao": "2025-01-16T12:00:00Z",
  "motoId": 1,
  "placaMoto": "XYZ5678",
  "usuarioId": 1,
  "nomeUsuario": "Empresa Exemplo",
  "links": []
}
```

#### 4. Atualizar Operação
```http
PUT /api/operacoes/{id}
Content-Type: application/json
```
**Body**:
```json
{
  "tipoOperacao": 1,
  "descricao": "Operação atualizada - Coleta da moto após entrega",
  "motoId": 1,
  "usuarioId": 2
}
```

#### 5. Deletar Operação
```http
DELETE /api/operacoes/{id}
```

---

### 📊 **ENDPOINTS DE STATUS MOTOS** (`/api/statusmotos`)

#### 1. Listar Status
```http
GET /api/statusmotos?pageNumber=1&pageSize=10
```

#### 2. Buscar Status por ID
```http
GET /api/statusmotos/{id}
```

#### 3. Buscar Status Atual da Moto
```http
GET /api/statusmotos/moto/{motoId}/atual
```

#### 4. Listar Histórico de Status da Moto
```http
GET /api/statusmotos/moto/{motoId}/historico?pageNumber=1&pageSize=10
```

#### 5. Listar Status por Tipo
```http
GET /api/statusmotos/tipo/{status}?pageNumber=1&pageSize=10
```
**Exemplo**: `GET /api/statusmotos/tipo/DISPONIVEL`

#### 6. Criar Status
```http
POST /api/statusmotos
Content-Type: application/json
```
**Body**:
```json
{
  "status": 0,
  "descricao": "Moto disponível para uso - Revisão completa realizada",
  "area": "Pátio Principal - Setor A",
  "motoId": 1,
  "usuarioId": 1
}
```

**Resposta** (201):
```json
{
  "id": 1,
  "status": 0,
  "descricao": "Moto disponível para uso - Revisão completa realizada",
  "area": "Pátio Principal - Setor A",
  "dataStatus": "2025-01-16T13:00:00Z",
  "motoId": 1,
  "placaMoto": "XYZ5678",
  "usuarioId": 1,
  "nomeUsuario": "Empresa Exemplo",
  "links": []
}
```

#### 7. Atualizar Status
```http
PUT /api/statusmotos/{id}
Content-Type: application/json
```
**Body**:
```json
{
  "status": 2,
  "descricao": "Status atualizado - Moto em manutenção preventiva",
  "area": "Oficina - Setor B",
  "motoId": 1,
  "usuarioId": 2
}
```

#### 8. Deletar Status
```http
DELETE /api/statusmotos/{id}
```

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

## 📋 Checklist de Entrega

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