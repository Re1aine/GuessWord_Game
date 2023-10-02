using System;

public class GameStateMachine
{
    private IState _currentState;
    private GameStatesContainer _statesContainer;

    public void Init(GameStatesContainer container)
    {
        _statesContainer = container;
    }

    public void Run()
    {
        Enter(GameStates.Initialize);
    }
    
    public void Enter(GameStates state)
    {
        _currentState?.Exit();
        
        switch (state)
        {
            case GameStates.Initialize: SwitchState(GameStates.Initialize);
                break;
            case GameStates.Menu: SwitchState(GameStates.Menu);
                break;
            case GameStates.GamePlay: SwitchState(GameStates.GamePlay);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, $"{state} doesn't exist");
        }
    }

    private void SwitchState(GameStates state)
    {
        _currentState = state switch
        {
            GameStates.Initialize => _statesContainer.Get<InitializeState>(),
            GameStates.Menu => _statesContainer.Get<MenuState>(),
            GameStates.GamePlay => _statesContainer.Get<GamePlayState>(),
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, $"{state} doesn't exist")
        };
        _currentState.Enter();
    }
}