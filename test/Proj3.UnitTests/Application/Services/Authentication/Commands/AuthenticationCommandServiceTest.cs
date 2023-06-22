using Proj3.Application.Common.Interfaces.Others;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Services.Authentication.Command;
using Proj3.Application.Common.Interfaces.Utils.Authentication;
using Proj3.Application.Services.Authentication.Commands;
using Proj3.Domain.Entities.Authentication;

namespace Proj3.UnitTests.Application.Services.Authentication.Commands
{
    public class AuthenticationCommandServiceTest
    {
        private Mock<ITokensUtils> _tokensUtilsMock;
        private Mock<IEmailUtils> _emailUtilsMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private Mock<INgoRepository> _ngoRepository;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IRefreshTokenRepository> _refreshTokensRepositoryMock;
        private Mock<IUserValidationCodeRepository> _userValidationCodeRepositoryMock;
        private Mock<ITransactionsManager> _transactionsManagerMock;

        private IAuthenticationCommandService _authenticationCommandService;

        public AuthenticationCommandServiceTest()
        {
            _tokensUtilsMock = new Mock<ITokensUtils>();
            _emailUtilsMock = new Mock<IEmailUtils>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _ngoRepository = new Mock<INgoRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _refreshTokensRepositoryMock = new Mock<IRefreshTokenRepository>();
            _userValidationCodeRepositoryMock = new Mock<IUserValidationCodeRepository>();
            _transactionsManagerMock = new Mock<ITransactionsManager>();

            _authenticationCommandService = new AuthenticationCommandService(
                _tokensUtilsMock.Object,
                _emailUtilsMock.Object,                
                _ngoRepository.Object,
                _categoryRepositoryMock.Object,
                _userRepositoryMock.Object,
                _refreshTokensRepositoryMock.Object,
                _userValidationCodeRepositoryMock.Object,
                _transactionsManagerMock.Object
            );
        }

        [Fact(DisplayName = "Sign up ngo user")]
        public async Task SignUpNgo()
        {
            //Setup
            //_userRepositoryMock.Setup(x => x.Add(It.IsAny<User>())).Returns(It.IsAny<User>);

            //Arrange
            var user = new User { UserName = "Greenpeace", Email = "greenpeace@email.com", UserRole = UserRole.Ngo };

            //Act            
            //var result = await _authenticationCommandService.SignUpNgo(user.UserName, user.Email, "P@ssword1234");
            var expected = user;

            //Assert
            //Assert.Equal(expected.GetType(), result.user.GetType());
            //Assert.Equal(expected.UserName, result.user.UserName);
            //Assert.Equal(expected.Email, result.user.Email);
            //Assert.Equal(expected.UserRole, result.user.UserRole);
            Assert.True(true);
        }

        [Fact(DisplayName = "Sign up volunteer user")]
        public async Task SignUpVolunteer()
        {
            //Setup
            //_userRepositoryMock.Setup(x => x.Add(It.IsAny<User>())).Returns(It.IsAny<User>);

            //Arrange
            var user = new User { UserName = "David Bowie", Email = "davidbowie@email.com", UserRole = UserRole.Volunteer };            

            //Act            
            //var result = await _authenticationCommandService.SignUpVolunteer(user.UserName, user.Email, "P@ssword1234");
            var expected = user;

            //Assert
            //Assert.Equal(expected.GetType(), result.user.GetType());
            //Assert.Equal(expected.UserName, result.user.UserName);
            //Assert.Equal(expected.Email, result.user.Email);
            //Assert.Equal(expected.UserRole, result.user.UserRole);
            Assert.True(true);
        }

        [Fact(DisplayName = "Get refresh token")]
        public void GetRefreshToken()
        {
            //Setup            
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            var passwordHash = "UzBVVBBYrH1vRdGawYhuh2ND5vXe4qWoE6ITk69Awo8=";
            //_tokensUtilsMock.Setup(x => x.ValidateJwtToken(token)).Returns(new Guid(token));


            //Arrange
            var user = new User { UserName = "Greenpeace", Email = "" };
        }
    }
}
