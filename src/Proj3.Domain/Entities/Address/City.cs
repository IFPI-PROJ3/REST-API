using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.Address 
{
    [Table("Cities")]
    public class City
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("States")]
        public int StateId { get; set; }

        public string IbgeCode { get; set; }

        public City()
        {            
        }
    }
}
