using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Fituska.Server.Entities.Interfaces;

namespace Fituska.Server.Entities
{
    public class EntityBase : IEntity
    {
        [Key]
        public Guid Id {  get; set; }
    }
}
