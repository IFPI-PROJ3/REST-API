using Proj3.Application.Common.Interfaces.Services;

namespace Proj3.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}