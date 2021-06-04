using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBoxButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator boxAnimator;

    private readonly int openBox = Animator.StringToHash("BoxOpen");

    public bool CanInteract { get; private set; } = true;

    public bool IsInteracting { get; private set; }

    void Start()
    {
        CanInteract = true;
    }

    public void Interact()
    {
       
    }

    //Disable Confiner Colliders and Play Open Box Animation
    public void OnStartInteraction()
    {
        boxAnimator.ResetTrigger(openBox);
        boxAnimator.SetTrigger(openBox);
        CanInteract = false;
        BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }
}
