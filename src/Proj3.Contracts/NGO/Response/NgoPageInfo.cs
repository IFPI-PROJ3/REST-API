using Proj3.Domain.Entities.NGO;

namespace Proj3.Contracts.NGO.Response
{
    public record NgoPageInfo(
        Ngo ngo,
        float average_rating
    );
}
