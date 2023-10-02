public class PlayerPresenter : Presenter
{
    private readonly PlayerModel _model;

    public PlayerPresenter(PlayerModel model)
    {
        _model = model;
    }

    public void Initialize(UIBase ui)
    {
        _model.MoneyChanged += ui.SetMoney;
        SetMoney(_model.Money);
    }
    
    private void SetMoney(int count)
    {
        _model.SetMoney(count);
    }

    public void AddMoney(int count)
    {
        _model.SetMoney(_model.Money + count);
    }

    public void SubtractMoney(int count)
    {
        _model.SetMoney(_model.Money - count);
    }

    public int GetMoneyCount()
    {
        return _model.Money;
    }
}