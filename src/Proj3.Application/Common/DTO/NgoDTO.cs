using Proj3.Domain.Entities.Common;

namespace Proj3.Application.Common.DTO
{
    public class NgoDTO
    {        
        public Guid id { get; set; } 
        public Guid user_id { get; set; }
        public string name { get; set; } = null!;
        public string description { get; set; } = null!;
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }        
        public List<Category>? categories { get; set; }
    }
}
