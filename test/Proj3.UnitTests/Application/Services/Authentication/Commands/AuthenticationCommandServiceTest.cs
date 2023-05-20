using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Services.Authentication.Command;
using Proj3.Application.Common.Interfaces.Utils.Authentication;
using Proj3.Application.Services.Authentication.Commands;
using Proj3.Domain.Entities.Authentication;
using System.Reflection;

namespace Proj3.UnitTests.Application.Services.Authentication.Commands
{
    public class AuthenticationCommandServiceTest
    {
        private Mock<ITokensUtils> _tokensUtilsMock;
        private Mock<IEmailUtils> _emailUtilsMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IRefreshTokenRepository> _refreshTokensRepositoryMock;
        private Mock<IUserValidationCodeRepository> _userValidationCodeRepositoryMock;

        private IAuthenticationCommandService _authenticationCommandService;

        public AuthenticationCommandServiceTest()
        {
            _tokensUtilsMock = new Mock<ITokensUtils>();
            _emailUtilsMock = new Mock<IEmailUtils>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _refreshTokensRepositoryMock = new Mock<IRefreshTokenRepository>();
            _userValidationCodeRepositoryMock = new Mock<IUserValidationCodeRepository>();

            _authenticationCommandService = new AuthenticationCommandService(
                _tokensUtilsMock.Object,
                _emailUtilsMock.Object,
                _userRepositoryMock.Object,
                _refreshTokensRepositoryMock.Object,
                _userValidationCodeRepositoryMock.Object
            );
        }

        [Fact(DisplayName = "Sign up ngo user")]
        public async Task SignUpNgo()
        {
            //Setup
            _userRepositoryMock.Setup(x => x.Add(It.IsAny<User>())).Returns(Task.FromResult(It.IsAny<User>));

            //Arrange
            var user = new User { UserName = "Greenpeace", Email = "greenpeace@email.com", UserRole = UserRole.Ngo };

            //Act            
            var result = await _authenticationCommandService.SignUpNgo(user.UserName, user.Email, "P@ssword1234");
            var expected = user;

            //Assert
            Assert.Equal(expected.GetType(), result.user.GetType());
            Assert.Equal(expected.UserName, result.user.UserName);
            Assert.Equal(expected.Email, result.user.Email);
            Assert.Equal(expected.UserRole, result.user.UserRole);
        }

        [Fact(DisplayName = "Sign up volunteer user")]
        public async Task SignUpVolunteer()
        {
            //Setup
            _userRepositoryMock.Setup(x => x.Add(It.IsAny<User>())).Returns(Task.FromResult(It.IsAny<User>));

            //Arrange
            var user = new User { UserName = "David Bowie", Email = "davidbowie@email.com", UserRole = UserRole.Volunteer };            

            //Act            
            var result = await _authenticationCommandService.SignUpVolunteer(user.UserName, user.Email, "P@ssword1234");
            var expected = user;

            //Assert
            Assert.Equal(expected.GetType(), result.user.GetType());
            Assert.Equal(expected.UserName, result.user.UserName);
            Assert.Equal(expected.Email, result.user.Email);
            Assert.Equal(expected.UserRole, result.user.UserRole);
        }
    }
}
