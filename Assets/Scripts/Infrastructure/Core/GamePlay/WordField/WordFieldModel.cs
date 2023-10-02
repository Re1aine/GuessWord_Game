using System;
using System.Collections.Generic;
using System.Linq;

public class WordFieldModel
{
    public event Func<int, List<WordSlotPresenter>> SlotsChanged;

    public List<WordSlotPresenter> Slots { get; private set; }
    public GameTaskStaticData Data { get; private set; }
    private KeyboardPresenter _keyboard;
    
    public WordFieldModel(KeyboardPresenter keyboard)
    {
        _keyboard = keyboard;
    }

    public void SetData(GameTaskStaticData data)
    {
        Data = data;
    }

    public void SetSlots(int count)
    {
        Slots = SlotsChanged?.Invoke(count);
    }

    public WordSlotPresenter GetFirstEmptySlot()
    {
        if (IsFull())
            return null;

        return Slots.First(x => x.IsEmpty());
    }

    private bool IsFull()
    {
        return !Slots[^1].IsEmpty();
    }
    
    public bool IsRightInputWord()
    {
        char[] word = new char[Slots.Count];

        for (int i = 0; i < word.Length; i++)
            word[i] = Slots[i].GetLetter();

        return string.Equals(new string(word), Data.Word);
    }
}
