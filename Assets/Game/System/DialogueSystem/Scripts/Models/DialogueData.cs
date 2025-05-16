namespace DialogueManager.Models
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [System.Serializable]
    public class DialogueData
    {
        public GameObject CanvasObjectsPrefab;
        public GameObject GameConversationsPrefab;
        public float WaitTime = 0.01f;
        public float VoiceVolume = 1f;
        public bool DoubleTap = true;
        public string NextKey = "z";
        public Font Font;
        public Material Material;
        public Transform DialogueStartPoint { get; set; }
        public Image ImageText { get; set; }
        public Animator Animator { get; set; }
        public AudioSource Source { get; set; }
        public bool Finished { get; set; }
        public Dialogue DialogueToShow { get; set; }
    }
}
