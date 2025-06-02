using UnityEngine;

[System.Serializable]
public class DialogueGroupSO : ScriptableObject // связан с DialogueContainerSO, скриптбл обджекты нужно разделять по отдельным файлам то юнити жалуется что имя файла не совпадает
{
    public string GroupName;
}
