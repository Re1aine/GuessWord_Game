using UnityEngine;

public class  MenuUI : UIBase
{
    [SerializeField] private Transform _container; 
        
    private IGameFactory _gameFactory;
    private GameStateMachine _stateMachine;

    protected override void Init()
    {
        CreateLevelSlots();
        
        Initialized += _gameFactory
            .GetContext()
            .Get<PlayerPresenter>()
            .Initialize;
    }
    
    public MenuUI Construct(GameStateMachine stateMachine, IGameFactory gameFactory)
    {
        _stateMachine = stateMachine;
        _gameFactory = gameFactory;

        return this;
    }

    private void CreateLevelSlots()
    {
        LevelStaticData[] levels = Resources.LoadAll<LevelStaticData>("");
        
        for (int i = 1; i <= levels.Length; i++)
        {
            var slot = _gameFactory.CreateLevelSlot(i, _container);
            slot.Construct(_stateMachine, _gameFactory);
        }
    }
}