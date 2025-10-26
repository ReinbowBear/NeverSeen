using UnityEngine;
using Zenject;

public class ElectoNetwork : MonoBehaviour
{
    [Inject] EntityRegistry world;

    public void UpdateNetwork(OnUpdateNetwork _)
    {
        var users = world.GetEntityWithComponents<EnergyUser>();
        var generators = world.GetEntityWithComponents<Generator>();

        foreach (var generator in generators)
        {
            generator.SetActive(true);
        }

        foreach (var generator in generators)
        {
            foreach (var conect in generator.connections)
            {
                if (conect is EnergyUser && users.Contains(conect as EnergyUser) && !generator.connections.Contains(conect))
                {
                    conect.SetActive(false);
                }
            }
        }
    }
}
