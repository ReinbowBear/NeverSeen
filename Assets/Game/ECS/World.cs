using UnityEngine;

public class World
{
    private EntityRegistry entityRegistry;
    private EntityObjRegistry objectRegistry;
    private MaskRegistry maskRegistry;

    private TypeRegistry typeRegistry;
    private StoreRegistry storeRegistry;

    public World()
    {
        entityRegistry = new();
        objectRegistry = new();
        maskRegistry = new();

        typeRegistry = new();
        storeRegistry = new();
    }


    #region CreateEntity
    public Entity CreateEntity()
    {
        var entity = entityRegistry.CreateEntity();
        maskRegistry.EnsureCapacity(entity.Id);
        return entity;
    }

    public Entity CreateEntity(GameObject obj)
    {
        var entity = entityRegistry.CreateEntity();
        var components = obj.GetComponents<Component>();

        maskRegistry.EnsureCapacity(entity.Id);
        objectRegistry.AddEntity(entity, obj);

        ref var mask = ref maskRegistry.GetMask(entity.Id);

        foreach (var comp in components)
        {
            var type = comp.GetType();
            var compIndex = typeRegistry.GetIndex(type);
            var chunk = storeRegistry.GetOrCreateStore(compIndex, type);

            mask.Add(compIndex);
            chunk.AddWithCast(entity, comp);
        }

        return entity;
    }

    public void DestroyEntity(Entity entity)
    {
        ref var mask = ref maskRegistry.GetMask(entity.Id);

        foreach(var compIndex in mask.GetSetBits())
        {
            var chunk = storeRegistry.GetStore(compIndex);
            chunk.Remove(entity);
            mask.Remove(compIndex);
        }

        objectRegistry.RemoveEntity(entity);
        entityRegistry.RemoveEntity(entity);
    }
    #endregion


    #region AddComponent
    public void AddComponent<T>(Entity entity, T component)
    {
        ref var mask = ref maskRegistry.GetMask(entity.Id);
        var compIndex = typeRegistry.GetIndex<T>();
        var chunk = storeRegistry.GetOrCreateStore<T>(compIndex);

        mask.Add(compIndex);
        chunk.Add(entity, component);
    }

    public void RemoveComponent<T>(Entity entity)
    {
        ref var mask = ref maskRegistry.GetMask(entity.Id);
        var compIndex = typeRegistry.GetIndex<T>();
        var chunk = storeRegistry.GetStore(compIndex);

        mask.Remove(compIndex);
        chunk.Remove(entity);

        if (mask.IsZero())
        {
            objectRegistry.RemoveEntity(entity);
            entityRegistry.RemoveEntity(entity);
        }
    }
    #endregion


    #region Get
    public bool Has<T>()
    {
        var compIndex = typeRegistry.GetIndex<T>();
        var chunk = storeRegistry.GetOrCreateStore<T>(compIndex);
        return chunk.Count > 0;
    }

    public T GetComponent<T>(Entity entity)
    {
        var compIndex = typeRegistry.GetIndex<T>();
        var chunk = storeRegistry.GetOrCreateStore<T>(compIndex);
        return chunk.GetComponent(entity);
    }


    public Entity GetEntity(GameObject obj)
    {
        return objectRegistry.GetEntity(obj);
    }

    public GameObject GetObject(Entity entity)
    {
        return objectRegistry.GetObject(entity);
    }
    #endregion


    #region Query
    public Query<T1> Query<T1>()
    {
        var compIndex1 = typeRegistry.GetIndex(typeof(T1));

        var executor = new Query<T1>()
        {
            TypeRegistry = typeRegistry,    
            MaskRegistry = maskRegistry,        
            Store1 = storeRegistry.GetOrCreateStore<T1>(compIndex1),
        };

        return executor;
    }

    public Query<T1, T2> Query<T1, T2>()
    {
        var compIndex1 = typeRegistry.GetIndex(typeof(T1));
        var compIndex2 = typeRegistry.GetIndex(typeof(T2));

        var executor = new Query<T1, T2>()
        {
            TypeRegistry = typeRegistry,
            MaskRegistry = maskRegistry,
            Store1 = storeRegistry.GetOrCreateStore<T1>(compIndex1),
            Store2 = storeRegistry.GetOrCreateStore<T2>(compIndex2),
        };

        return executor;
    }

    public Query<T1, T2, T3> Query<T1, T2, T3>()
    {
        var compIndex1 = typeRegistry.GetIndex(typeof(T1));
        var compIndex2 = typeRegistry.GetIndex(typeof(T2));
        var compIndex3 = typeRegistry.GetIndex(typeof(T3));

        var executor = new Query<T1, T2, T3>()
        {
            TypeRegistry = typeRegistry,
            MaskRegistry = maskRegistry,
            Store1 = storeRegistry.GetOrCreateStore<T1>(compIndex1),
            Store2 = storeRegistry.GetOrCreateStore<T2>(compIndex2),
            Store3 = storeRegistry.GetOrCreateStore<T3>(compIndex3),
        };

        return executor;
    }

    public Query<T1, T2, T3, T4> Query<T1, T2, T3, T4>()
    {
        var compIndex1 = typeRegistry.GetIndex(typeof(T1));
        var compIndex2 = typeRegistry.GetIndex(typeof(T2));
        var compIndex3 = typeRegistry.GetIndex(typeof(T3));
        var compIndex4 = typeRegistry.GetIndex(typeof(T4));

        var executor = new Query<T1, T2, T3, T4>()
        {
            TypeRegistry = typeRegistry,
            MaskRegistry = maskRegistry,
            Store1 = storeRegistry.GetOrCreateStore<T1>(compIndex1),
            Store2 = storeRegistry.GetOrCreateStore<T2>(compIndex2),
            Store3 = storeRegistry.GetOrCreateStore<T3>(compIndex3),
            Store4 = storeRegistry.GetOrCreateStore<T4>(compIndex4),
        };

        return executor;
    }
    #endregion


    #region Unility
    public void Clear()
    {
        objectRegistry.Clear();
        entityRegistry.Clear();

        storeRegistry.Clear();
    }
    #endregion
}
