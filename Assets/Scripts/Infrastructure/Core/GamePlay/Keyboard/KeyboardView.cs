using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardView : MonoBehaviour
{
    public event Action<KeyPresenter> KeyClicked;
    
    [SerializeField] private List<KeyView> _keys;
    private KeyboardPresenter _presenter;

    public void Construct(KeyboardPresenter presenter)
    {
        _presenter = presenter;
    }
    
    private void OnEnable()
    {
        foreach (var view in _keys) 
            view.Clicked += KeyClicked;
    }

    private void OnDisable()
    {
        foreach (var view in _keys) 
            view.Clicked -= KeyClicked;
    }

    public void SetKeyState(KeyPresenter key)
    {
        key?.SetAvailableState();
    }

    public void RemoveUnnecessaryLetters()
    {
        _presenter.RemoveUnnecessaryLetters();
    }
    
    public List<KeyView> GetKeys()
    {
        return _keys;
    }

    public void OnWordStartCheckout()
    {
        _keys.ForEach(x => x.Off());
    }

    public void OnWordCompleteCheckout()
    {
        _keys.ForEach(x => x.On());
    }
}