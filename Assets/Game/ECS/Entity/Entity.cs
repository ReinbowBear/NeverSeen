using System;

public struct Entity : IEquatable<Entity>
{
    public readonly int Id;

    public Entity(int id)
    {
        Id = id;
    }

    public override int GetHashCode() => Id;
    public override bool Equals(object obj) => obj is Entity entity && entity.Id == Id;
    public bool Equals(Entity other) => Id == other.Id;
}
