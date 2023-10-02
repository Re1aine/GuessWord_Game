using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordSlotView : MonoBehaviour, IPointerClickHandler
{
    public event Action<KeyPresenter> Clicked;

    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _image;

    private IAudioService _audioService;
    private WordSlotPresenter _presenter;

    public bool _isInteractable = true;
    public void Construct(WordSlotPresenter presenter, IAudioService audioService)
    {
        _presenter = presenter;
        _audioService = audioService;
    }
    
    public void SetLetter(char letter)
    {
        _text.text = letter.ToString();
    }

    public void SetColor(Color color)
    {
        _image.color = color;
    }

    public Color GetColor()
    {
        return _image.color;
    }
    
    public void HighlightUnCorrect(Action onCompleted = null)
    {
         StartCoroutine(ColorLerp(Color.red, onCompleted));
    }

    public void HighlightCorrect(Action onCompleted = null)
    {
        StartCoroutine(ColorLerp(Color.green, onCompleted));
    }

    public void ResetColor(Action onCompleted = null)
    {
        StartCoroutine(ColorLerp(Color.gray, onCompleted));
    }

    private IEnumerator ColorLerp(Color to, Action onCompeted)
    {
        var currentColor = new Vector4(_image.color.r, _image.color.g, _image.color.b, _image.color.a);
        var targetColor = new Vector4(to.r, to.g, to.b, _image.color.a);
        var distance = (targetColor - currentColor).magnitude;
        
        while (distance > 0.01f)
        {
            _image.color = Color.Lerp(_image.color, targetColor, 0.075F);
            currentColor =  new Vector4(_image.color.r, _image.color.g, _image.color.b, _image.color.a);
            distance = (targetColor - currentColor).magnitude;
            yield return null;
        }
        onCompeted?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isInteractable || _presenter.IsEmpty())
            return;

        Clicked?.Invoke(_presenter.GetInputKey());
        _audioService.OneShotPlaySoundEffect(_clickSound);
        _presenter.Reset();
    }

    public void SilentClick()
    {
        if (!_isInteractable || _presenter.IsEmpty())
            return;
        
        Clicked?.Invoke(_presenter.GetInputKey());
        _presenter.Reset();
    }
    
    public void Off()
    {
        _isInteractable = false;
    }

    public void On()
    {
        _isInteractable = true;
    }
}