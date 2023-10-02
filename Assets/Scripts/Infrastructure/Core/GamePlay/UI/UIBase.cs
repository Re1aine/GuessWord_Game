using System;
using TMPro;
using UnityEngine;

public abstract class UIBase : View, IInitializable
{
    public event Action<UIBase> Initialized;
    public event Action Destroyed; 
    
    [SerializeField] private UIState _state;
    [SerializeField] private TextMeshProUGUI _money;
    
    public void SetMoney(int count)
    {
        _money.text = count.ToString();
    }

    public void Initialize()
    {
        Init();
        Initialized?.Invoke(this);
    }

    protected abstract void Init();

    public void Destroy()
    {
        Destroyed?.Invoke();
        Destroy(gameObject);
    }
}