using UnityEngine;

public class StartCam : MonoBehaviour
{
    void Start()
    {
        float offsetZ = MapGenerator.Instance.mapRadius / 2;
        float offsetX = MapGenerator.Instance.mapRadius / 2;

        transform.position = new Vector3(offsetX, 0, offsetZ);
    }
}
