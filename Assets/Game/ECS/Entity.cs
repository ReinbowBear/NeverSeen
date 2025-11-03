using System;

public struct Entity : IEquatable<Entity>
{
    public readonly int Id;
    public ulong ComponentMask;

    public Entity(int id)
    {
        Id = id;
        ComponentMask = 0;
    }


    public override int GetHashCode() => Id;
    public override bool Equals(object obj) => obj is Entity entity && entity.Id == Id;
    public bool Equals(Entity other) => Id == other.Id;


    public void AddComponentBit(int bitIndex)
    {
        ComponentMask |= 1UL << bitIndex;
    }

    public void RemoveComponentBit(int bitIndex)
    {
        ComponentMask &= ~(1UL << bitIndex);
    }

    public bool HasComponents(ulong mask)
    {
        return (ComponentMask & mask) == mask;
    }
}
