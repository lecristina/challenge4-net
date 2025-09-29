# Sistema de Gestão de Motos - .NET Core

Sistema RESTful para gerenciamento de motos, usuários, operações e status, desenvolvido em .NET 9.0 com Entity Framework Core e SQL Server.

## 🚀 Tecnologias

- **Backend:** .NET 9.0 / ASP.NET Core
- **Banco de Dados:** SQL Server (Azure SQL Database)
- **ORM:** Entity Framework Core 9.0
- **Documentação:** Swagger/OpenAPI
- **Mapeamento:** AutoMapper
- **Validação:** FluentValidation
- **Segurança:** BCrypt para hash de senhas
- **Monitoramento:** Application Insights
- **Cloud:** Azure App Service

## 📋 Funcionalidades

### Usuários
- ✅ CRUD completo de usuários
- ✅ Perfis: ADMIN, GERENTE, OPERADOR
- ✅ Validação de email e CNPJ únicos
- ✅ Hash seguro de senhas com BCrypt

### Motos
- ✅ Cadastro e gerenciamento de motos
- ✅ Controle por placa e chassi únicos
- ✅ Relacionamento com usuários responsáveis

### Status das Motos
- ✅ Rastreamento de status em tempo real
- ✅ Múltiplos status: DISPONIVEL, EM_USO, MANUTENCAO, etc.
- ✅ Histórico de mudanças de status
- ✅ Controle por área/localização

### Operações
- ✅ Registro de operações (CHECK_IN, CHECK_OUT, etc.)
- ✅ Histórico completo de operações
- ✅ Associação com usuários e motos

## 🔧 Configuração Local

### Pré-requisitos
- .NET 9.0 SDK
- SQL Server ou LocalDB
- Visual Studio 2022 ou VS Code

### Instalação
1. Clone o repositório:
```bash
git clone https://github.com/andrealtobelli/challenge3-devops-net.git
cd challenge3-devops-net
```

2. Configure a connection string no `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SistemaGestaoMotos;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

3. Execute as migrations:
```bash
cd challenge-3-net/challenge-3-net
dotnet ef database update
```

4. Execute a aplicação:
```bash
dotnet run
```

5. Acesse a documentação da API:
- Swagger UI: `https://localhost:5001`
- API: `https://localhost:5001/api`

## ☁️ Deploy no Azure

### Automático via Script
Execute o script de deploy automatizado:
```bash
chmod +x script-devops.sh
./script-devops.sh
```

O script irá:
- Criar grupos de recursos no Azure
- Configurar SQL Server no Azure
- Criar App Service Plan
- Deploy da aplicação .NET
- Configurar Application Insights
- Configurar variáveis de ambiente

### Manual via Azure CLI

1. **Criar recursos do Azure:**
```bash
az group create --name rg-trackzone-net --location brazilsouth
az sql server create --name trackzone-sql-server --resource-group rg-trackzone-net --location brazilsouth --admin-user sqladmin --admin-password YourPassword123
az sql db create --resource-group rg-trackzone-net --server trackzone-sql-server --name SistemaGestaoMotos --service-objective Basic
```

2. **Criar App Service:**
```bash
az appservice plan create --name trackzone-plan --resource-group rg-trackzone-net --sku F1 --is-linux
az webapp create --resource-group rg-trackzone-net --plan trackzone-plan --name trackzone-net-app --runtime "DOTNETCORE:9.0"
```

3. **Configurar variáveis de ambiente:**
```bash
az webapp config appsettings set --resource-group rg-trackzone-net --name trackzone-net-app --settings \
  ConnectionStrings__DefaultConnection="Server=trackzone-sql-server.database.windows.net;Database=SistemaGestaoMotos;User Id=sqladmin;Password=YourPassword123;Encrypt=true;"
```

## 🏗️ Arquitetura

```
├── Controllers/           # Controladores da API
├── Models/               # Entidades do domínio
├── Data/                 # Contexto do Entity Framework
├── Repositories/         # Camada de acesso a dados
├── Services/             # Lógica de negócio
├── DTOs/                 # Objetos de transferência
└── Migrations/           # Migrações do EF Core
```

