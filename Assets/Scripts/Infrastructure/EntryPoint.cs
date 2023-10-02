using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _uiRoot;
    
    private void Awake()
    {
        IGameFactory gameFactory = new GameFactory(_uiRoot);
        IAudioService audioService = new AudioService(gameFactory);
        IAdsService adsService = new AdsService();
        
        var gameStateMachine = new GameStateMachine();
        var statesContainer = new GameStatesContainer(gameStateMachine, gameFactory, audioService, adsService);
        gameStateMachine.Init(statesContainer);
        gameStateMachine.Run();
    }
}