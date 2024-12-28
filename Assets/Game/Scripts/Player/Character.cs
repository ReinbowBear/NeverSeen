using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterSO baseStats;
    [HideInInspector] public CharacterSO stats;

    [HideInInspector] public BattleMap battleMap;
    [HideInInspector] public CombatManager combatManager;
    [HideInInspector] public Inventory inventory;
    
    public Health health;
    public Move move;

    public WeaponControl weaponControl;
    public AbilityControl abilityControl;
    
    public EquipmentControl equipmentControl;
    public ModifierControl modifierControl;

    void Awake()
    {
        stats = Instantiate(baseStats); //копия данных не затрагивающая оригинал
    }
}
