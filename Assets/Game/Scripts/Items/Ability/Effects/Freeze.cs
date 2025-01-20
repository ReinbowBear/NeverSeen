
public class Freeze : Effect
{
    public override void DoEffect(Entity character)
    {
        character.currentStats.mannaRegen -= stats.value;
    }

    public override void FalseEffect(Entity character)
    {
        character.currentStats.mannaRegen += stats.value;
    }
}
