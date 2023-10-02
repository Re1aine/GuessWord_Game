using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PictureSlotView : MonoBehaviour, IPictureSlotView, IPointerClickHandler
{
    public PictureSlotType PictureSlotType => _pictureSlotType;
    
    [SerializeField] private PictureSlotType _pictureSlotType;
    protected PlayerPresenter _player;

    public abstract void Interact();
    
    public void Construct(PlayerPresenter player)
    {
        _player = player;
    }
    
    public virtual void SetPicture(Sprite sprite)
    {
        
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Interact();
        Debug.Log("Clicked");
    }
}