using System;

public class KeyPresenter
{
    private readonly KeyModel _model;
    private readonly KeyView _view;

    public KeyPresenter(KeyModel model, KeyView view)
    {
        _model = model;
        _view = view;

        _model.LetterChanged += _view.SetLetter;
    }

    public void SetAvailableState()
    {
        _view.SetAvailableState();   
    }

    public void SetNonAvailableState()
    {
        _view.SetNonAvailableState();
    }
    
    public void RemoveLetter()
    {
        _model.SetLetter(' ');
    }
    
    public void PutLetter(char letter)
    {
        _model.SetLetter(letter);
    }

    public char GetLetter()
    {
        return _model.Letter;
    }
    
    public bool IsEmpty()
    {
        return String.IsNullOrWhiteSpace(_model.Letter.ToString());
    }

    public void Off()
    {
        _view.Off();
    }

    public void On()
    {
        _view.On();
    }
    
}