namespace Proj3.Application.Common.Interfaces.Utils.Authentication
{
    public interface IEmailUtils
    {
        Task<bool> SendEmail(string email, string subject, string content);
    }
}
