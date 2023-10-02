using UnityEngine;

public interface IPictureSlotView
{
    PictureSlotType PictureSlotType { get; }
    void Interact();
    void SetPicture(Sprite sprite);
}