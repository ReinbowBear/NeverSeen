
public class Shield : Effect
{
    public override void DoEffect(Entity character)
    {
        character.currentStats.armor += (byte)stats.value;
        character.armorBar.ChangeBar(character.baseStats.armor, character.currentStats.armor);
    }

    public override void FalseEffect(Entity character)
    {
        character.currentStats.armor -= (byte)stats.value;
        character.armorBar.ChangeBar(character.baseStats.armor, character.currentStats.armor);
    }
}
