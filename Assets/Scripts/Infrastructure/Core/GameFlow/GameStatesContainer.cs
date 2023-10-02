using System;
using System.Collections.Generic;

public class GameStatesContainer
{
    private readonly Dictionary<Type, IState> _states;

    public GameStatesContainer(GameStateMachine stateMachine, IGameFactory gameFactory, IAudioService audioService, IAdsService adsService)
    {
        _states = new Dictionary<Type, IState>()
        {
            [typeof(InitializeState)] = new InitializeState(stateMachine, gameFactory),
            [typeof(MenuState)] = new MenuState(stateMachine, gameFactory),
            [typeof(GamePlayState)] = new GamePlayState(stateMachine, gameFactory, audioService)
        };
    }
    
    public IState Get<T>() where T : IState
    {
        return _states[typeof(T)];
    }
}