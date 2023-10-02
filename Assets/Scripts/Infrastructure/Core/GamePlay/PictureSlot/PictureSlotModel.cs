using System;
using UnityEngine;

public class PictureSlotModel
{
    public event Action<Sprite> PictureChanged;

    private Sprite _picture;

    public Sprite Picture
    {
        get => _picture;
        private set
        {
            _picture = value;
            PictureChanged?.Invoke(_picture);
        }
    }

    public void SetPicture(Sprite picture)
    {
        Picture = picture;
    }
}