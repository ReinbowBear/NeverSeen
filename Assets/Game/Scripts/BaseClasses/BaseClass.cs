using System;
using System.Collections.Generic;
using Zenject;

public class BaseClass
{
    private List<Action> Subscription = new();
    [Inject] private EventBus eventBus;
    [Inject] private EventBus world;
    [Inject] private EventBus entry;

    private void UnsubscribeAll()
    {
        foreach (var action in Subscription)
        {
            //eventBus.RemoveListener(action);
            // надо знать событие и второй ключ
            // тут то и были бы полезны токены
        }
    }
}