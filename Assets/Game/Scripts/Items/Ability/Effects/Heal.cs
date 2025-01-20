
public class Heal : Effect
{
    public override void DoEffect(Entity character)
    {
        character.health.TakeHeal((int)stats.value);
    }
}
