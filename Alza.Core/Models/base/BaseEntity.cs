namespace Alza.Core.Models;

/// <summary>
/// Base entity class.
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseEntity<T> : IEntity<T>
{
    public virtual T Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
