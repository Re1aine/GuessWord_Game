using System.Collections.Generic;

public class PictureFieldPresenter
{
    private readonly PictureFieldModel _model;
    private readonly PictureFieldView _view;
    private readonly GameFactory _gameFactory;
    private readonly PlayerPresenter _player;

    public PictureFieldPresenter(PictureFieldModel model, PictureFieldView view, GameFactory gameFactory)
    {
        _model = model;
        _view = view;
        _gameFactory = gameFactory;
        
        CreateSlots();
    }

    private void CreateSlots()
    {
        List<PictureSlotPresenter> slots = new (4);
        
        for (int i = 0; i < _view.GetSlots().Count; i++) 
            slots.Add(_gameFactory.CreatePictureSlot(_view.GetSlots()[i]));

        _model.SetSlots(slots);
    }

    public void UpdateSlot(int index)
    {
        var slot = _gameFactory.CreatePictureSlot(_view.GetSlots()[index]);
        _model.UpdateSlot(slot, index);
        _model.Slots[index].SetPicture(_model.Data.Pictures[index]);
    }
    
    public void LoadData(GameTaskStaticData data)
    {
        _model.SetData(data);
        
        for (int i = 0; i < data.Pictures.Count; i++) 
            _model.Slots[i].SetPicture(data.Pictures[i]);
    }
}