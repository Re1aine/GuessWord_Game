using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyView : MonoBehaviour, IPointerClickHandler
{
    public event Action<KeyPresenter> Clicked;
    
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AudioClip _clickSound;

    private IAudioService _audioService;
    private KeyPresenter _presenter;
    private Image _image;
    private Button _button;

    private bool _isRaycastable = true;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    public void Construct(KeyPresenter presenter, IAudioService audioService)
    {
        _presenter = presenter;
        _audioService = audioService;
    }

    public void SetLetter(char letter)
    {
        _text.text = letter.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!_isRaycastable)
            return;
        
        if(!_button.IsInteractable())
            return;
        
        SetReleaseState();
        
        Clicked?.Invoke(_presenter);
        _audioService.OneShotPlaySoundEffect(_clickSound);
    }

    private void SetReleaseState()
    {
        _button.interactable = false;
        
        _text.color = new Color(
            _text.color.r,
            _text.color.b,
            _text.color.b,
            _image.color.a / 2);
    }

    public void SetNonAvailableState()
    {
        _button.interactable = false;
        _presenter.RemoveLetter();
    }

    public void SetAvailableState()
    {
        _button.interactable = true;
        
        _image.color = new Color(
            _image.color.r, 
            _image.color.g,
            _image.color.b,
            1);

        _text.color = new Color(
            _text.color.r,
            _text.color.b,
            _text.color.b,
            1);
    }
    
    public void Off()
    {
        _isRaycastable = false;
    }

    public void On()
    {
        _isRaycastable = true;
    }
}