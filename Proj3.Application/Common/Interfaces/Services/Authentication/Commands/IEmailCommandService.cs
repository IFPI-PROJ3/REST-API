namespace Proj3.Application.Common.Interfaces.Services.Authentication.Commands
{
    public interface IEmailCommandService
    {
        void SendResetPasswordEmail(string email);
    }
}
