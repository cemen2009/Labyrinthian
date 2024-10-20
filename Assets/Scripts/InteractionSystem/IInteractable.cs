public interface IInteractable
{
    public string InteractionPrompt { get; }

    public bool TryInteract(Interactor initiator);
}