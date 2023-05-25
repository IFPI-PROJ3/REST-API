using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.Address
{
    [Table("States")]
    public class State
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string UF { get; set; }

        public string IbgeCode { get; set; }

        public string DDD { get; set; }

        public State()
        {            
        }
    }
}
