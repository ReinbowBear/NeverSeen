using UnityEngine;

public class EcsBootstrap : MonoBehaviour
{
    public ISystem[] Awake;

    public ISystem[] Init;
    public ISystem[] Logic;
    public ISystem[] View;

    public ISystem[] Enter;
    public ISystem[] Exit;

    private World world = new();

    void Start()
    {
        
    }


    void Update()
    {
        
    }


    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }
}
