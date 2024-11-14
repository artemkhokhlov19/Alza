namespace Alza.Core.Models;

public interface IEntity<T>
{
    new T Id { get; set; }
}
