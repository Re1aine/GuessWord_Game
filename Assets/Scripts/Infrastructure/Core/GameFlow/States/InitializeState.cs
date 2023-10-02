public class InitializeState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly IGameFactory _gameFactory;

    public InitializeState(GameStateMachine stateMachine, IGameFactory gameFactory)
    {
        _stateMachine = stateMachine;
        _gameFactory = gameFactory;
    }

    public void Enter()
    {
        _gameFactory.CreatePlayer();
        _gameFactory
            .CreateLevel()
            .Init(_stateMachine);
            
        _stateMachine.Enter(GameStates.Menu);
    }

    public void Exit()
    {
        
    }
}