### Padrões Implementados
- **Repository Pattern:** Abstração do acesso a dados
- **Service Layer:** Separação da lógica de negócio
- **DTO Pattern:** Transferência segura de dados
- **Dependency Injection:** Inversão de controle
- **Auto Mapping:** Mapeamento automático entre entidades e DTOs

## 📖 Documentação da API

### Endpoints Principais

#### Usuários
- `GET /api/usuarios` - Listar usuários
- `POST /api/usuarios` - Criar usuário
- `GET /api/usuarios/{id}` - Buscar usuário
- `PUT /api/usuarios/{id}` - Atualizar usuário
- `DELETE /api/usuarios/{id}` - Deletar usuário

#### Motos
- `GET /api/motos` - Listar motos
- `POST /api/motos` - Cadastrar moto
- `GET /api/motos/{id}` - Buscar moto
- `PUT /api/motos/{id}` - Atualizar moto
- `DELETE /api/motos/{id}` - Deletar moto

#### Status das Motos
- `GET /api/status-motos` - Listar status
- `POST /api/status-motos` - Registrar status
- `GET /api/status-motos/moto/{motoId}` - Status por moto

#### Operações
- `GET /api/operacoes` - Listar operações
- `POST /api/operacoes` - Registrar operação
- `GET /api/operacoes/moto/{motoId}` - Operações por moto

### Exemplos de Payloads

#### Criar Usuário
```json
{
  "nomeFilial": "Filial Centro",
  "email": "usuario@exemplo.com",
  "senha": "MinhaSenh@123",
  "cnpj": "12.345.678/0001-99",
  "endereco": "Rua das Flores, 123",
  "telefone": "(11) 99999-9999",
  "perfil": "OPERADOR"
}
```

#### Registrar Moto
```json
{
  "placa": "ABC1234",
  "chassi": "1HGBH41JXMN109186",
  "motor": "Honda 160cc",
  "usuarioId": 1
}
```

## 🔒 Segurança

- ✅ Senhas hasheadas com BCrypt
- ✅ Validação de entrada com FluentValidation
- ✅ CORS configurado para produção
- ✅ HTTPS obrigatório em produção
- ✅ Connection strings seguras no Azure

## 📊 Monitoramento

- **Application Insights:** Métricas de performance e erros
- **Health Checks:** Verificação de saúde da aplicação
- **Logs estruturados:** Rastreamento detalhado de operações

## 🧪 Testes

Execute os testes:
```bash
dotnet test
```

## 🐳 Docker

Construir e executar com Docker:
```bash
docker build -t trackzone-net .
docker run -p 8080:80 trackzone-net
```

## 📝 Variáveis de Ambiente

### Desenvolvimento
- `ConnectionStrings__DefaultConnection` - String de conexão do banco
- `ASPNETCORE_ENVIRONMENT` - Ambiente (Development/Production)

### Produção (Azure)
- `ConnectionStrings__DefaultConnection` - Azure SQL connection string
- `APPLICATIONINSIGHTS_CONNECTION_STRING` - Application Insights
- `DB_SERVER` - Servidor SQL
- `DB_DATABASE` - Nome da base de dados
- `DB_USERNAME` - Usuário do banco
- `DB_PASSWORD` - Senha do banco

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma feature branch (`git checkout -b feature/nova-feature`)
3. Commit suas mudanças (`git commit -am 'Adiciona nova feature'`)
4. Push para a branch (`git push origin feature/nova-feature`)
5. Crie um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.

## 👥 Equipe

- **Leticia Cristina Dos Santos Passos RM**: 555241
- **André Rogério Vieira Pavanela Altobelli Antunes RM**: 554764
- **Enrico Figueiredo Del Guerra RM**: 558604
- **FIAP** - Orientação acadêmica

---

**🚀 Sistema de Gestão de Motos - Transformando a mobilidade urbana com tecnologia .NET!**
