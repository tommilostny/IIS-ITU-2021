using System.ComponentModel.DataAnnotations;
using Fituska.Server.Entities.Interfaces;

namespace Fituska.Server.Entities;

public class EntityBase : IEntity
{
    [Key]
    public Guid Id { get; set; }
}
