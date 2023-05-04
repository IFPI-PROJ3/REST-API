﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.NGO
{
    [Table("EventImages")]
    public class EventImage
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
