using Microsoft.EntityFrameworkCore;
using Proj3.Domain.Entities.Authentication;

namespace Proj3.IntegrationTests
{
    public class UnitTest1
    {
        public UnitTest1()
        {
            //var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "WeChangeTestDb" + DateTime.UtcNow).Options;
        }

        [Fact]
        public void Test1()
        {
            // Arrange

            //var newUserVolunteer = User.NewUserVolunteer("David Bowie", "davidbowie@email.com");
            
            // Act

            // Assert
            Assert.True(true);
        }
    }
}