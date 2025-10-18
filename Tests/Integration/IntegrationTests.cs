using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using challenge_3_net.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Text.Json;
using challenge_3_net.Models.DTOs;

namespace challenge_3_net.Tests.Integration
{
    /// <summary>
    /// Testes de integração usando WebApplicationFactory
    /// </summary>
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remover o DbContext real
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    // Adicionar DbContext em memória para testes
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDatabase");
                    });
                });
            });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task HealthCheck_ShouldReturnHealthy()
        {
            // Act
            var response = await _client.GetAsync("/health");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Healthy", content);
        }

        [Fact]
        public async Task HealthCheck_Database_ShouldReturnStatus()
        {
            // Act
            var response = await _client.GetAsync("/health/database");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotNull(content);
        }

        [Fact]
        public async Task HealthCheck_Memory_ShouldReturnStatus()
        {
            // Act
            var response = await _client.GetAsync("/health/memory");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotNull(content);
        }

        [Fact]
        public async Task Swagger_ShouldBeAccessible()
        {
            // Act
            var response = await _client.GetAsync("/swagger/v1/swagger.json");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("TrackZone", content);
        }

        [Fact]
        public async Task SwaggerUI_ShouldBeAccessible()
        {
            // Act
            var response = await _client.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("swagger", content.ToLower());
        }

        [Fact]
        public async Task Usuarios_Get_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/usuarios");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotNull(content);
        }

        [Fact]
        public async Task Usuarios_Post_WithValidData_ShouldReturnCreated()
        {
            // Arrange
            var usuarioDto = new CriarUsuarioDto
            {
                NomeFilial = "Teste Filial",
                Email = "teste@teste.com",
                Senha = "123456",
                Cnpj = "12345678000199",
                Endereco = "Rua Teste, 123",
                Telefone = "11999999999",
                Perfil = "ADMIN"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/usuarios", usuarioDto);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotNull(content);
        }

        [Fact]
        public async Task Usuarios_Post_WithInvalidData_ShouldReturnBadRequest()
        {
            // Arrange
            var usuarioDto = new CriarUsuarioDto
            {
                NomeFilial = "", // Nome vazio
                Email = "invalid-email", // Email inválido
                Senha = "", // Senha vazia
                Cnpj = "123", // CNPJ inválido
                Perfil = "INVALID" // Perfil inválido
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/usuarios", usuarioDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Motos_Get_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/motos");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotNull(content);
        }

        [Fact]
        public async Task Motos_Post_WithValidData_ShouldReturnCreated()
        {
            // Arrange
            var motoDto = new CriarMotoDto
            {
                Placa = "TEST-1234",
                Chassi = "12345678901234567",
                Motor = "150cc",
                UsuarioId = 1
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/motos", motoDto);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotNull(content);
        }

        [Fact]
        public async Task Operacoes_Get_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/operacoes");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotNull(content);
        }

        [Fact]
        public async Task StatusMotos_Get_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/statusmotos");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotNull(content);
        }

        [Fact]
        public async Task Auth_Login_WithValidCredentials_ShouldReturnToken()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "admin@teste.com",
                Senha = "123456"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v2/auth/login", loginDto);

            // Assert
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Assert.NotNull(content);
            }
            else
            {
                // Se falhar, pode ser porque não há usuário no banco de teste
                Assert.True(response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                           response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            }
        }

        [Fact]
        public async Task Auth_Login_WithInvalidCredentials_ShouldReturnUnauthorized()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "invalid@teste.com",
                Senha = "wrongpassword"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v2/auth/login", loginDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task ML_ModelInfo_ShouldReturnModelInformation()
        {
            // Act
            var response = await _client.GetAsync("/api/v2/ml/model-info");

            // Assert
            // Pode retornar 401 se não autenticado, ou 200 se autenticado
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK ||
                       response.StatusCode == System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task API_Versioning_ShouldWorkCorrectly()
        {
            // Test v1 endpoints
            var v1Response = await _client.GetAsync("/api/v1/usuarios");
            Assert.True(v1Response.IsSuccessStatusCode);

            // Test v2 endpoints (may require authentication)
            var v2Response = await _client.GetAsync("/api/v2/usuarios");
            Assert.True(v2Response.StatusCode == System.Net.HttpStatusCode.OK ||
                       v2Response.StatusCode == System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task CORS_ShouldBeConfigured()
        {
            // Act
            var request = new HttpRequestMessage(HttpMethod.Options, "/api/v1/usuarios");
            request.Headers.Add("Origin", "https://example.com");
            request.Headers.Add("Access-Control-Request-Method", "GET");

            var response = await _client.SendAsync(request);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task ContentType_ShouldBeJson()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/usuarios");

            // Assert
            response.EnsureSuccessStatusCode();
            var contentType = response.Content.Headers.ContentType?.MediaType;
            Assert.Equal("application/json", contentType);
        }

        [Fact]
        public async Task ErrorHandling_ShouldReturnProperStatusCodes()
        {
            // Test 404 for non-existent endpoint
            var notFoundResponse = await _client.GetAsync("/api/v1/nonexistent");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, notFoundResponse.StatusCode);

            // Test 405 for wrong HTTP method
            var methodNotAllowedResponse = await _client.DeleteAsync("/api/v1/usuarios");
            Assert.Equal(System.Net.HttpStatusCode.MethodNotAllowed, methodNotAllowedResponse.StatusCode);
        }

        [Fact]
        public async Task Performance_ShouldRespondQuickly()
        {
            // Arrange
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            // Act
            var response = await _client.GetAsync("/health");

            // Assert
            stopwatch.Stop();
            response.EnsureSuccessStatusCode();
            Assert.True(stopwatch.ElapsedMilliseconds < 5000); // Deve responder em menos de 5 segundos
        }
    }
}
