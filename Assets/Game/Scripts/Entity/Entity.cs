using DG.Tweening;
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
    public BarChange meleeArmorBar;
    public BarChange magicArmorBar;
    public BarChange rangeArmorBar;
    [Space]
    [HideInInspector] public BattleMap battleMap;
    [HideInInspector] public CombatManager combatManager;
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

        meleeArmorBar.ChangeBar(baseStats.meleeArmor, currentStats.meleeArmor);
        magicArmorBar.ChangeBar(baseStats.magicArmor, currentStats.magicArmor);
        rangeArmorBar.ChangeBar(baseStats.rangeArmor, currentStats.rangeArmor);

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

    public void ChoseAbility(byte index)
    {
        Ability ability = abilityControl.abilitys[index];

        if (ability == null)
        {
            return;
        }

        if (currentStats.manna < ability.stats.mannaCost)
        {
            return;
        }

        if (ability.cooldown == null)
        {
            ability.gameObject.SetActive(true);

            ability.Prepare();
            manna.TakeManna(ability.stats.mannaCost);
            ability.cooldown = StartCoroutine(ability.Reload());

            DOTween.Sequence()
                .SetLink(gameObject)
                .Append(transform.DOScale(new Vector3(1.1f, 0.8f, 1.1f), 0.25f))
                .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
        }
    }
}
