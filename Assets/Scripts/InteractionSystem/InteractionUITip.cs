using TMPro;
using UnityEngine;

public class InteractionUITip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactionPrompt;
    public IInteractable InteractionObject { get; private set; }

    public void SetUp(IInteractable interactionObject)
    {
        interactionPrompt.text = interactionObject.InteractionPrompt;
    }
}
