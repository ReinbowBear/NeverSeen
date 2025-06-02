using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerSpawner))]
public class ReplaceCharacter : Editor
{
    private string characterName;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerSpawner myScript = (PlayerSpawner)target;
        if (myScript == null)
        {
            return;
        }

        characterName = EditorGUILayout.TextField("Создать игрока", characterName);

        if (Application.isPlaying)
        {
            if (GUILayout.Button("Выполнить функцию"))
            {
                Destroy(Character.instance.gameObject);

                myScript.StartCoroutine(myScript.CreatePlayer(characterName));
            }
        }
        else
        {
            GUIStyle biggerHelpBox = new GUIStyle(EditorStyles.helpBox);
            biggerHelpBox.fontSize = EditorStyles.label.fontSize; // подгоняю шрифт под размер юнити шрифтов

            EditorGUILayout.LabelField("Функция доступна только во время игры", biggerHelpBox);
        }
    }
}
