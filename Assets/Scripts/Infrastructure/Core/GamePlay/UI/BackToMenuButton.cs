using UnityEngine;
using UnityEngine.EventSystems;

public class BackToMenuButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip _clickSound;
    
    private GameStateMachine _stateMachine;
    private IAudioService _audioService;

    private bool _isInteractable = true;
    
    public void Construct(GameStateMachine stateMachine, IAudioService audioService)
    {
        _stateMachine = stateMachine;
        _audioService = audioService;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!_isInteractable)
            return;
        
        _audioService.OneShotPlaySoundEffect(_clickSound);
        _stateMachine.Enter(GameStates.Menu);
    }

    public void OnWordStartCheckout()
    {
        _isInteractable = false;
    }

    public void OnWordCompleteCheckout()
    {
        _isInteractable = true;
    }
}