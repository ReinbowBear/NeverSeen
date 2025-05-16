using UnityEngine;

public class LetterComponent : MonoBehaviour
{
    public Letter Model;

    private void Update()
    {
        if (Model.Effect != null)
        {
            Model.Effect.Update();
        }
    }
}
