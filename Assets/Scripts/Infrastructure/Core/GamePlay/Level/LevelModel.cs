using System;

public class LevelModel
{
    public event Action<LevelStaticData> LevelChanged;
    public event Action<GameTaskStaticData> GameTaskChanged; 
    
    public LevelStaticData LevelStaticData
    {
        get => _levelStaticData;
        set
        {
            _levelStaticData = value;
            LevelChanged?.Invoke(_levelStaticData);
        }
    }

    public GameTaskStaticData CurrentTask
    {
        get => _currentTask;
        private set
        {
            _currentTask = value;
            GameTaskChanged?.Invoke(_currentTask);
        }
    }

    private LevelStaticData _levelStaticData;
    private GameTaskStaticData _currentTask;
    
    public void SetLevelData(LevelStaticData data)
    {
        LevelStaticData = data;
    }

    public void SetCurrentTask(GameTaskStaticData data)
    {
        CurrentTask = data;
    }
    
    
    public void SetLevel(LevelStaticData data)
    {
        _levelStaticData = data;
    }

    public void SetTask(GameTaskStaticData data)
    {
        _currentTask = data;
    }

    public LevelStaticData GetLevelData()
    {
        return _levelStaticData;
    }
}