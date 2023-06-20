using Proj3.Domain.Entities.Common;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Common.DTO
{
    public class NgoDTO
    {        
        public Guid Id { get; set; } = Guid.NewGuid();        
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }        
        public List<Category>? Categories { get; set; }
    }
}
