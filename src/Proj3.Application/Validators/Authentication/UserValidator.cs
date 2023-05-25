using FluentValidation;

namespace Proj3.Application.Validators.Authentication
{
    public class UserValidator : AbstractValidator<Domain.Entities.Authentication.User>
    {
        public UserValidator()
        {
            
        }
    }
}
