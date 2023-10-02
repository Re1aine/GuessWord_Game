using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelPresenter : Presenter
{
    private PictureFieldPresenter _pictureFieldPresenter;
    private WordFieldPresenter _wordFieldPresenter;
    private KeyboardPresenter _keyboardPresenter;

    private readonly LevelModel _model;
    private readonly IGameFactory _gameFactory;
    private CoreUI _ui;

    private GameStateMachine _stateMachine;
    private readonly Dictionary<int, LevelStaticData> _levels;

    public LevelPresenter(LevelModel model, IGameFactory gameFactory)
    {
        _model = model;
        _gameFactory = gameFactory;
        
        _model.LevelChanged += LoadLevel;
        _model.GameTaskChanged += LoadTask;
        
        _levels = Resources.LoadAll<LevelStaticData>("StaticData")
            .ToDictionary(x => x.Id, x => x);
    }

    public void Init(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public void Initialize(UIBase ui)
    {
        _ui = (CoreUI)ui;
        Run();
    }

    private void Run()
    {
        Create();
        LoadLevel(_model.GetLevelData());
    }

    public void SetLevelData(LevelStaticData data)
    {
        _model.SetLevel(data);
        _model.SetTask(data.Tasks[0]);
    }
    
    private void Create()
    {
        _pictureFieldPresenter = _gameFactory.CreatePictureField(_ui);
        _keyboardPresenter = _gameFactory.CreateKeyboard(_ui);
        _wordFieldPresenter = _gameFactory.CreateWordField(_ui, _keyboardPresenter, this);
    }

    private void LoadLevel(LevelStaticData data)
    {
        SetLevelId();
        LoadTask(_model.CurrentTask);
    }
    
    private void LoadTask(GameTaskStaticData data)
    {
        _pictureFieldPresenter.LoadData(data);
        _wordFieldPresenter.LoadData(data);
        _keyboardPresenter.LoadData(data);
    }
    
    private void SetLevelId()
    {
        _ui.SetLevel(_model.LevelStaticData.Id);
    }
        
    public void SetNextData()
    {
        if (!IsCanNext())
        {
            _stateMachine.Enter(GameStates.Menu);
            return;
        }
        
        if (!IsLastTask())
            _model.SetTask(_model.LevelStaticData.Tasks[GetTaskIndex(_model.CurrentTask) + 1]);
        else
        {
            _model.SetLevel(_levels[_model.LevelStaticData.Id + 1]);
            _model.SetTask(_model.LevelStaticData.Tasks[0]);
        }
    }
    
    private bool IsLastTask()
    {
        int index = _model.LevelStaticData.Tasks.IndexOf(_model.CurrentTask);
        return index == _model.LevelStaticData.Tasks.Count - 1;
    }

    private bool IsCanNext()
    {
       return _model.LevelStaticData.Id <= _levels[_levels.Count - 1].Id;
    }

    private int GetTaskIndex(GameTaskStaticData task)
    {
        return _model.LevelStaticData.Tasks.IndexOf(task);
    }

    public void SetCompleted()
    {
        if (!IsLastTask()) 
            _model.CurrentTask._IsCompleted = true;
        else
            _model.LevelStaticData.IsCompleted = true;

        EditorUtility.SetDirty(_model.LevelStaticData);
    }
}