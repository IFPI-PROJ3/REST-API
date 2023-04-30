using Proj3.Application.Common.Errors.Authentication;
using Proj3.Application.Common.Interfaces.Services.Authentication.Commands;
using Proj3.Application.Common.Interfaces.Utils.Authentication;
using Proj3.Application.Utils.Authentication;

namespace Proj3.Application.Services.Authentication.Commands
{
    public class EmailCommandService : IEmailCommandService
    {
        private readonly IEmailUtils _emailUtils;

        public EmailCommandService(IEmailUtils emailUtils)
        {
            _emailUtils = emailUtils;
        }

        public void SendResetPasswordEmail(string email)
        {
            if (!Validation.IsValidEmail(email))
            {
                throw new InvalidEmailException();
            }

            _emailUtils.SendEmail(email, "Redefinição de senha", "");
        }
    }
}
