using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character instance;

    public Health health;
    public Move move;
    public Inventory inventory;
    public ActionShow actionShow;
    [Space]
    public Stats stats;


    [HideInInspector] public State state;
    private int previousWeapon;

    void Awake()
    {
        instance = this;
    }

    public void TryAttack(int index, Vector3 targetPos)
    {
        Ability weapon = inventory.weapons[index];

        if (weapon == null)
        {
            return;
        }

        if (state == State.attack && index != previousWeapon) // кансел атаки другой способностью
        {
            StopAllCoroutines();
            inventory.weapons[previousWeapon].Cancel(this);
        }
        else if (state == State.attack)
        {
            return;
        }

        previousWeapon = index;

        StartCoroutine(move.RotateTo(targetPos, weapon.prepare));
        weapon.StartCoroutine(weapon.Use(this));
        weapon.StartCoroutine(weapon.ShowAttack(this));
    }
}

[System.Serializable]
public class Stats
{
    public short maxHp;
    private short hp;

    public float speed;
    public float rotate;
}

public enum State
{
    None,
    attack,
    move,
    stunn,
}
