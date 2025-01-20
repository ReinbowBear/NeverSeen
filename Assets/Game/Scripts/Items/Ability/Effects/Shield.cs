
public class Shield : Effect
{
    public override void DoEffect(Entity character)
    {
        character.currentStats.magicArmor += (byte)stats.value;
    }

    public override void FalseEffect(Entity character)
    {
        character.currentStats.magicArmor -= (byte)stats.value;
    }
}
