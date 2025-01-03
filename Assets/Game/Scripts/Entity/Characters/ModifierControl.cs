using UnityEngine;

public class ModifierControl : MonoBehaviour
{
    [SerializeField] private Entity myCharacter;
    private Modifier modifier;

    public void AddModifier(Modifier newModifier)
    {
        if (modifier != null)
        {
            modifier.Deactivate();
        }

        modifier = newModifier;
        modifier.Active(myCharacter);
    }
}
