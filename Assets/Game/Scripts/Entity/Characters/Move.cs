using DG.Tweening;
using UnityEngine;

public class Move : MonoBehaviour
{
    [HideInInspector] public Entity character;
    [HideInInspector] public int myPos;

    public void MoveForward()
    {
        if (myPos == 0)
        {
            return;
        }

        if (character.battleMap.points[character.baseStats.isPlayer][myPos-1].childCount == 0)
        {
            myPos--;
            DoMove(myPos);
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
            DoMove(myPos);
        }
    }


    private void DoMove(int pos)
    {
        character.transform.SetParent(character.battleMap.points[character.baseStats.isPlayer][myPos]);

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOMove(character.battleMap.points[character.baseStats.isPlayer][myPos].position, 0.75f));
    }
}
