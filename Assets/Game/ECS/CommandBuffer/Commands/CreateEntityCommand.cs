using UnityEngine;

public class CreateEntityCommand : ICommand
{
    private Entity entity;
    private GameObject obj;

    private EntityObjRegistry entityObjRegistry;
    private ComponentRegistry componentRegistry;
    private QueryRegistry queryRegistry;

    public CreateEntityCommand(Entity entity, GameObject obj,  EntityObjRegistry entityObjRegistry, ComponentRegistry componentRegistry, QueryRegistry queryRegistry)
    {
        this.entity = entity;
        this.obj = obj;

        this.entityObjRegistry = entityObjRegistry;
        this.componentRegistry = componentRegistry;
        this.queryRegistry = queryRegistry;
    }


    public void Execute()
    {
        entityObjRegistry.AddEntity(entity, obj);
        var components = obj.GetComponents<Component>();

        foreach (var comp in components)
        {
            var chunk = componentRegistry.GetStore(comp.GetType());
            chunk.AddWithCast(entity, comp);
        }

        queryRegistry.TryAddEntity(entity);
    }
}
