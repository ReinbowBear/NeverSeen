
public class AbilityControl
{
    public Entity character;
    public Ability[] abilitys = new Ability[8];

    public void AddAbility(Ability newAbility, byte index)
    {
        abilitys[index] = newAbility;
        abilitys[index].character = character;

        abilitys[index].transform.SetParent(character.weaponModel.transform);
        abilitys[index].gameObject.SetActive(false);
    }

    public void RemoveAbility(byte index)
    {
        Address.DestroyAsset(abilitys[index].gameObject);
        abilitys[index] = null;
    }
}
