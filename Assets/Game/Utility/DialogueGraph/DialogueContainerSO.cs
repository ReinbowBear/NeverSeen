using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueContainerSO : ScriptableObject
{
    public string FileName;
    public Dictionary<NodeGroupSO, List<DialogueNodeSO>> DialogueGroups = new ();
    public List<DialogueNodeSO> UngroupedDialogues = new ();


    public List<string> GetDialogueGroupNames()
    {
        List<string> dialogueGroupNames = new List<string>();

        foreach (NodeGroupSO dialogueGroup in DialogueGroups.Keys)
        {
            dialogueGroupNames.Add(dialogueGroup.GroupName);
        }

        return dialogueGroupNames;
    }

    public List<string> GetGroupedDialogueNames(NodeGroupSO dialogueGroup, bool startingDialoguesOnly)
    {
        List<DialogueNodeSO > groupedDialogues = DialogueGroups[dialogueGroup];
        List<string> groupedDialogueNames = new List<string>();

        foreach (DialogueNodeSO groupedDialogue in groupedDialogues)
        {
            if (startingDialoguesOnly && !groupedDialogue.IsStartNode) continue;

            groupedDialogueNames.Add(groupedDialogue.Name);
        }

        return groupedDialogueNames;
    }

    public List<string> GetUngroupedDialogueNames(bool startingDialoguesOnly)
    {
        List<string> ungroupedDialogueNames = new List<string>();

        foreach (DialogueNodeSO ungroupedDialogue in UngroupedDialogues)
        {
            if (startingDialoguesOnly && !ungroupedDialogue.IsStartNode) continue;

            ungroupedDialogueNames.Add(ungroupedDialogue.Name);
        }

        return ungroupedDialogueNames;
    }
}
