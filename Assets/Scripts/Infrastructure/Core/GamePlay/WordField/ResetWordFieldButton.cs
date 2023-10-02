using UnityEngine;
using UnityEngine.EventSystems;

public class ResetWordFieldButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip _clickSound;
    
    private WorldFieldView _worldFieldView;
    private IAudioService _audioService;

    private bool _isInteractable = true;

    public void Construct(WorldFieldView worldFieldView, IAudioService audioService)
    {
        _worldFieldView = worldFieldView;
        _audioService = audioService;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!_isInteractable)
            return;
        
        _worldFieldView.AllSlotsClicked();
        _audioService.OneShotPlaySoundEffect(_clickSound);
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