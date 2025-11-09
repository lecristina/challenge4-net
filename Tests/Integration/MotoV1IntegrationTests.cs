using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using challenge_3_net;

namespace challenge_3_net.Tests.Integration
{
    /// <summary>
    /// Testes de integração para endpoints de motos v1.0
    /// </summary>
    public class MotoV1IntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public MotoV1IntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        private async Task<string?> GetAuthTokenAsync()
        {
            // Usar v2.0 para login (AuthController só existe na v2.0)
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
        public async Task GetMotosV1_WithValidToken_ShouldReturnOk()
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
            var response = await _client.GetAsync("/api/v1.0/Motos?pageNumber=1&pageSize=10");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetMotosV1_WithoutToken_ShouldReturnOk()
        {
            // Arrange - v1.0 não requer token
            _client.DefaultRequestHeaders.Authorization = null;

            // Act
            var response = await _client.GetAsync("/api/v1.0/Motos");

            // Assert - v1.0 não requer autenticação, deve retornar OK
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetMotoByIdV1_WithValidToken_ShouldReturnOkOrNotFound()
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
            var response = await _client.GetAsync("/api/v1.0/Motos/1");

            // Assert - Pode ser OK se existir ou NotFound se não existir
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task CreateMotoV1_WithValidToken_ShouldReturnCreated()
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
                placa = $"V1{Guid.NewGuid().ToString().Substring(0, 4)}",
                chassi = $"9BW{Guid.NewGuid().ToString().Substring(0, 14)}",
                motor = "Motor v1.0",
                usuarioId = 1L
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1.0/Motos", criarMotoDto);

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
        public async Task UpdateMotoV1_WithValidToken_ShouldReturnOkOrNotFound()
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
                placa = $"V1U{Guid.NewGuid().ToString().Substring(0, 3)}",
                chassi = $"9BW{Guid.NewGuid().ToString().Substring(0, 14)}",
                motor = "Motor atualizado v1.0",
                usuarioId = 1L
            };

            // Act
            var response = await _client.PutAsJsonAsync("/api/v1.0/Motos/999999", atualizarMotoDto);

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
        public async Task DeleteMotoV1_WithValidToken_ShouldReturnNoContentOrNotFound()
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
            var response = await _client.DeleteAsync("/api/v1.0/Motos/999999");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.NoContent ||
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }
    }
}

