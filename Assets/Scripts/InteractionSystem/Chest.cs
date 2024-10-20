using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string objectName;
    [SerializeField] private string interactionPrompt;
    public string InteractionPrompt => interactionPrompt;

    public bool TryInteract(Interactor initiator)
    {
        Debug.Log("opening the chest");

        return true;
    }
}
