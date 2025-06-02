using UnityEngine;

public class LetterComponent : MonoBehaviour
{
    public Letter Model;

    private void Update() // этот скрипт проверяет наличие эффектов в тексте, и обновляет его зачем о вместо самого эффекта
    { 
        if (Model.Effect != null) 
        {
            Model.Effect.Update();
        }
    }
}

public class Letter // соотведственно это настройки что по идеи должны были бы быть в эффекте
{
    public float Speed;
    public char Character;
    public TextEffect Effect;
    public bool isActive;
}
