using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Tooltip("Internal point of character for detecting collisions with interactable objects")]
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRadius;
    [SerializeField] private LayerMask interactionLayer;

    [SerializeField] GameObject interactionTipPrefab, interactionUIPanel;
    
    private GameObject interactionTip;
    private IInteractable interactionObject;

    private readonly Collider[] colliders = new Collider[3];
    private int interactionsAmount;

    private void Update()
    {
        interactionsAmount = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRadius, 
            colliders, interactionLayer);

        if (interactionsAmount > 0)
        {
            interactionObject = colliders[0].GetComponent<IInteractable>();

            if (interactionObject != null)
            {
                // additional check is this actual interaction object in interaction tip
                if (interactionTip == null || interactionTip.GetComponent<InteractionUITip>().InteractionObject != interactionObject)
                {
                    Destroy(interactionTip);
                    interactionTip = Instantiate(interactionTipPrefab, interactionUIPanel.transform);
                    interactionTip.GetComponent<InteractionUITip>().SetUp(interactionObject);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (interactionObject.TryInteract(this))
                    {
                        Debug.Log($"successful interaction with {colliders[0].gameObject.name}");
                    }
                    else
                    {
                        Debug.Log($"unsuccessful interaction with {colliders[0].gameObject.name}");
                    }
                }
            }
        }
        else
        {
            if (interactionTip != null) Destroy(interactionTip);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    }
}
