using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaveControl))]
public class CreateEnemy : Editor
{
    private string enemyName;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WaveControl myScript = (WaveControl)target;
        if (myScript == null)
        {
            return;
        }

        enemyName = EditorGUILayout.TextField("Создать врага", enemyName);

        if (Application.isPlaying)
        {
            if (GUILayout.Button("Выполнить функцию"))
            {
                myScript.StartCoroutine(myScript.CreateEnemy(enemyName));
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