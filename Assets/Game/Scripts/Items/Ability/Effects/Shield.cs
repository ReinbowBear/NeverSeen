
public class Shield : Effect
{
    public override void DoEffect(Entity character)
    {
        character.currentStats.magicArmor += (byte)stats.value;
        character.magicArmorBar.ChangeBar(character.baseStats.magicArmor, character.currentStats.magicArmor);
    }

    public override void FalseEffect(Entity character)
    {
        character.currentStats.magicArmor -= (byte)stats.value;
        character.magicArmorBar.ChangeBar(character.baseStats.magicArmor, character.currentStats.magicArmor);
    }
}
