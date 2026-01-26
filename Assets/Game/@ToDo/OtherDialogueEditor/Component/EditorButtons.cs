namespace DialogueManager.InspectorEditors
{
    using UnityEngine;

    public class EditorButtons
    {
        public static GUILayoutOption NormalButtonWidth = GUILayout.Width( 85f );
        public static GUILayoutOption MiniButtonWidth = GUILayout.Width( 24f );

        public static GUIContent AddStatusButton = new GUIContent( "Add", "Add Status" );
        public static GUIContent RemoveStatusButton = new GUIContent( "-", "Remove Status" );

        public static GUIContent AddDialogueButton = new GUIContent( "Add", "Add Dialogue" );
        public static GUIContent RemoveDialogueButton = new GUIContent( "-", "Remove Dialogue" );
        
        public static GUIContent AddPendingStatusButton = new GUIContent( "Add", "Add PendingStatus" );
        public static GUIContent RemovePendingStatusButton = new GUIContent( "-", "Remove PendingStatus" );

        public static GUIContent AddExpressionButton = new GUIContent( "Add", "Add Expression" );
        public static GUIContent RemoveExpressionButton = new GUIContent( "-", "Remove Expression" );
    }
}
