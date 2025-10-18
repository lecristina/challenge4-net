using Xunit;
using Microsoft.Extensions.Logging;
using challenge_3_net.Services.ML;
using challenge_3_net.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace challenge_3_net.Tests.Unit
{
    /// <summary>
    /// Testes unitários para MotoAnalysisService
    /// </summary>
    public class MotoAnalysisServiceTests
    {
        private readonly Mock<ILogger<MotoAnalysisService>> _mockLogger;
        private readonly ApplicationDbContext _context;
        private readonly MotoAnalysisService _motoAnalysisService;

        public MotoAnalysisServiceTests()
        {
            _mockLogger = new Mock<ILogger<MotoAnalysisService>>();

            // Configurar DbContext em memória
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            _motoAnalysisService = new MotoAnalysisService(_context, _mockLogger.Object);

            // Seed data para testes
            SeedTestData();
        }

        [Fact]
        public async Task TrainStatusPredictionModel_WithInsufficientData_ShouldReturnFailure()
        {
            // Arrange - Limpar dados existentes
            _context.StatusMotos.RemoveRange(_context.StatusMotos);
            _context.SaveChanges();

            // Act
            var result = await _motoAnalysisService.TrainStatusPredictionModelAsync();

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Dados insuficientes", result.Message);
            Assert.Equal(0, result.RecordsUsed);
        }

        [Fact]
        public async Task TrainStatusPredictionModel_WithSufficientData_ShouldReturnSuccess()
        {
            // Act
            var result = await _motoAnalysisService.TrainStatusPredictionModelAsync();

            // Assert
            Assert.True(result.Success);
            Assert.Contains("Modelo treinado com sucesso", result.Message);
            Assert.True(result.RecordsUsed > 0);
            Assert.True(result.Accuracy >= 0);
        }

        [Fact]
        public async Task PredictMotoStatus_WithValidInput_ShouldReturnPrediction()
        {
            // Arrange
            var input = new MotoPredictionInput
            {
                Perfil = "ADMIN",
                TipoOperacao = "CHECK_IN",
                DiasDesdeCriacao = 30,
                TotalOperacoes = 5
            };

            // Primeiro treinar o modelo
            await _motoAnalysisService.TrainStatusPredictionModelAsync();

            // Act
            var result = await _motoAnalysisService.PredictMotoStatusAsync(input);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.PredictedStatus);
            Assert.NotEmpty(result.PredictedStatus);
            Assert.True(result.Confidence >= 0);
        }

        [Fact]
        public async Task PredictMotoStatus_WithInvalidInput_ShouldHandleGracefully()
        {
            // Arrange
            var input = new MotoPredictionInput
            {
                Perfil = "",
                TipoOperacao = "",
                DiasDesdeCriacao = -1,
                TotalOperacoes = -1
            };

            // Act
            var result = await _motoAnalysisService.PredictMotoStatusAsync(input);

            // Assert
            // O resultado pode ser sucesso ou falha, mas não deve lançar exceção
            Assert.NotNull(result);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task AnalyzeOperationPatterns_WithData_ShouldReturnAnalysis()
        {
            // Act
            var result = await _motoAnalysisService.AnalyzeOperationPatternsAsync();

            // Assert
            Assert.True(result.Success);
            Assert.Contains("Análise de padrões concluída", result.Message);
            Assert.True(result.TotalOperacoes > 0);
            Assert.NotNull(result.OperacaoFrequencia);
            Assert.NotNull(result.OperacaoPorHora);
            Assert.NotNull(result.OperacaoPorUsuario);
            Assert.NotNull(result.MotosMaisAtivas);
        }

        [Fact]
        public async Task AnalyzeOperationPatterns_WithNoData_ShouldReturnFailure()
        {
            // Arrange - Limpar dados de operações
            _context.Operacoes.RemoveRange(_context.Operacoes);
            _context.SaveChanges();

            // Act
            var result = await _motoAnalysisService.AnalyzeOperationPatternsAsync();

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Nenhuma operação encontrada", result.Message);
        }

        [Theory]
        [InlineData("ADMIN", "CHECK_IN", 10, 3)]
        [InlineData("OPERADOR", "CHECK_OUT", 5, 1)]
        [InlineData("GERENTE", "CHECK_IN", 20, 7)]
        public async Task PredictMotoStatus_WithDifferentInputs_ShouldReturnValidPredictions(
            string perfil, string tipoOperacao, int diasDesdeCriacao, int totalOperacoes)
        {
            // Arrange
            var input = new MotoPredictionInput
            {
                Perfil = perfil,
                TipoOperacao = tipoOperacao,
                DiasDesdeCriacao = diasDesdeCriacao,
                TotalOperacoes = totalOperacoes
            };

            // Primeiro treinar o modelo
            await _motoAnalysisService.TrainStatusPredictionModelAsync();

            // Act
            var result = await _motoAnalysisService.PredictMotoStatusAsync(input);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.PredictedStatus);
            Assert.NotEmpty(result.PredictedStatus);
        }

        [Fact]
        public async Task TrainStatusPredictionModel_MultipleCalls_ShouldNotFail()
        {
            // Act
            var result1 = await _motoAnalysisService.TrainStatusPredictionModelAsync();
            var result2 = await _motoAnalysisService.TrainStatusPredictionModelAsync();

            // Assert
            Assert.True(result1.Success);
            Assert.True(result2.Success);
        }

        private void SeedTestData()
        {
            // Criar usuários de teste
            var usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Id = 1,
                    NomeFilial = "Filial Teste 1",
                    Email = "teste1@teste.com",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Cnpj = "11111111000111",
                    Perfil = Perfil.ADMIN,
                    DataCriacao = DateTime.UtcNow.AddDays(-30)
                },
                new Usuario
                {
                    Id = 2,
                    NomeFilial = "Filial Teste 2",
                    Email = "teste2@teste.com",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Cnpj = "22222222000222",
                    Perfil = Perfil.OPERADOR,
                    DataCriacao = DateTime.UtcNow.AddDays(-20)
                }
            };

            _context.Usuarios.AddRange(usuarios);

            // Criar motos de teste
            var motos = new List<Moto>
            {
                new Moto
                {
                    Id = 1,
                    Placa = "ABC-1234",
                    Chassi = "12345678901234567",
                    Motor = "150cc",
                    UsuarioId = 1,
                    DataCriacao = DateTime.UtcNow.AddDays(-25)
                },
                new Moto
                {
                    Id = 2,
                    Placa = "XYZ-5678",
                    Chassi = "98765432109876543",
                    Motor = "200cc",
                    UsuarioId = 2,
                    DataCriacao = DateTime.UtcNow.AddDays(-15)
                }
            };

            _context.Motos.AddRange(motos);

            // Criar status de motos
            var statusMotos = new List<StatusMoto>
            {
                new StatusMoto
                {
                    Id = 1,
                    MotoId = 1,
                    UsuarioId = 1,
                    Status = StatusMotoEnum.PRONTA,
                    Area = "Pátio Principal",
                    DataStatus = DateTime.UtcNow.AddDays(-1)
                },
                new StatusMoto
                {
                    Id = 2,
                    MotoId = 2,
                    UsuarioId = 2,
                    Status = StatusMotoEnum.MANUTENCAO_AGENDADA,
                    Area = "Oficina",
                    DataStatus = DateTime.UtcNow.AddDays(-2)
                }
            };

            _context.StatusMotos.AddRange(statusMotos);

            // Criar operações de teste
            var operacoes = new List<Operacao>
            {
                new Operacao
                {
                    Id = 1,
                    MotoId = 1,
                    UsuarioId = 1,
                    TipoOperacao = TipoOperacao.CHECK_IN,
                    Descricao = "Check-in inicial",
                    DataOperacao = DateTime.UtcNow.AddDays(-1)
                },
                new Operacao
                {
                    Id = 2,
                    MotoId = 1,
                    UsuarioId = 1,
                    TipoOperacao = TipoOperacao.CHECK_OUT,
                    Descricao = "Check-out para manutenção",
                    DataOperacao = DateTime.UtcNow.AddDays(-2)
                },
                new Operacao
                {
                    Id = 3,
                    MotoId = 2,
                    UsuarioId = 2,
                    TipoOperacao = TipoOperacao.CHECK_IN,
                    Descricao = "Check-in para reparo",
                    DataOperacao = DateTime.UtcNow.AddDays(-3)
                }
            };

            _context.Operacoes.AddRange(operacoes);

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
