using System;
using UnityEngine;

public class LockedPictureSlotView : PictureSlotView
{
    [SerializeField] private AudioSource _source;

    [SerializeField] private RectTransform _container;
    [SerializeField] private RectTransform _root;
    [SerializeField] private RectTransform _content;

    [SerializeField] private ClosePictureSlotView prefab;
    
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void UnLocked(int costCloseSlot)
    {
        CreateSlot(costCloseSlot);    
    }

    private void CreateSlot(int costCloseSlot)
    {
        var slot = Instantiate(prefab, transform);
        var slotRectTransform = slot.GetComponent<RectTransform>();
        
        slot.SetContainerRect(_container);
        slot.SetContentRect(_content);
        slot.SetCost(costCloseSlot);
        
        slotRectTransform.sizeDelta = _rectTransform.sizeDelta;
        slotRectTransform.position = _rectTransform.position;

        slotRectTransform.SetParent(_rectTransform);
        slotRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        slotRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        slotRectTransform.position = transform.position;

        var field = transform.parent.parent.GetComponent<PictureFieldView>();
        
        slot.transform.SetParent(_content);
        int index = field.GetSiblingIndex(this);
        slot.transform.SetSiblingIndex(index);
        
        field.UpdateSlot(index);
        
        Destroy(gameObject);
    }
    
    public override void Interact()
    {
        _source.Play();
    }
}