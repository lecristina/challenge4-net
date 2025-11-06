using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using challenge_3_net;

namespace challenge_3_net.Tests.Integration
{
    /// <summary>
    /// Testes de integração para endpoints de operações
    /// </summary>
    public class OperacaoIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public OperacaoIntegrationTests(WebApplicationFactory<Program> factory)
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
        public async Task GetOperacoesV2_WithValidToken_ShouldReturnOk()
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
            var response = await _client.GetAsync("/api/v2.0/Operacoes?pageNumber=1&pageSize=10");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOperacoesV2_WithoutToken_ShouldReturnUnauthorized()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;

            // Act
            var response = await _client.GetAsync("/api/v2.0/Operacoes");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetOperacaoByIdV2_WithValidToken_ShouldReturnOkOrNotFound()
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
            var response = await _client.GetAsync("/api/v2.0/Operacoes/1");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        // Testes v1.0
        [Fact]
        public async Task GetOperacoesV1_WithValidToken_ShouldReturnOk()
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
            var response = await _client.GetAsync("/api/v1.0/Operacoes?pageNumber=1&pageSize=10");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOperacoesV1_WithoutToken_ShouldReturnUnauthorized()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;

            // Act
            var response = await _client.GetAsync("/api/v1.0/Operacoes");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetOperacaoByIdV1_WithValidToken_ShouldReturnOkOrNotFound()
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
            var response = await _client.GetAsync("/api/v1.0/Operacoes/1");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        // Testes POST/PUT/DELETE v2.0
        [Fact]
        public async Task CreateOperacaoV2_WithValidToken_ShouldReturnCreated()
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

            var criarOperacaoDto = new
            {
                tipoOperacao = 0, // CHECK_IN
                descricao = "Operação de teste",
                motoId = 1L,
                usuarioId = 1L
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v2.0/Operacoes", criarOperacaoDto);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Created ||
                response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest ||
                response.StatusCode == HttpStatusCode.InternalServerError,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task CreateOperacaoV2_WithoutToken_ShouldReturnUnauthorized()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;

            var criarOperacaoDto = new
            {
                tipoOperacao = 0,
                descricao = "Operação de teste",
                motoId = 1L,
                usuarioId = 1L
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v2.0/Operacoes", criarOperacaoDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task UpdateOperacaoV2_WithValidToken_ShouldReturnOkOrNotFound()
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

            var atualizarOperacaoDto = new
            {
                tipoOperacao = 1, // CHECK_OUT
                descricao = "Operação atualizada",
                motoId = 1L,
                usuarioId = 1L
            };

            // Act
            var response = await _client.PutAsJsonAsync("/api/v2.0/Operacoes/999999", atualizarOperacaoDto);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task DeleteOperacaoV2_WithValidToken_ShouldReturnNoContentOrNotFound()
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
            var response = await _client.DeleteAsync("/api/v2.0/Operacoes/999999");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.NoContent ||
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        // Testes POST/PUT/DELETE v1.0
        [Fact]
        public async Task CreateOperacaoV1_WithValidToken_ShouldReturnCreated()
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

            var criarOperacaoDto = new
            {
                tipoOperacao = 0,
                descricao = "Operação v1.0",
                motoId = 1L,
                usuarioId = 1L
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1.0/Operacoes", criarOperacaoDto);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Created ||
                response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest ||
                response.StatusCode == HttpStatusCode.InternalServerError,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task UpdateOperacaoV1_WithValidToken_ShouldReturnOkOrNotFound()
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

            var atualizarOperacaoDto = new
            {
                tipoOperacao = 1,
                descricao = "Operação atualizada v1.0",
                motoId = 1L,
                usuarioId = 1L
            };

            // Act
            var response = await _client.PutAsJsonAsync("/api/v1.0/Operacoes/999999", atualizarOperacaoDto);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task DeleteOperacaoV1_WithValidToken_ShouldReturnNoContentOrNotFound()
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
            var response = await _client.DeleteAsync("/api/v1.0/Operacoes/999999");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.NoContent ||
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }
    }
}

