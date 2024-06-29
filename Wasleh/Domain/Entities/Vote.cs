using System.ComponentModel.DataAnnotations;
using Wasleh.Domain.Enums;

namespace Wasleh.Domain.Entities;

public class Vote : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int EntityId { get; set; }
    public EntityType EntityType { get; set; }
    [AllowedValues([-1, 1])]
    public int Value { get; set; }
}
