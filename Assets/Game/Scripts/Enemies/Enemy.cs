using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO baseStats;
    [HideInInspector] public EnemySO stats;

    [HideInInspector] public EntityFactory entityManager;
    [HideInInspector] public CombatManager combatManager;
    [HideInInspector] public Inventory inventory;
    
    public EnHealth health;

    public ModifierControl modifierControl;

    void Awake()
    {
        stats = Instantiate(baseStats); //копия данных не затрагивающая оригинал
    }
}


