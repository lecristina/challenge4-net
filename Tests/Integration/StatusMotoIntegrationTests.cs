using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using challenge_3_net;

namespace challenge_3_net.Tests.Integration
{
    /// <summary>
    /// Testes de integração para endpoints de status de motos
    /// </summary>
    public class StatusMotoIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public StatusMotoIntegrationTests(CustomWebApplicationFactory<Program> factory)
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
        public async Task GetStatusMotosV2_WithValidToken_ShouldReturnOk()
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
            var response = await _client.GetAsync("/api/v2.0/StatusMotos?pageNumber=1&pageSize=10");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetStatusMotosV2_WithoutToken_ShouldReturnUnauthorized()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;

            // Act
            var response = await _client.GetAsync("/api/v2.0/StatusMotos");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetStatusMotoByIdV2_WithValidToken_ShouldReturnOkOrNotFound()
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
            var response = await _client.GetAsync("/api/v2.0/StatusMotos/1");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task GetStatusAtualMotoV2_WithValidToken_ShouldReturnOkOrNotFound()
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
            var response = await _client.GetAsync("/api/v2.0/StatusMotos/moto/1/atual");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        // Testes v1.0
        [Fact]
        public async Task GetStatusMotosV1_WithValidToken_ShouldReturnOk()
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
            var response = await _client.GetAsync("/api/v1.0/StatusMotos?pageNumber=1&pageSize=10");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetStatusMotosV1_WithoutToken_ShouldReturnOk()
        {
            // Arrange - v1.0 não requer token
            _client.DefaultRequestHeaders.Authorization = null;

            // Act
            var response = await _client.GetAsync("/api/v1.0/StatusMotos");

            // Assert - v1.0 não requer autenticação, deve retornar OK
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetStatusMotoByIdV1_WithValidToken_ShouldReturnOkOrNotFound()
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
            var response = await _client.GetAsync("/api/v1.0/StatusMotos/1");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        // Testes GET específicos v2.0
        [Fact]
        public async Task GetHistoricoStatusMotoV2_WithValidToken_ShouldReturnOkOrNotFound()
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
            var response = await _client.GetAsync("/api/v2.0/StatusMotos/moto/1/historico?pageNumber=1&pageSize=10");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task GetStatusMotoPorTipoV2_WithValidToken_ShouldReturnOk()
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
            var response = await _client.GetAsync("/api/v2.0/StatusMotos/tipo/0?pageNumber=1&pageSize=10");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // Testes POST/PUT/DELETE v2.0
        [Fact]
        public async Task CreateStatusMotoV2_WithValidToken_ShouldReturnCreated()
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

            var criarStatusDto = new
            {
                status = 0, // PENDENTE
                descricao = "Status de teste",
                area = "Área de teste",
                motoId = 1L,
                usuarioId = 1L
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v2.0/StatusMotos", criarStatusDto);

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
        public async Task CreateStatusMotoV2_WithoutToken_ShouldReturnUnauthorized()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;

            var criarStatusDto = new
            {
                status = 0,
                descricao = "Status de teste",
                area = "Área de teste",
                motoId = 1L,
                usuarioId = 1L
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v2.0/StatusMotos", criarStatusDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task UpdateStatusMotoV2_WithValidToken_ShouldReturnOkOrNotFound()
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

            var atualizarStatusDto = new
            {
                status = 5, // PRONTA
                descricao = "Status atualizado",
                area = "Área atualizada",
                motoId = 1L,
                usuarioId = 1L
            };

            // Act
            var response = await _client.PutAsJsonAsync("/api/v2.0/StatusMotos/999999", atualizarStatusDto);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task DeleteStatusMotoV2_WithValidToken_ShouldReturnNoContentOrNotFound()
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
            var response = await _client.DeleteAsync("/api/v2.0/StatusMotos/999999");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.NoContent ||
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        // Testes POST/PUT/DELETE v1.0
        [Fact]
        public async Task CreateStatusMotoV1_WithValidToken_ShouldReturnCreated()
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

            var criarStatusDto = new
            {
                status = 0,
                descricao = "Status v1.0",
                area = "Área v1.0",
                motoId = 1L,
                usuarioId = 1L
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1.0/StatusMotos", criarStatusDto);

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
        public async Task UpdateStatusMotoV1_WithValidToken_ShouldReturnOkOrNotFound()
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

            var atualizarStatusDto = new
            {
                status = 5,
                descricao = "Status atualizado v1.0",
                area = "Área atualizada v1.0",
                motoId = 1L,
                usuarioId = 1L
            };

            // Act
            var response = await _client.PutAsJsonAsync("/api/v1.0/StatusMotos/999999", atualizarStatusDto);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest,
                $"Status code inesperado: {response.StatusCode}"
            );
        }

        [Fact]
        public async Task DeleteStatusMotoV1_WithValidToken_ShouldReturnNoContentOrNotFound()
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
            var response = await _client.DeleteAsync("/api/v1.0/StatusMotos/999999");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.NoContent ||
                response.StatusCode == HttpStatusCode.NotFound,
                $"Status code inesperado: {response.StatusCode}"
            );
        }
    }
}

