using Fituska.DAL.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Fituska.DAL.Entities;

public abstract class EntityBase : IEntity
{
    [Key]
    public Guid Id { get; set; }
}
