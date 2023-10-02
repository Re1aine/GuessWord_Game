using UnityEngine;

public class PictureSlotPresenter
{
    private readonly PictureSlotModel _model;
    private readonly PictureSlotView _view;

    public PictureSlotPresenter(PictureSlotModel model, PictureSlotView view)
    {
        _model = model;
        _view = view;

        _model.PictureChanged += _view.SetPicture;
    }

    public void SetPicture(Sprite sprite)
    {
        _model.SetPicture(sprite);
    }
}