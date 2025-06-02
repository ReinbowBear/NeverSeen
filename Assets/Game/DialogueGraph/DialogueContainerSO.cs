using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueContainerSO : ScriptableObject
{
    public string FileName;
    public SerializableDictionary<DialogueGroupSO, List<DialogueNodeSO>> DialogueGroups = new SerializableDictionary<DialogueGroupSO, List<DialogueNodeSO>>();
    public List<DialogueNodeSO> UngroupedDialogues = new List<DialogueNodeSO>();


    public List<string> GetDialogueGroupNames()
    {
        List<string> dialogueGroupNames = new List<string>();

        foreach (DialogueGroupSO dialogueGroup in DialogueGroups.Keys)
        {
            dialogueGroupNames.Add(dialogueGroup.GroupName);
        }

        return dialogueGroupNames;
    }

    public List<string> GetGroupedDialogueNames(DialogueGroupSO dialogueGroup, bool startingDialoguesOnly)
    {
        List<DialogueNodeSO > groupedDialogues = DialogueGroups[dialogueGroup];
        List<string> groupedDialogueNames = new List<string>();

        foreach (DialogueNodeSO groupedDialogue in groupedDialogues)
        {
            if (startingDialoguesOnly && !groupedDialogue.IsStartNode)
            {
                continue;
            }

            groupedDialogueNames.Add(groupedDialogue.DialogueName);
        }

        return groupedDialogueNames;
    }

    public List<string> GetUngroupedDialogueNames(bool startingDialoguesOnly)
    {
        List<string> ungroupedDialogueNames = new List<string>();

        foreach (DialogueNodeSO ungroupedDialogue in UngroupedDialogues)
        {
            if (startingDialoguesOnly && !ungroupedDialogue.IsStartNode)
            {
                continue;
            }

            ungroupedDialogueNames.Add(ungroupedDialogue.DialogueName);
        }

        return ungroupedDialogueNames;
    }
}
