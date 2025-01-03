using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Entity character;
    private int myPos;

    public void MoveForward()
    {
        if (myPos == 0)
        {
            return;
        }

        if (character.battleMap.points[character.baseStats.isPlayer][myPos-1].childCount == 0)
        {
            myPos--;
            MoveAction(myPos);
        }
    }

    public void MoveBack()
    {
        if (myPos == character.battleMap.points[character.baseStats.isPlayer].Length)
        {
            return;
        }

        if (character.battleMap.points[character.baseStats.isPlayer][myPos+1].childCount == 0)
        {
            myPos++;
            MoveAction(myPos);
        }
    }


    private void MoveAction(int pos)
    {
        character.transform.position = character.battleMap.points[character.baseStats.isPlayer][myPos].position;
        character.transform.SetParent(character.battleMap.points[character.baseStats.isPlayer][myPos]);
    }
}
