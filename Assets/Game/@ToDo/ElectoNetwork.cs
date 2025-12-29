using System.Collections.Generic;
using UnityEngine;

public class ElectoNetwork : MonoBehaviour
{
    public void UpdateNetwork(OnUpdateNetwork _)
    {
        List<EnergyUser> users = new ();
        Generator[] generators = new Generator[5];

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
