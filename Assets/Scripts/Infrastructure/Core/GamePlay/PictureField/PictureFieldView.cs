using System.Collections.Generic;
using UnityEngine;

public class PictureFieldView : MonoBehaviour
{
    private PictureFieldPresenter _presenter;
    private List<PictureSlotView> _slots;
    private PlayerPresenter _player;

    private void Awake ()
    {
        _slots = new List<PictureSlotView>(4);
        GatherSlots();
    }

    public void Construct(PictureFieldPresenter presenter)
    {
        _presenter = presenter;
    }

    public void GatherSlots()
    {
        _slots.Clear();
        
        foreach (Transform child in transform.GetChild(0))
        {
            if(child.TryGetComponent(out PictureSlotView view))
                _slots.Add(view);
        }
    }

    public void UpdateSlot(int index)
    {
        GatherSlots();
        _presenter.UpdateSlot(index);
    }

    public bool IsLastSlot(int index)
    {
        return index == _slots.Count - 1;
    }

    public int GetSlotIndex(PictureSlotView slot)
    {
        return _slots.IndexOf(slot);
    }
    
    public List<PictureSlotView> GetSlots()
    {
        return _slots;
    }

    public PictureSlotView GetSlot(int index)
    {
        return _slots[index];
    }

    public int GetSiblingIndex(PictureSlotView slot)
    {
        return _slots.IndexOf(slot);
    }
}