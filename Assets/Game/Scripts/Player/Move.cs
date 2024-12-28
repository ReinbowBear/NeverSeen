using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Character character;
    private int myPos;

    public void MoveForward()
    {
        int newPos = myPos - 1;
        if (newPos > 0)
        {
            return;
        }

        if (character.battleMap.CharacterPoints[newPos].childCount == 0)
        {
            myPos = newPos;
            MoveAction(newPos);
        }
    }

    public void MoveTo(int pos)
    {

    }


    private void MoveAction(int pos)
    {
        character.transform.position = character.battleMap.CharacterPoints[pos].position;
    }
}
