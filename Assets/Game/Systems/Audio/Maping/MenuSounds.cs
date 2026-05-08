using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : ISystem
{
    private Audio audio;
    private Dictionary<Type, SoundSO> sounds = new();

    public MenuSounds(Audio audio, Factory factory)
    {
        this.audio = audio;

        foreach (var soundBind in factory.LoadLabel<SoundBindingSO>(Labels.MenuSound))
        {
            sounds.Add(soundBind.EventType.Type ,soundBind.Sound);
        }
    }


    public void Execute(World world, EntityCommands commands)
    {
        foreach (var type in commands.Events)
        {
            if (sounds.TryGetValue(type, out var sound))
            {
                audio.Play(sound);
            }
        }

        //if (world.Has<OnHoverEnter>()) audio.Play(Resolve<OnHoverEnter>());
        //if (world.Has<OnButtonInvoke>()) audio.Play(Resolve<OnButtonInvoke>());
//
        //if (world.Has<OnPanelOpen>()) audio.Play(Resolve<OnPanelOpen>());
        //if (world.Has<OnPanelClose>()) audio.Play(Resolve<OnPanelClose>());
    }
}
