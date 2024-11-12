namespace Alza.Core.Models;

public class BaseEntity<T> : IEntity<T>
{
    public virtual T Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
