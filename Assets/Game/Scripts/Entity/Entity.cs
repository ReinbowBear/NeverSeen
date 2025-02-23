using UnityEngine;

public class Entity : MonoBehaviour
{
    [HideInInspector] public CharacterSO baseStats;
    [HideInInspector] public CharacterSO currentStats;
    [Space]
    public MeshFilter characterModel;
    public MeshFilter weaponModel;
    [Space]
    public BarChange hpBar;
    public BarChange armorBar;
    [Space]
    [HideInInspector] public BattleMap battleMap;
    [HideInInspector] public Inventory inventory = new Inventory();
    [HideInInspector] public ShowInventory inventoryUI;


    public Health health;
    public Move move;

    public WeaponPoint weaponPoint = new WeaponPoint();
    public WeaponControl abilityControl = new WeaponControl();
    public EquipmentControl equipmentControl = new EquipmentControl();
    public EffectControl effectControl;


    public void Init(CharacterSO newStats)
    {
        baseStats = Instantiate(newStats);
        currentStats = Instantiate(newStats);

        characterModel.mesh = currentStats.model;

        health.hpBar.ChangeBar(baseStats.health, currentStats.health);
        armorBar.ChangeBar(baseStats.armor, currentStats.armor);

        health.character = this;
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
