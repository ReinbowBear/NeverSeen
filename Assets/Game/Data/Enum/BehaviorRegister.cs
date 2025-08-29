using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class BehaviorRegister
{
    public static IBehavior CreateBehavior(BehaviorRegister2 descriptor)
    {
        var constructor = descriptor.behaviorType.GetConstructors().FirstOrDefault();

        var args = constructor.GetParameters()
        .Select(p =>
        {
            if (!descriptor.parameters.TryGetValue(p.Name, out var value))
                throw new Exception($"Missing parameter {p.Name} for {descriptor.behaviorType.Name}");
            return Convert.ChangeType(value, p.ParameterType);
        })
        .ToArray();

        return (IBehavior)constructor.Invoke(args);
    }
}

public class BehaviorRegister2
{
    public Type behaviorType;
    public Dictionary<string, object> parameters;
}



//ПРИМЕР ИСПОЛЬЗОВАНИЯ 

//var spawnData = new EnemySpawnData
//{
//    position = new Vector3(0, 0, 0),
//    behaviors = new List<BehaviorDescriptor>
//    {
//        new BehaviorDescriptor
//        {
//            behaviorType = typeof(MeleeAttack),
//            parameters = new Dictionary<string, object>
//            {
//                { "range", 1.5f }
//            }
//        },
//        new BehaviorDescriptor
//        {
//            behaviorType = typeof(Patrol),
//            parameters = new Dictionary<string, object>
//            {
//                { "points", new[] { new Vector3(0,0,0), new Vector3(5,0,0) } }
//            }
//        }
//    }
//};
//