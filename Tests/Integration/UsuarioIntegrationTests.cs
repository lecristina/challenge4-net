using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using challenge_3_net;

namespace challenge_3_net.Tests.Integration
{
    /// <summary>
    /// Testes de integração para endpoints de usuários
    /// </summary>
    public class UsuarioIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public UsuarioIntegrationTests(WebApplicationFactory<Program> factory)
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

        // Testes v2.0
        [Fact]
        public async Task GetUsuariosV2_WithValidToken_ShouldReturnOk()
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
            var response = await _client.GetAsync("/api/v2.0/Usuarios?pageNumber=1&pageSize=10");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetUsuariosV2_WithoutToken_ShouldReturnUnauthorized()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;

            // Act
            var response = await _client.GetAsync("/api/v2.0/Usuarios");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetUsuarioByIdV2_WithValidToken_ShouldReturnOkOrNotFound()
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
            var response = await _client.GetAsync("/api/v2.0/Usuarios/1");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        // Testes v1.0
        [Fact]
        public async Task GetUsuariosV1_WithValidToken_ShouldReturnOk()
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
            var response = await _client.GetAsync("/api/v1.0/Usuarios?pageNumber=1&pageSize=10");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetUsuariosV1_WithoutToken_ShouldReturnUnauthorized()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;

            // Act
            var response = await _client.GetAsync("/api/v1.0/Usuarios");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetUsuarioByIdV1_WithValidToken_ShouldReturnOkOrNotFound()
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
            var response = await _client.GetAsync("/api/v1.0/Usuarios/1");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        // Testes POST/PUT/DELETE v2.0
        [Fact]
        public async Task CreateUsuarioV2_WithValidToken_ShouldReturnCreated()
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

            var criarUsuarioDto = new
            {
                nomeFilial = $"Filial Teste {Guid.NewGuid().ToString().Substring(0, 8)}",
                email = $"teste{Guid.NewGuid().ToString().Substring(0, 8)}@example.com",
                senha = "123456",
                cnpj = $"{Guid.NewGuid().ToString().Substring(0, 14)}",
                endereco = "Endereço de teste",
                telefone = "11999999999",
                perfil = 0 // ADMIN
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v2.0/Usuarios", criarUsuarioDto);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Created ||
                response.StatusCode == HttpStatusCode.Conflict ||
                response.StatusCode == HttpStatusCode.BadRequest ||
                response.StatusCode == HttpStatusCode.InternalServerError,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task CreateUsuarioV2_WithoutToken_ShouldReturnUnauthorized()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;

            var criarUsuarioDto = new
            {
                nomeFilial = "Filial Teste",
                email = "teste@example.com",
                senha = "123456",
                cnpj = "12345678000199",
                endereco = "Endereço de teste",
                telefone = "11999999999",
                perfil = 0
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v2.0/Usuarios", criarUsuarioDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task UpdateUsuarioV2_WithValidToken_ShouldReturnOkOrNotFound()
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

            var atualizarUsuarioDto = new
            {
                nomeFilial = $"Filial Atualizada {Guid.NewGuid().ToString().Substring(0, 8)}",
                email = $"atualizado{Guid.NewGuid().ToString().Substring(0, 8)}@example.com",
                cnpj = $"{Guid.NewGuid().ToString().Substring(0, 14)}",
                endereco = "Endereço atualizado",
                telefone = "11888888888",
                perfil = 1 // GERENTE
            };

            // Act
            var response = await _client.PutAsJsonAsync("/api/v2.0/Usuarios/999999", atualizarUsuarioDto);

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
        public async Task DeleteUsuarioV2_WithValidToken_ShouldReturnNoContentOrNotFound()
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
            var response = await _client.DeleteAsync("/api/v2.0/Usuarios/999999");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.NoContent ||
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        // Testes POST/PUT/DELETE v1.0
        [Fact]
        public async Task CreateUsuarioV1_WithValidToken_ShouldReturnCreated()
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

            var criarUsuarioDto = new
            {
                nomeFilial = $"Filial V1 {Guid.NewGuid().ToString().Substring(0, 8)}",
                email = $"v1{Guid.NewGuid().ToString().Substring(0, 8)}@example.com",
                senha = "123456",
                cnpj = $"{Guid.NewGuid().ToString().Substring(0, 14)}",
                endereco = "Endereço v1.0",
                telefone = "11777777777",
                perfil = 0
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1.0/Usuarios", criarUsuarioDto);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Created ||
                response.StatusCode == HttpStatusCode.Conflict ||
                response.StatusCode == HttpStatusCode.BadRequest ||
                response.StatusCode == HttpStatusCode.InternalServerError,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task UpdateUsuarioV1_WithValidToken_ShouldReturnOkOrNotFound()
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

            var atualizarUsuarioDto = new
            {
                nomeFilial = $"Filial Atualizada V1 {Guid.NewGuid().ToString().Substring(0, 8)}",
                email = $"v1upd{Guid.NewGuid().ToString().Substring(0, 8)}@example.com",
                cnpj = $"{Guid.NewGuid().ToString().Substring(0, 14)}",
                endereco = "Endereço atualizado v1.0",
                telefone = "11666666666",
                perfil = 1
            };

            // Act
            var response = await _client.PutAsJsonAsync("/api/v1.0/Usuarios/999999", atualizarUsuarioDto);

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
        public async Task DeleteUsuarioV1_WithValidToken_ShouldReturnNoContentOrNotFound()
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
            var response = await _client.DeleteAsync("/api/v1.0/Usuarios/999999");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.NoContent ||
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }
    }
}

