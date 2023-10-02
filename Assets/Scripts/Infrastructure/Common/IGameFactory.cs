using UnityEngine;

public interface IGameFactory
{
    GameFactoryContext GetContext();
    void Init(IAudioService audioService);
    T CreateUI<T>() where T : UIBase;
    PlayerPresenter CreatePlayer();
    LevelPresenter CreateLevel();
    PictureFieldPresenter CreatePictureField(CoreUI ui);
    PictureSlotPresenter CreatePictureSlot(PictureSlotView view);
    PictureSlotPresenter CreatePictureSlot(PictureSlotType type, Transform parent);
    WordFieldPresenter CreateWordField(CoreUI ui, KeyboardPresenter keyboard, LevelPresenter levelPresenter);
    WordSlotPresenter CreateWordSlot(Transform parent);
    KeyboardPresenter CreateKeyboard(CoreUI ui);
    KeyPresenter CreateKey(KeyView view);
    AudioPlayer CreateAudioPlayer();
    LevelSlot CreateLevelSlot(int id, Transform parent);
}