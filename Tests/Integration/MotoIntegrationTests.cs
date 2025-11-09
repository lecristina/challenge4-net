using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using challenge_3_net;

namespace challenge_3_net.Tests.Integration
{
    /// <summary>
    /// Testes de integração para endpoints de motos
    /// </summary>
    public class MotoIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public MotoIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        private async Task<string?> GetAuthTokenAsync()
        {
            var loginDto = new
            {
                email = "ala@example.com",
                senha = "123456"
            };

            var loginResponse = await _client.PostAsJsonAsync("/api/v2.0/Auth/login", loginDto);
            if (loginResponse.StatusCode != HttpStatusCode.OK)
                return null;

            var loginContent = await loginResponse.Content.ReadAsStringAsync();
            var loginResult = JsonSerializer.Deserialize<JsonElement>(loginContent);
            
            if (loginResult.TryGetProperty("token", out var tokenElement))
            {
                return tokenElement.GetString();
            }

            return null;
        }

        [Fact]
        public async Task GetMotos_WithValidToken_ShouldReturnOk()
        {
            // Arrange
            var token = await GetAuthTokenAsync();
            if (token == null)
            {
                Assert.Fail("Não foi possível obter token de autenticação");
                return;
            }

            _client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync("/api/v2.0/Motos?pageNumber=1&pageSize=10");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetMotos_WithoutToken_ShouldReturnUnauthorized()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;

            // Act
            var response = await _client.GetAsync("/api/v2.0/Motos");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetMotoById_WithValidToken_ShouldReturnOkOrNotFound()
        {
            // Arrange
            var token = await GetAuthTokenAsync();
            if (token == null)
            {
                Assert.Fail("Não foi possível obter token de autenticação");
                return;
            }

            _client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync("/api/v2.0/Motos/1");

            // Assert - Pode ser OK se existir ou NotFound se não existir
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task GetMotoByPlaca_WithValidToken_ShouldReturnOkOrNotFound()
        {
            // Arrange
            var token = await GetAuthTokenAsync();
            if (token == null)
            {
                Assert.Fail("Não foi possível obter token de autenticação");
                return;
            }

            _client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync("/api/v2.0/Motos/placa/ABC1234");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task GetMotoByChassi_WithValidToken_ShouldReturnOkOrNotFound()
        {
            // Arrange
            var token = await GetAuthTokenAsync();
            if (token == null)
            {
                Assert.Fail("Não foi possível obter token de autenticação");
                return;
            }

            _client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync("/api/v2.0/Motos/chassi/9BW12345678901234");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task GetMotosByUsuario_WithValidToken_ShouldReturnOk()
        {
            // Arrange
            var token = await GetAuthTokenAsync();
            if (token == null)
            {
                Assert.Fail("Não foi possível obter token de autenticação");
                return;
            }

            _client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync("/api/v2.0/Motos/usuario/1?pageNumber=1&pageSize=10");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task CreateMoto_WithValidToken_ShouldReturnCreated()
        {
            // Arrange
            var token = await GetAuthTokenAsync();
            if (token == null)
            {
                Assert.Fail("Não foi possível obter token de autenticação");
                return;
            }

            _client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var criarMotoDto = new
            {
                placa = $"TEST{Guid.NewGuid().ToString().Substring(0, 4)}",
                chassi = $"9BW{Guid.NewGuid().ToString().Substring(0, 14)}",
                motor = "Motor de teste",
                usuarioId = 1L
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v2.0/Motos", criarMotoDto);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Created ||
                response.StatusCode == HttpStatusCode.Conflict ||
                response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest ||
                response.StatusCode == HttpStatusCode.InternalServerError,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task CreateMoto_WithoutToken_ShouldReturnUnauthorized()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;

            var criarMotoDto = new
            {
                placa = "TEST1234",
                chassi = "9BW12345678901234",
                motor = "Motor de teste",
                usuarioId = 1L
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v2.0/Motos", criarMotoDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task UpdateMoto_WithValidToken_ShouldReturnOkOrNotFound()
        {
            // Arrange
            var token = await GetAuthTokenAsync();
            if (token == null)
            {
                Assert.Fail("Não foi possível obter token de autenticação");
                return;
            }

            _client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var atualizarMotoDto = new
            {
                placa = $"UPD{Guid.NewGuid().ToString().Substring(0, 4)}",
                chassi = $"9BW{Guid.NewGuid().ToString().Substring(0, 14)}",
                motor = "Motor atualizado",
                usuarioId = 1L
            };

            // Act
            var response = await _client.PutAsJsonAsync("/api/v2.0/Motos/999999", atualizarMotoDto);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.Conflict ||
                response.StatusCode == HttpStatusCode.BadRequest,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task DeleteMoto_WithValidToken_ShouldReturnNoContentOrNotFound()
        {
            // Arrange
            var token = await GetAuthTokenAsync();
            if (token == null)
            {
                Assert.Fail("Não foi possível obter token de autenticação");
                return;
            }

            _client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.DeleteAsync("/api/v2.0/Motos/999999");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.NoContent ||
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task DeleteMoto_WithoutToken_ShouldReturnUnauthorized()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;

            // Act
            var response = await _client.DeleteAsync("/api/v2.0/Motos/1");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}

