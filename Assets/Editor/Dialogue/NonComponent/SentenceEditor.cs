﻿namespace DialogueManager.InspectorEditors
{
    using System.Linq;
    using DialogueManager.Models;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Inspector Editor for a Sentence
    /// </summary>
    public class SentenceEditor
    {
        /// <summary>
        /// Displays on the Inspector GUI a Sentence
        /// </summary>
        /// <param name="sentence">Sentence to be displayed</param>
        public static void Display(Sentence sentence)
        {
            sentence.Character = EditorGUILayout.ObjectField( "Character", sentence.Character, typeof( CharacterTalk ), true ) as CharacterTalk;

            if (sentence.Character != null)
            {
                string[] expressionListNames = sentence.Character.Expressions.Select( e => e.Name ).ToArray();
                sentence.ExpressionIndex = EditorGUILayout.Popup(
                    "Expression",
                    sentence.ExpressionIndex,
                    expressionListNames,
                    EditorStyles.popup );
                EditorGUILayout.LabelField( "Paragraph:" );
                EditorGUI.indentLevel++;
                sentence.SentenceText = EditorGUILayout.TextArea( sentence.SentenceText, GUILayout.MaxHeight( 75 ) );
                EditorGUI.indentLevel--;
            }
        }
    }
}