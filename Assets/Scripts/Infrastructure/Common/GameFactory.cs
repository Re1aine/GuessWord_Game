using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameFactory : IGameFactory
{
    private readonly Dictionary<PictureSlotType, PictureSlotView> _slots;

    private IAudioService _audioService;
    private readonly Transform _uiRoot;

    private PlayerPresenter _player;
    private KeyboardPresenter _keyboard;
    private WordFieldPresenter _wordField;

    private UIBase _uiBase;
    private readonly GameFactoryContext _context;
    
    public void Init(IAudioService audioService)
    {
        _audioService = audioService;
    }
    
    public GameFactory(Transform uiRoot)
    {
        _uiRoot = uiRoot;
        _context = new GameFactoryContext();

        _slots = Resources.LoadAll<PictureSlotView>("PictureSlot")
            .ToDictionary(x => x.PictureSlotType, x => x);
    }

    public GameFactoryContext GetContext()
    {
        return _context;
    }
    
    public T CreateUI<T>() where T : UIBase
    {
        var prefab = Resources.Load<T>("UI/" + typeof(T));
        var ui =  Object.Instantiate(prefab, _uiRoot);
        _context.Register(ui);
        ui.Destroyed += _context.UnRegister<T>;
        return ui;
    }

    public PlayerPresenter CreatePlayer()
    {
        var model = new PlayerModel(10);
        var presenter = new PlayerPresenter(model);
        
        _player = presenter;
        
        _context.Register(presenter);
        
        return presenter;
    }

    public LevelPresenter CreateLevel()
    {
        var model = new LevelModel();
        var level =  new LevelPresenter(model, this);
        
        _context.Register(level);

        return level;
    }

    public PictureFieldPresenter CreatePictureField(CoreUI ui)
    {
        var model = new PictureFieldModel();
        var view = ui.PictureFieldView;
        var presenter =  new PictureFieldPresenter(model, view, this);
        view.Construct(presenter);

        return presenter;
    }

    public PictureSlotPresenter CreatePictureSlot(PictureSlotView view)
    {
        var model = new PictureSlotModel();
        view.Construct(_player);
        
        return new PictureSlotPresenter(model, view);
    }

    public PictureSlotPresenter CreatePictureSlot(PictureSlotType type, Transform parent)
    {
        var prefab = _slots[type];
        
        var model = new PictureSlotModel();
        var view = Object.Instantiate(prefab, parent);
        
        return new PictureSlotPresenter(model, view);
    }

    public WordFieldPresenter CreateWordField(CoreUI ui, KeyboardPresenter keyboard, LevelPresenter levelPresenter)
    {
        var model = new WordFieldModel(keyboard);
        var view = ui.WorldFieldView;
        var presenter = new WordFieldPresenter(model, view, levelPresenter);
        view.Construct(this, presenter, _audioService);

        return presenter;
    }

    public WordSlotPresenter CreateWordSlot(Transform parent)
    {
        var prefab = Resources.Load<WordSlotView>("WordSlot");

        var model = new WordSlotModel(Color.gray);
        var view = Object.Instantiate(prefab, parent);
        var presenter = new WordSlotPresenter(model, view);
        view.Construct(presenter, _audioService);

        return presenter;
    }

    public KeyboardPresenter CreateKeyboard(CoreUI ui)
    {
        var model = new KeyboardModel();
        var view = ui.KeyboardView;
        var presenter =  new KeyboardPresenter(model, view, this);
        view.Construct(presenter);

        return presenter;
    }

    public KeyPresenter CreateKey(KeyView view)
    {
        var model = new KeyModel();
        var presenter =  new KeyPresenter(model, view);
        view.Construct(presenter, _audioService);
        
        return presenter;
    }
    
    public AudioPlayer CreateAudioPlayer()
    {
        var prefab = Resources.Load<AudioPlayer>("AudioPlayer");
        return Object.Instantiate(prefab);
    }

    public LevelSlot CreateLevelSlot(int id, Transform parent)
    {
        var prefab = Resources.Load<LevelSlot>("UI/LevelSlot");
        var levelsData = Resources.LoadAll<LevelStaticData>("StaticData")
            .ToDictionary(x => x.Id, x => x);
        
        var level = Object.Instantiate(prefab, parent);
        level.SetData(levelsData[id]);

        return level;
    }
}

public class GameFactoryContext
{
    private readonly Dictionary<Type, View> _views = new();
    private readonly Dictionary<Type, Presenter> _presenters = new();
    
    public void Register<T>(T instance)
    {
        Type instanceType = typeof(T);
        
        if (instanceType.IsSubclassOf(typeof(View)) && !_views.ContainsKey(instanceType))
            _views.Add(instanceType, instance as View);
        else if (instanceType.IsSubclassOf(typeof(Presenter)) && !_presenters.ContainsKey(instanceType))
            _presenters.Add(instanceType, instance as Presenter);
        else
            Debug.LogWarning($"Instance of type {instanceType} is already registered.");
    }

    public void UnRegister<T>()
    {
        Type targetType = typeof(T);

        if (targetType.IsSubclassOf(typeof(View)) && _views.ContainsKey(targetType))
            _views.Remove(targetType);
        else if (targetType.IsSubclassOf(typeof(Presenter)) && _presenters.ContainsKey(targetType))
            _presenters.Remove(targetType);
        else
            Debug.LogWarning($"Instance of type {targetType} is not registered.");
    }

    public void Cleanup()
    {
        _views.Clear();
        _presenters.Clear();
    }
    
    public T Get<T>() where T : class
    {
        Type target = typeof(T);

        if (_views.TryGetValue(target, out var view))
            return view as T;

        if (_presenters.TryGetValue(target, out var presenter))
            return presenter as T;

        return null;
    }
}

public abstract class View : MonoBehaviour
{
    
}

public abstract class Presenter
{
    
}