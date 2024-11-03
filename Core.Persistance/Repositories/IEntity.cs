namespace Core.Persistance.Repositories;

public interface IEntity<T>
{
    T Id { get; set; }
}