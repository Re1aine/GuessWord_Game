using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldFieldView : MonoBehaviour
{
    public event Action<KeyPresenter> SlotClicked;
    public event Action StartCheckoutWord;
    public event Action CompleteCheckoutWord;
    public event Action SuccessCompleteCheckout;
    
    [SerializeField] private Transform _container;
    [SerializeField] private AudioClip _unCorrectCheckoutSound;
    [SerializeField] private AudioClip _correctCheckoutSound;
    
    private GameFactory _gameFactory;
    private IAudioService _audioService;
    private WordFieldPresenter _presenter;

    private List<WordSlotPresenter> _slots;
    private List<WordSlotView> _views;

    public void Construct(GameFactory gameFactory, WordFieldPresenter presenter, IAudioService audioService)
    {
        _gameFactory = gameFactory;
        _presenter = presenter;
        _audioService = audioService;
    }

    public List<WordSlotPresenter> SetSlots(int count)
    {
        Cleanup();
        CreateSlots(count);
        _slots.ForEach(x => x.Reset());

        return _slots;
    }

    public void RemoveUnnecessaryLetters()
    {
        _presenter.RemoveUnnecessaryLetters();
    }
    
    public void Fill(KeyPresenter presenter)
    {
        _presenter.Fill(presenter);
    }

    public void AllSlotsClicked()
    {
        _views.ForEach(x => x.SilentClick());
    }

    public void StartCheckoutInputWord()
    {
        Off();
        StartCheckoutWord?.Invoke();
    }

    private void CompleteCheckoutInputWord(WordSlotPresenter slot, bool isSuccess)
    {
        if (isSuccess)
            return;
        else
        {
            On();
            slot.Reset();   
            CompleteCheckoutWord?.Invoke();
        }
    }
    
    private void CreateSlots(int count)
    {
        _slots = new List<WordSlotPresenter>(count);
        _views = new List<WordSlotView>(count);
        
        for (int i = 0; i < count; i++) 
            _slots.Add(_gameFactory.CreateWordSlot(_container));
        
        _views = new List<WordSlotView>(count);
       
        foreach (Transform slot in _container)
        {
            if (slot.TryGetComponent(out WordSlotView view)) 
                _views.Add(view);
        }
        
        foreach (var view in _views) 
            view.Clicked += SlotClicked;
    }

    public void CompleteFailure()
    {
        foreach (var slot in _slots)
            slot.HighlightUnCorrect(() =>CompleteCheckoutInputWord(slot, false));
        
        _audioService.OneShotPlaySoundEffect(_unCorrectCheckoutSound);
    }
    
    public void CompleteSuccess()
    {
        foreach (var slot in _slots ) 
            slot.HighlightCorrect(() => CompleteCheckoutInputWord(slot, true));
        
        _audioService.OneShotPlaySoundEffect(_correctCheckoutSound);
        SuccessCompleteCheckout?.Invoke();
    }
    
    private void Cleanup()
    {
        foreach (Transform child in _container) 
            Destroy(child.gameObject);
    }
    
    private void Off()
    {
        _views.ForEach(x => x.Off());
    }

    private void On()
    {
        _views.ForEach(x => x.On());
    }
}