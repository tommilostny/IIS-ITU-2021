using System.ComponentModel.DataAnnotations;
using Fituska.DAL.Entities.Interfaces;

namespace Fituska.DAL.Entities;

public class EntityBase : IEntity
{
    [Key]
    public Guid Id { get; set; }
}
