using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    public bool TryInteract(Interactor initiator)
    {
        Debug.Log("opening the door");

        return true;
    }
}
