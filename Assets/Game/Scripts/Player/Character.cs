using UnityEngine;

public class Character : MonoBehaviour
{
    public EntitySO baseStats;
    [HideInInspector] public EntitySO stats;

    [HideInInspector] public BattleMap battleMap;
    [HideInInspector] public CombatManager combatManager;
    [HideInInspector] public Inventory inventory;
    [HideInInspector] public AbilityFactory abilityFactory;
    
    public Health health;
    public Move move;

    public AbilityControl abilityControl;
    
    public EquipmentControl equipmentControl;
    public ModifierControl modifierControl;

    void Awake()
    {
        stats = Instantiate(baseStats); //копия данных не затрагивающая оригинал
    }
}
