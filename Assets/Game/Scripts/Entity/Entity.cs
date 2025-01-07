using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntitySO baseStats;
    [HideInInspector] public EntitySO stats;
    [Space]
    public MeshRenderer model;

    [HideInInspector] public BattleMap battleMap;
    [HideInInspector] public CombatManager combatManager;
    [HideInInspector] public Inventory inventory = new Inventory();

    public Health health;
    public Move move;

    public AbilityControl abilityControl;
    public EquipmentControl equipmentControl;
    public EffectControl modifierControl;

    void Awake()
    {
        stats = Instantiate(baseStats); //копия данных не затрагивающая оригинал
    }
}
