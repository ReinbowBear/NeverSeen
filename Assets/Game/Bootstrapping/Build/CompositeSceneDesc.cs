
public class CompositeSceneDesc
{
    public readonly IModule[] Descriptors;

    public CompositeSceneDesc(params IModule[] descriptors)
    {
        Descriptors = descriptors;
    }
}
