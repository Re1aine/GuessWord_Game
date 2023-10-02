using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using RectTransform = UnityEngine.RectTransform;

public class OpenPictureSlotView : PictureSlotView
{
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _container;
    [SerializeField] private float _smoothZoom;
    
    private RectTransform _zoomingImageRect;
    private RectTransform _rectTransform;
    private RectTransform _previousRect;
    private OpenPictureSlotView _zoomingSlot;

    private bool _isRaycastable;
    private bool _isZoomed;

    public override void SetPicture(Sprite sprite)
    {
        _image.sprite = sprite;
    }
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _isRaycastable = true;
    }

    public void SetContainerRect(RectTransform container)
    {
        _container = container;
    }
    
    private void SetPreviousRect(RectTransform rect)
    {
        _previousRect = rect;
    }

    private void Zoom()
    {
        CreateSlot();
        
        StartCoroutine(RectLerp(_zoomingImageRect, _container, ZoomCompleted));
    }

    private void UnZoom()
    {
        StartCoroutine(RectLerp(_rectTransform, _previousRect, UnZoomCompleted));
    }

    private void CreateSlot()
    {
        var slot = Instantiate(this, transform);
        var slotRectTransform = slot.GetComponent<RectTransform>();

        slot._isRaycastable = false;
        slot._isZoomed = true;
        
        slot.SetPreviousRect(_rectTransform);
        slot.SetContainerRect(_container);

        slotRectTransform.sizeDelta = _rectTransform.sizeDelta;
        slotRectTransform.position = _rectTransform.position;

        slotRectTransform.SetParent(_container);
        slotRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        slotRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        slotRectTransform.position = transform.position;
        
        _zoomingImageRect = slotRectTransform;
        _zoomingSlot = slot;
    }

    private IEnumerator RectLerp(RectTransform from, RectTransform to, Action completed)
    {
        _isRaycastable = false;
        while ((from.position - to.position).magnitude > 0.1) 
        {
            from.position = Vector2.Lerp(
                from.position,
                to.position,
                _smoothZoom);
            
            from.sizeDelta = Vector2.Lerp(
                from.sizeDelta,
                to.sizeDelta,
                _smoothZoom);
            
            yield return null;
        }

        _isRaycastable = true;
        completed?.Invoke();
    }
    
    private void ZoomCompleted()
    {
        _zoomingSlot._isRaycastable = true;
    }

    private void UnZoomCompleted()
    {
        Destroy(_container.GetChild(0).gameObject);
    }
    
    public override void Interact()
    {
        if (!_isRaycastable) return;
        
        if (_isZoomed)
            UnZoom();
        else
            Zoom();
    }
}