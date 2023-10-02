using System;

public class PlayerModel
{
    public event Action<int> MoneyChanged;

    public int Money
    {
        get => _money;
        private set
        {
            _money = value;
            MoneyChanged?.Invoke(_money);
        }
    }
    private int _money;

    public PlayerModel(int money)
    {
        _money = money;
    }

    public void SetMoney(int count)
    {
        Money = count;
    }
}