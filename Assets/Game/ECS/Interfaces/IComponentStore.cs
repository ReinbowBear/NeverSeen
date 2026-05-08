
public interface IComponentStore
{
    int Count { get; }
    void AddWithCast(Entity entity, object component);
    bool Contains(Entity entity);
    void Remove(Entity entity);
}
