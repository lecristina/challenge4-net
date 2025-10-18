using Xunit;
using Microsoft.AspNetCore.Mvc;
using challenge_3_net.Controllers;
using challenge_3_net.Services.Auth;
using challenge_3_net.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace challenge_3_net.Tests.Unit
{
    /// <summary>
    /// Testes unitários para AuthController
    /// </summary>
    public class AuthControllerTests
    {
        private readonly Mock<JwtService> _mockJwtService;
        private readonly Mock<ILogger<AuthController>> _mockLogger;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockJwtService = new Mock<JwtService>(
                Mock.Of<IConfiguration>(),
                Mock.Of<ApplicationDbContext>(),
                Mock.Of<ILogger<JwtService>>()
            );
            _mockLogger = new Mock<ILogger<AuthController>>();
            _controller = new AuthController(_mockJwtService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ShouldReturnOkWithToken()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "teste@teste.com",
                Senha = "123456"
            };

            var expectedToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.test.token";
            _mockJwtService.Setup(x => x.AuthenticateAsync(loginDto.Email, loginDto.Senha))
                .ReturnsAsync(expectedToken);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<LoginResponseDto>(okResult.Value);
            Assert.Equal(expectedToken, response.Token);
            Assert.Equal("Bearer", response.TokenType);
            Assert.Equal(3600, response.ExpiresIn);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ShouldReturnUnauthorized()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "invalid@teste.com",
                Senha = "wrongpassword"
            };

            _mockJwtService.Setup(x => x.AuthenticateAsync(loginDto.Email, loginDto.Senha))
                .ReturnsAsync((string?)null);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var errorResponse = Assert.IsType<ErrorResponseDto>(unauthorizedResult.Value);
            Assert.Contains("Email ou senha incorretos", errorResponse.Message);
        }

        [Fact]
        public async Task Login_WithInvalidModel_ShouldReturnBadRequest()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "", // Email vazio
                Senha = ""  // Senha vazia
            };

            _controller.ModelState.AddModelError("Email", "Email é obrigatório");

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorResponse = Assert.IsType<ErrorResponseDto>(badRequestResult.Value);
            Assert.Contains("Dados de entrada inválidos", errorResponse.Message);
        }

        [Fact]
        public async Task ValidateToken_WithValidToken_ShouldReturnUserInfo()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "Teste Filial"),
                new Claim(ClaimTypes.Email, "teste@teste.com"),
                new Claim("perfil", "ADMIN"),
                new Claim("cnpj", "12345678000199"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
            var usuario = new Usuario
            {
                Id = 1,
                NomeFilial = "Teste Filial",
                Email = "teste@teste.com",
                Perfil = Perfil.ADMIN,
                Cnpj = "12345678000199",
                DataCriacao = DateTime.UtcNow
            };

            _mockJwtService.Setup(x => x.ValidateToken(It.IsAny<string>()))
                .Returns(principal);
            _mockJwtService.Setup(x => x.GetCurrentUserAsync(principal))
                .ReturnsAsync(usuario);

            // Simular header de autorização
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
            };
            _controller.Request.Headers.Add("Authorization", "Bearer valid.token.here");

            // Act
            var result = await _controller.ValidateToken();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var userInfo = Assert.IsType<UserInfoDto>(okResult.Value);
            Assert.Equal(1, userInfo.Id);
            Assert.Equal("Teste Filial", userInfo.NomeFilial);
            Assert.Equal("teste@teste.com", userInfo.Email);
            Assert.Equal("ADMIN", userInfo.Perfil);
        }

        [Fact]
        public async Task ValidateToken_WithInvalidToken_ShouldReturnUnauthorized()
        {
            // Arrange
            _mockJwtService.Setup(x => x.ValidateToken(It.IsAny<string>()))
                .Returns((ClaimsPrincipal?)null);

            // Simular header de autorização
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
            };
            _controller.Request.Headers.Add("Authorization", "Bearer invalid.token.here");

            // Act
            var result = await _controller.ValidateToken();

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var errorResponse = Assert.IsType<ErrorResponseDto>(unauthorizedResult.Value);
            Assert.Contains("Token inválido ou expirado", errorResponse.Message);
        }

        [Fact]
        public async Task ValidateToken_WithMissingToken_ShouldReturnUnauthorized()
        {
            // Arrange
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
            };
            // Não adicionar header de autorização

            // Act
            var result = await _controller.ValidateToken();

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var errorResponse = Assert.IsType<ErrorResponseDto>(unauthorizedResult.Value);
            Assert.Contains("Token de autorização não fornecido", errorResponse.Message);
        }

        [Fact]
        public async Task GetCurrentUser_WithValidUser_ShouldReturnUserInfo()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "Teste Filial"),
                new Claim(ClaimTypes.Email, "teste@teste.com"),
                new Claim("perfil", "ADMIN"),
                new Claim("cnpj", "12345678000199"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
            var usuario = new Usuario
            {
                Id = 1,
                NomeFilial = "Teste Filial",
                Email = "teste@teste.com",
                Perfil = Perfil.ADMIN,
                Cnpj = "12345678000199",
                DataCriacao = DateTime.UtcNow
            };

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
            };
            _controller.User = principal;

            _mockJwtService.Setup(x => x.GetCurrentUserAsync(principal))
                .ReturnsAsync(usuario);

            // Act
            var result = await _controller.GetCurrentUser();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var userInfo = Assert.IsType<UserInfoDto>(okResult.Value);
            Assert.Equal(1, userInfo.Id);
            Assert.Equal("Teste Filial", userInfo.NomeFilial);
            Assert.Contains("Admin", userInfo.Roles);
        }

        [Fact]
        public async Task GetCurrentUser_WithInvalidUser_ShouldReturnUnauthorized()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "999"), // ID inexistente
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
            };
            _controller.User = principal;

            _mockJwtService.Setup(x => x.GetCurrentUserAsync(principal))
                .ReturnsAsync((Usuario?)null);

            // Act
            var result = await _controller.GetCurrentUser();

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var errorResponse = Assert.IsType<ErrorResponseDto>(unauthorizedResult.Value);
            Assert.Contains("Usuário não autenticado", errorResponse.Message);
        }

        [Fact]
        public void CheckAdminPermission_WithAdminUser_ShouldReturnAdminPermissions()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Gerente"),
                new Claim(ClaimTypes.Role, "Operador")
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
            };
            _controller.User = principal;

            _mockJwtService.Setup(x => x.IsAdmin(principal)).Returns(true);
            _mockJwtService.Setup(x => x.IsManagerOrAdmin(principal)).Returns(true);

            // Act
            var result = _controller.CheckAdminPermission();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var permissionCheck = Assert.IsType<PermissionCheckDto>(okResult.Value);
            Assert.True(permissionCheck.IsAdmin);
            Assert.True(permissionCheck.IsManager);
            Assert.True(permissionCheck.IsOperador);
        }

        [Fact]
        public void CheckAdminPermission_WithOperadorUser_ShouldReturnOperadorPermissions()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Operador")
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
            };
            _controller.User = principal;

            _mockJwtService.Setup(x => x.IsAdmin(principal)).Returns(false);
            _mockJwtService.Setup(x => x.IsManagerOrAdmin(principal)).Returns(false);

            // Act
            var result = _controller.CheckAdminPermission();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var permissionCheck = Assert.IsType<PermissionCheckDto>(okResult.Value);
            Assert.False(permissionCheck.IsAdmin);
            Assert.False(permissionCheck.IsManager);
            Assert.True(permissionCheck.IsOperador);
        }
    }
}
