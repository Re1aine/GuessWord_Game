
using System;
using System.Linq;

public class WordFieldPresenter
{
    private readonly WordFieldModel _model;
    private readonly WorldFieldView _view;
    private readonly LevelPresenter _levelPresenter;
    
    public WordFieldPresenter(WordFieldModel model, WorldFieldView view, LevelPresenter levelPresenter)
    {
        _model = model;
        _view = view;
        _levelPresenter = levelPresenter;

        _model.SlotsChanged += _view.SetSlots;
    }
    
    public void LoadData(GameTaskStaticData data)
    {
        _model.SetData(data);
        _model.SetSlots(data.Word.Length);
    }
    
    public void Fill(KeyPresenter keyPresenter)
    {
        var emptySlot = _model.GetFirstEmptySlot();
        if(emptySlot == null)
            return;
        
        if (IsLast(emptySlot))
        {
            FillSlot(emptySlot, keyPresenter);
            CheckoutWord();
            return;    
        }
        
        FillSlot(emptySlot, keyPresenter);
    }

    public void RemoveUnnecessaryLetters()
    {
        string word = _model.Data.Word;

        foreach (var slot in _model.Slots.Where(x => !word.Contains(x.GetLetter()))) 
            slot.Reset();
    }
    
    private void FillSlot(WordSlotPresenter slot, KeyPresenter keyPresenter)
    {
        slot.SetInputKey(keyPresenter);
        slot.SetLetter(keyPresenter.GetLetter());
    }
    
    private bool IsLast(WordSlotPresenter slot)
    {
        return _model.Slots[^1] == slot;
    }
    
    private void CheckoutWord()
    {
        _view.StartCheckoutInputWord();

        if (_model.IsRightInputWord())
            CompleteSuccessCheckout();
        else
            CompleteFailureCheckout();
    }

    private void CompleteSuccessCheckout()
    {
        _view.CompleteSuccess();
        _levelPresenter.SetCompleted();
    }

    private void CompleteFailureCheckout()
    {
        _view.CompleteFailure();
    }
}