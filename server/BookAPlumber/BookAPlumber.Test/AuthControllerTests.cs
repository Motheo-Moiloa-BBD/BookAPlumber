using BookAPlumber.Service.Interfaces;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using BookAPlumber.Core.Models.DTO;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Net;
using BookAPlumber.Core.Exceptions;

namespace BookAPlumber.Test
{
    public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly Mock<IAuthService> authServiceMock = new();
        private readonly WebApplicationFactory<Program> factory;
        private HttpClient httpClient;
        public AuthControllerTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory;

            httpClient = this.factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                    });
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddSingleton(authServiceMock.Object);
                    });
                })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async Task Register_Success()
        {
            //Arrange
            var credentials = new RegisterDTO
            {
                Username = "testUser@bookaplumber.co.za",
                Password = "testUser@123",
                Roles = ["User"]
            };

            var ExpectedUser = new IdentityUser
            {
                UserName = credentials.Username,
                Email = credentials.Username
            };

            authServiceMock.Setup(authService => authService.RegisterUser(It.IsAny<RegisterDTO>())).ReturnsAsync(ExpectedUser);

            //Act
            var response = await httpClient.PostAsJsonAsync("api/auth/register", credentials);
            
            //Assert
            response.EnsureSuccessStatusCode();
            var returnedJson = await response.Content.ReadAsStringAsync();
            var returnedUser = JsonConvert.DeserializeObject<string>(returnedJson);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public Task Register_DuplicateException_409()
        {
            return AssertThatRegisterHandlesGivenException(new DuplicateException("The username already exists."), HttpStatusCode.Conflict);
        }

        [Fact]
        public Task Register_BadRequestException_400()
        {
            return AssertThatRegisterHandlesGivenException(new BadRequestException("There was a problem when registering the user."), HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task LoginSuccess()
        {
            //Arrange
            var credentials = new LoginDTO
            {
                Username = "testUser@bookaplumber.co.za",
                Password = "testUser@123",
            };

            var expectedLoginResponse = new LoginResponseDTO
            {
                JwtToken = "TestToken"
            };

            authServiceMock.Setup(authService => authService.LoginUser(It.IsAny<LoginDTO>())).ReturnsAsync(expectedLoginResponse);

            //Act
            var response = await httpClient.PostAsJsonAsync("api/auth/login", credentials);

            //Assert
            response.EnsureSuccessStatusCode();
            var returnedJson = await response.Content.ReadAsStringAsync();
            var returnedLoginResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(returnedJson);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equivalent(expectedLoginResponse, returnedLoginResponse);
        }

        [Fact]
        public async Task Login_BadRequestException_400()
        {
            var credentials = new LoginDTO
            {
                Username = "testUser@bookaplumber.co.za",
                Password = "testUser@123",
            };

            authServiceMock.Setup(authService => authService.LoginUser(It.IsAny<LoginDTO>())).ThrowsAsync(new BadRequestException("Username or password incorrect."));

            var response = await httpClient.PostAsJsonAsync("api/auth/register", credentials);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private async Task AssertThatRegisterHandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            var credentials = new RegisterDTO
            {
                Username = "testUser@bookaplumber.co.za",
                Password = "testUser@123",
                Roles = ["User"]
            };

            authServiceMock.Setup(authService => authService.RegisterUser(It.IsAny<RegisterDTO>())).ThrowsAsync(givenException);

            var response = await httpClient.PostAsJsonAsync("api/auth/register", credentials);

            Assert.Equal(resultingStatusCode, response.StatusCode);
        }
    }
}
