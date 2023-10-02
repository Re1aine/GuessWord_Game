public class GamePlayState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly IGameFactory _gameFactory;
    private readonly IAudioService _audioService;

    public GamePlayState(GameStateMachine stateMachine,IGameFactory gameFactory, IAudioService audioService)
    {
        _stateMachine = stateMachine;
        _gameFactory = gameFactory;
        _audioService = audioService;
    }

    public void Enter()
    {
        _gameFactory
            .CreateUI<CoreUI>()
            .Construct(_stateMachine, _gameFactory, _audioService)
            .Initialize();
    }

    public void Exit()
    {
        _gameFactory
            .GetContext()
            .Get<CoreUI>()
            .Destroy();
    }
}