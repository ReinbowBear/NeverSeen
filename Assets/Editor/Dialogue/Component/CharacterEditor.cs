namespace DialogueManager.InspectorEditors
{
    using System.Collections.Generic;
    using DialogueManager.Models;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;

    /// <summary>
    /// Inspector custom editor of the Character Object
    /// </summary>
    [CustomEditor( typeof( CharacterTalk ) )]
    public class CharacterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.Update();
            CharacterTalk character = ( CharacterTalk )this.target;

            character.characterName = EditorGUILayout.TextField( "Name", character.characterName );
            character.Voice = EditorGUILayout.ObjectField( "Voice", character.Voice, typeof( AudioClip ), true ) as AudioClip;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField( "Expression List", EditorStyles.boldLabel );

            if (character.Expressions == null)
            {
                character.Expressions = new List<Expression>();
            }

            ExpressionEditor.Display( character.Expressions );
            if (GUILayout.Button( EditorButtons.AddExpressionButton, EditorStyles.miniButton, EditorButtons.NormalButtonWidth ))
            {
                Expression newExpression = new Expression();
                character.Expressions.Add( newExpression );
            }
            
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty( this.target );
                EditorSceneManager.MarkSceneDirty( EditorSceneManager.GetActiveScene() );
            }
        }
    }
}