
public class Poison : Effect
{
    public override void DoEffect(Entity character)
    {
        character.health.TakeDamage((int)stats.value, DamageType.magic);
    }
}
