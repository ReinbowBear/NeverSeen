using UnityEngine;
using Zenject;

public class Initialazer : MonoBehaviour
{
    public void Initialize()
    {
        foreach (var initializable in GetComponents<IInitializable>())
        {
            initializable.Initialize();
        }
    }
}
