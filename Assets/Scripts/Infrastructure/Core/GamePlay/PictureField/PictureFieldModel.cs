using System;
using System.Collections.Generic;

public class PictureFieldModel
{
    public event Action SlotChanged;

    private List<PictureSlotPresenter> _slots;

    public GameTaskStaticData Data { get; private set; }

    public List<PictureSlotPresenter> Slots
    {
        get => _slots;
        private set
        {
            _slots = value;
            SlotChanged?.Invoke();
        }
    }

    public void SetData(GameTaskStaticData data)
    {
        Data = data;
    }
    
    public void SetSlots(List<PictureSlotPresenter> slots)
    {
        Slots = slots;
    }

    public void UpdateSlot(PictureSlotPresenter slot, int index)
    {
        _slots[index] = slot;
    }
}