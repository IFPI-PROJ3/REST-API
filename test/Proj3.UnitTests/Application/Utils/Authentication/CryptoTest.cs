using Proj3.Application.Utils.Authentication;

namespace Proj3.UnitTests.Application.Utils.Authentication
{
    public class CryptoTest
    {        
        [Fact(DisplayName = "Get random salt")]
        public void GetSalt()
        {
            // Arrange
            var salt = Crypto.GetSalt;

            // Act
            var result = Crypto.GetSalt;

            // Assert
            Assert.NotEqual(salt, result);
        }

        [Fact(DisplayName = "Password hashing")]
        public void PasswordHashing()
        {
            // Arrange
            var password = "P@ssword#69";
            var salt = Crypto.GetSalt;

            var user = new Domain.Entities.Authentication.User
            {
                Email = "user@email.com",
                Salt = salt
            };

            // Act            
            var result = Crypto.ReturnUserHash(user, password);
            var expected = Crypto.ReturnUserHash(new Domain.Entities.Authentication.User { Email = "user@email.com", Salt = salt}, password);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
