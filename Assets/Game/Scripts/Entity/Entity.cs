using UnityEngine;

public class Entity : MonoBehaviour
{
    [HideInInspector] public EntitySO baseStats;
    [HideInInspector] public EntitySO currentStats;
    [Space]
    public MeshFilter characterModel;
    public MeshFilter weaponModel;
    [Space]
    public BarChange hpBar;
    public BarChange mpBar;
    public BarChange armorBar;
    [Space]
    [HideInInspector] public BattleMap battleMap;
    [HideInInspector] public Inventory inventory = new Inventory();


    public Health health;
    public Manna manna;
    public Move move;

    public WeaponPoint weaponPoint = new WeaponPoint();
    public AbilityControl abilityControl = new AbilityControl();
    public EquipmentControl equipmentControl = new EquipmentControl();
    public EffectControl effectControl;


    public void Init(EntitySO newStats)
    {
        baseStats = Instantiate(newStats);
        currentStats = Instantiate(newStats);

        characterModel.mesh = currentStats.model;

        health.hpBar.ChangeBar(baseStats.health, currentStats.health);
        manna.mpBar.ChangeBar(baseStats.manna, currentStats.manna);
        armorBar.ChangeBar(baseStats.armor, currentStats.armor);

        health.character = this;
        manna.character = this;
        move.character = this;

        weaponPoint.character = this;
        abilityControl.character = this;
        equipmentControl.character = this;
    }


    public void CanAttack()
    {
        if (!baseStats.isPlayer)
        {
            EnemyLogic enemyLogic = GetComponent<EnemyLogic>();
            enemyLogic.character = this;
            enemyLogic.Init();
        }
    }
}
