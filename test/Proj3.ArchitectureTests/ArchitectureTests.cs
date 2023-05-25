
using System.Reflection;

namespace Proj3.ArchitectureTests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "Proj3.Domain";        
        private const string ApplicationNamespace = "Proj3.Application";
        private const string InfrastructureNamespace = "Proj3.Infrastructure";
        private const string ContractsNamespace = "Proj3.Contracts";
        private const string WebApiNamespace = "Proj3.Api";

        [Fact(DisplayName = "Domain should not have dependency on other projects")]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var types = Types.InAssembly(Assembly.Load(DomainNamespace));

            var otherProjects = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace, 
                ContractsNamespace, 
                WebApiNamespace,
            };

            // Act            
            var result = types
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact(DisplayName = "Application should not have dependency on other projects")]
        public void Application_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                InfrastructureNamespace,                  
                WebApiNamespace,
            };

            // Act            
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact(DisplayName = "Infrastructure should not have dependency on other projects")]
        public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

            var otherProjects = new[]
            {                   
                ContractsNamespace,
                WebApiNamespace,
            };

            // Act            
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact(DisplayName = "Contracts should not have dependency on other projects")]
        public void Contracts_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange            
            var assembly = typeof(Contracts.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                WebApiNamespace,
            };

            // Act            
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact(DisplayName = "Domain should have all public classes sealed")]
        public void Domain_Should_ShouldHaveAllPublicClassesSealed()
        {
            // Arrange
            var types = Types.InAssembly(Assembly.Load(DomainNamespace));

            // Act
            var result = types
                .That()
                .AreClasses()                
                .Should()
                .BeSealed()                 
                .GetResult();

            // Assert
            Assert.True(result.IsSuccessful);            
        }
    }
}