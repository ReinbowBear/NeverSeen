using DG.Tweening;
using UnityEngine;

public class Move : MonoBehaviour
{
    [HideInInspector] public Entity character;
    [HideInInspector] public int pos;

    public void MoveTo(int moveValue)
    {
        Transform[] mySide = character.battleMap.points[character.baseStats.isPlayer];

        if (pos + moveValue < 0 || pos + moveValue > mySide.Length)
        {
            return;
        }

        if (mySide[pos + moveValue].childCount != 0)
        {
            Entity otherCharacter = mySide[pos + moveValue].GetComponentInChildren<Entity>();
            int otherCharacterPos;

            if (pos > otherCharacter.move.pos)
            {
                otherCharacterPos = 1;
            }
            else
            {
                otherCharacterPos = -1;
            }

            character.transform.parent = null;
            otherCharacter.move.MoveTo(otherCharacterPos);
        }

        pos += moveValue;
        AnimToPos(mySide);
    }


    private void AnimToPos(Transform[] mySide)
    {
        character.transform.SetParent(mySide[pos]);

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOMove(mySide[pos].position, 0.75f));
    }
}
