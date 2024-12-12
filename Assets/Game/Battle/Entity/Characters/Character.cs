using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject hpBar;
    [HideInInspector] public MoveCharacter moveCharacter;


    void Awake()
    {
        moveCharacter = GetComponent<MoveCharacter>();
    }


    public void ChoseCharacter()
    {
        hpBar.SetActive(true);
        
        if (moveCharacter.isPathCheck != true && moveCharacter.wasMoved != true)
        {
            moveCharacter.PathCheck();
        }
    }

    public void FalseCharacter()
    {
        moveCharacter.FalsePath();
        hpBar.SetActive(false);
    }


    public void ChoseAttack(int InputKey)
    {
        //if (combatCharacter.wasAttaking != true && moveCharacter.isMove != true && activeWeapon != InputKey)
        //{
        //    FalseCharacter();
        //    activeWeapon = InputKey;
        //    combatCharacter.CharacterAttack(InputKey);
        //    hpBar.SetActive(true);
        //}
        //else
        //{
        //    combatCharacter.FalseAttack();
        //    ChoseCharacter();
        //}
    }


    private void NewTurn()
    {
        //combatCharacter.wasAttaking = false;
        moveCharacter.wasMoved = false;
    }


    void OnEnable()
    {
        //TurnManager.onPlayerTurn += NewTurn;
    }

    void OnDisable()
    {
        //TurnManager.onPlayerTurn -= NewTurn;
    }
}
