using System;
using UnityEngine;

public class WordSlotPresenter
{
    private readonly WordSlotModel _model;
    private readonly WordSlotView _view;

    public WordSlotPresenter(WordSlotModel model , WordSlotView view)
    {
        _model = model;
        _view = view;

        _model.LetterChanged += _view.SetLetter;
        _model.ColorChanged += _view.SetColor;
    }

    public void SetLetter(char letter)
    {
        _model.SetLetter(letter);
    }
    
    public char GetLetter()
    {
        return _model.Letter;
    }

    public void SetColor(Color color)
    {
        _model.SetColor(color);
    }

    public void RemoveLetter()
    {
        _model.SetLetter(' ');
    }

    public bool IsEmpty()
    {
        return char.IsWhiteSpace(_model.Letter);
    }
    
    public void SetInputKey(KeyPresenter presenter)
    {
        _model.SetInputKey(presenter);
    }

    public KeyPresenter GetInputKey()
    {
        return _model.KeyPresenter;
    }
    
    public void HighlightUnCorrect(Action onCompleted)
    {
        _view.HighlightUnCorrect(onCompleted);
    }
    
    public void Reset()
    {
        ResetColor();
        RemoveLetter();
        
        if(GetInputKey() != null)
            GetInputKey().SetAvailableState();
    }
    
    public void HighlightCorrect(Action onCompleted = null)
    {
        _view.HighlightCorrect(onCompleted);
    }

    public void ResetColor(Action onCompleted = null)
    {
        _view.ResetColor(onCompleted);
    }
}