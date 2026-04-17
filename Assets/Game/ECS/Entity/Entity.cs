using System;

public class Entity : IEquatable<Entity>
{
    public int Id;
    public BitMask64 Mask;

    public Entity(int id)
    {
        Id = id;
        Mask = new();
    }

    public override int GetHashCode() => Id;
    public override bool Equals(object obj) => obj is Entity entity && entity.Id == Id;
    public bool Equals(Entity other) => Id == other.Id;
}
