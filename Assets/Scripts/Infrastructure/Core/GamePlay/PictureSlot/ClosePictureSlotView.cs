using TMPro;
using UnityEngine;

public class ClosePictureSlotView : PictureSlotView
{
    [SerializeField] private TextMeshProUGUI _openCostText;
    [SerializeField] private RectTransform _root;
    [SerializeField] private RectTransform _container;
    [SerializeField] private RectTransform _content;
    [SerializeField] private int _openCost = 2;

    private RectTransform _rectTransform;
    private AudioSource _source;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();;
        _source = GetComponent<AudioSource>();
    }

    public void SetContainerRect(RectTransform rect)
    {
        _container = rect;
    }
    
    public void SetContentRect(RectTransform content)
    {
        _content = content;
    }

    private void Start()
    {
        SetCost(_openCost);
    }

    public void SetCost(int cost)
    {
        SetOpenCost(cost);
        SetText();
    }
    
    private void SetText()
    {
        _openCostText.text = $"-{_openCost}";
    }
    
    private void SetOpenCost(int openCost)
    {
        _openCost = openCost;
    }

    private void CreateSlot()
    {
        var prefab = Resources.Load<OpenPictureSlotView>("PictureSlot/PictureSlotView");
        var slot = Instantiate(prefab, transform);
        var slotRectTransform = slot.GetComponent<RectTransform>();
        
        slot.SetContainerRect(_container);
        
        slotRectTransform.sizeDelta = _rectTransform.sizeDelta;
        slotRectTransform.position = _rectTransform.position;

        slotRectTransform.SetParent(_root);
        slotRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        slotRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        slotRectTransform.position = transform.position;

        var field = transform.parent.parent.GetComponent<PictureFieldView>();
        field.GatherSlots();

        TransformNextLockSlot(field);

        slot.transform.SetParent(_content);
        int childIndex = field.GetSiblingIndex(this);
        slot.transform.SetSiblingIndex(childIndex);
        
        field.UpdateSlot(childIndex);
        
        Destroy(gameObject);
    }

    private void TransformNextLockSlot(PictureFieldView field)
    {
        int index = field.GetSlotIndex(this);
        
        if (!field.IsLastSlot(index))
        {
            var nextSlot = field.GetSlot(index + 1);
            if (nextSlot.transform.TryGetComponent(out LockedPictureSlotView lockedSlot))
                lockedSlot.UnLocked(_openCost * 2);
        }
    }
    
    private void OpenSlot()
    {
        if (_player.GetMoneyCount() < _openCost)
            return;
        
        _player.SubtractMoney(_openCost);
        
        CreateSlot();
    }
    
    public override void Interact()
    {
        OpenSlot();
    }
}