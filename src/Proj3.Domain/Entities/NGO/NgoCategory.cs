using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.NGO
{
    [Table("NgoCategories")]
    public class NgoCategory
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Ngos")]
        public Guid NgoId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Categories")]
        public int CategoryId { get; set; }

        public DateTime CreatedAt { get; set; }

        public NgoCategory(Guid ngoId, int categoryId)
        {
            NgoId = ngoId;
            CategoryId = categoryId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
