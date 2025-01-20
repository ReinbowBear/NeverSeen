
public class WeaponPoint
{
    public Entity character;

    public void SetHandWeapon(AbilitySO ability)
    {
        character.weaponModel.mesh = ability.model;
    }
}
