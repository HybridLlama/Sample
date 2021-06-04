
/// <summary>
/// Interface Implemented by all interactables
/// Interactions are currently called By the <see cref="InteractablesRaycast"/> on all objects in the layer <remarks>Raycast Interactables</remarks>
/// </summary>
public interface IInteractable
{
    bool CanInteract { get; }
    bool IsInteracting { get; }

    //Runs Constantly on a condition like Update
    void Interact();
    //Runs Once When the interaction is Initialized
    void OnStartInteraction();
}
