
public class CompositeSceneDesc
{
    public readonly ISystemGroupDesc[] Descriptors;

    public CompositeSceneDesc(params ISystemGroupDesc[] descriptors)
    {
        Descriptors = descriptors;
    }
}
