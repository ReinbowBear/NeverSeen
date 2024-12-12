using UnityEngine;

public class AbilityHud : MonoBehaviour
{
    [SerializeField] private AbilitySlot[] slots;

    public void UseAbility(byte index)
    {
        Debug.Log("прикрути функцию");
        //slots[index].onClick.Invoke();
    }
}
