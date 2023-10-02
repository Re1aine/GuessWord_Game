public class MenuState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly IGameFactory _gameFactory;

    public MenuState(GameStateMachine stateMachine, IGameFactory gameFactory)
    {
        _stateMachine = stateMachine;
        _gameFactory = gameFactory;
    }
    
    public void Enter()
    {
        _gameFactory
            .CreateUI<MenuUI>()
            .Construct(_stateMachine, _gameFactory)
            .Initialize();
    }

    public void Exit()
    {
        _gameFactory
            .GetContext()
            .Get<MenuUI>()
            .Destroy();
    }
}