namespace DialogueManager.GameComponents.Triggers
{

    using UnityEngine;

    public class PositionTrigger : MonoBehaviour
    {
        public GameObject Tracked;
        private bool wasTriggered = false;

        private Transform TrackedTransform;

        private void Start()
        {
            TrackedTransform = Tracked.GetComponent<Transform>();
        }

        private void Update()
        {
            if (TrackedTransform.position.x < transform.position.x && TrackedTransform.position.y > transform.position.y)
            {
                if (wasTriggered == false)
                {
                    wasTriggered = true;

                    ConversationComponent conversation = GetComponent<ConversationComponent>();
                    if (conversation != null)
                    {
                        conversation.Trigger( );
                    }
                }
            }
            else if (wasTriggered)
            {
                wasTriggered = false;
            }
        }
    }
}