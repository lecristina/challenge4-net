using Microsoft.EntityFrameworkCore;
using challenge_3_net.Data;
using challenge_3_net.Repositories;
using challenge_3_net.Repositories.Interfaces;
using challenge_3_net.Services;
using challenge_3_net.Services.Interfaces;
using challenge_3_net.Services.Mapping;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar Application Insights
builder.Services.AddApplicationInsightsTelemetry();

// Configurar Entity Framework
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (!string.IsNullOrEmpty(connectionString))
    {
        options.UseSqlServer(connectionString);
    }
    else
    {
        // Fallback para SQL Server local em desenvolvimento
        options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SistemaGestaoMotos;Trusted_Connection=true;MultipleActiveResultSets=true");
    }
});

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Sistema de Gestão de Motos API",
        Version = "v1",
        Description = "API RESTful para gerenciamento de motos, usuários, operações e status",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Equipe de Desenvolvimento",
            Email = "dev@fiap.com"
        }
    });

    // Incluir comentários XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Configurar exemplos de payloads
    // c.EnableAnnotations(); // Comentado pois não está disponível na versão atual
});

// Configurar injeção de dependência - Repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IOperacaoRepository, OperacaoRepository>();
builder.Services.AddScoped<IStatusMotoRepository, StatusMotoRepository>();

// Configurar injeção de dependência - Serviços
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IMotoService, MotoService>();
builder.Services.AddScoped<IOperacaoService, OperacaoService>();
builder.Services.AddScoped<IStatusMotoService, StatusMotoService>();

// Configurar HttpContextAccessor para HATEOAS
builder.Services.AddHttpContextAccessor();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Log de inicialização
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("=== Iniciando aplicação ===");
logger.LogInformation("Ambiente: {Environment}", app.Environment.EnvironmentName);
logger.LogInformation("Connection String presente: {HasConnectionString}", 
    !string.IsNullOrEmpty(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure the HTTP request pipeline.
// Habilitar Swagger em todos os ambientes para facilitar testes
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema de Gestão de Motos API v1");
    if (app.Environment.IsDevelopment())
    {
        c.RoutePrefix = string.Empty; // Para acessar o Swagger na raiz apenas em dev
    }
    c.DocumentTitle = "Sistema de Gestão de Motos API";
    c.DisplayRequestDuration();
    c.EnableDeepLinking();
    c.EnableFilter();
    c.ShowExtensions();
    c.EnableValidator();
});

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Endpoint de health check detalhado
app.MapGet("/health", (IConfiguration config) => {
    try 
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        
        return Results.Ok(new { 
            Status = "Healthy", 
            Timestamp = DateTime.UtcNow,
            Environment = app.Environment.EnvironmentName,
            HasConnectionString = !string.IsNullOrEmpty(connectionString),
            ConnectionStringLength = connectionString?.Length ?? 0,
            EnvironmentVariables = new {
                DB_SERVER = Environment.GetEnvironmentVariable("DB_SERVER"),
                DB_DATABASE = Environment.GetEnvironmentVariable("DB_DATABASE"),
                DB_USERNAME = Environment.GetEnvironmentVariable("DB_USERNAME"),
                HasPassword = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_PASSWORD")),
                ASPNETCORE_ENVIRONMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            }
        });
    }
    catch (Exception ex)
    {
        return Results.Ok(new { 
            Status = "Error", 
            Error = ex.Message,
            Timestamp = DateTime.UtcNow 
        });
    }
});

// Endpoint de diagnóstico do banco
app.MapGet("/health/database", async (ApplicationDbContext context) => {
    try 
    {
        var canConnect = await context.Database.CanConnectAsync();
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        var appliedMigrations = await context.Database.GetAppliedMigrationsAsync();
        
        return Results.Ok(new { 
            Status = canConnect ? "Connected" : "Cannot Connect",
            CanConnect = canConnect,
            PendingMigrations = pendingMigrations,
            AppliedMigrations = appliedMigrations,
            Timestamp = DateTime.UtcNow
        });
    }
    catch (Exception ex)
    {
        return Results.Ok(new { 
            Status = "Database Error", 
            Error = ex.Message,
            InnerError = ex.InnerException?.Message,
            Timestamp = DateTime.UtcNow 
        });
    }
});

// Endpoint para verificar dados no banco
app.MapGet("/admin/data", async (ApplicationDbContext context) => {
    try 
    {
        var usuarios = await context.Usuarios.CountAsync();
        var motos = await context.Motos.CountAsync();
        var operacoes = await context.Operacoes.CountAsync();
        var statusMotos = await context.StatusMotos.CountAsync();
        
        return Results.Ok(new { 
            Status = "Success",
            DataCounts = new {
                Usuarios = usuarios,
                Motos = motos,
                Operacoes = operacoes,
                StatusMotos = statusMotos
            },
            Timestamp = DateTime.UtcNow
        });
    }
    catch (Exception ex)
    {
        return Results.Ok(new { 
            Status = "Error", 
            Error = ex.Message,
            InnerError = ex.InnerException?.Message,
            Timestamp = DateTime.UtcNow 
        });
    }
});

// Configurar banco de dados com tratamento de erro
var dbLogger = app.Services.GetRequiredService<ILogger<Program>>();
try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        // Verificar se o banco está acessível (as tabelas já foram criadas pelo script SQL)
        var canConnect = await context.Database.CanConnectAsync();
        if (canConnect)
        {
            dbLogger.LogInformation("Banco de dados conectado com sucesso!");
            
            // Verificar se existem dados de teste
            var usuarioCount = await context.Usuarios.CountAsync();
            dbLogger.LogInformation("Usuários encontrados no banco: {Count}", usuarioCount);
        }
        else
        {
            dbLogger.LogWarning("Não foi possível conectar ao banco de dados.");
        }
    }
}
catch (Exception ex)
{
    dbLogger.LogError(ex, "Erro ao configurar o banco de dados durante a inicialização");
    // Não parar a aplicação por causa do banco - deixar continuar e tratar nos endpoints
}

// Endpoint para testar diretamente os dados dos usuários
app.MapGet("/debug/usuarios", async (ApplicationDbContext context) => {
    try 
    {
        var usuarios = await context.Usuarios.Take(5).ToListAsync();
        return Results.Ok(new { 
            Status = "Success",
            Count = usuarios.Count,
            Usuarios = usuarios.Select(u => new {
                u.Id,
                u.NomeFilial,
                u.Email,
                u.Cnpj,
                u.Perfil,
                u.DataCriacao
            }),
            Timestamp = DateTime.UtcNow
        });
    }
    catch (Exception ex)
    {
        return Results.Ok(new { 
            Status = "Error", 
            Error = ex.Message,
            InnerError = ex.InnerException?.Message,
            StackTrace = ex.StackTrace?.Split('\n').Take(5),
            Timestamp = DateTime.UtcNow 
        });
    }
});

app.Run();
