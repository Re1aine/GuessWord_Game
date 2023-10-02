using UnityEngine;
using UnityEngine.UI;

public class VictoryWindow : MonoBehaviour
{
    [SerializeField] private Button _nextButton;
    
    private GameStateMachine _stateMachine;
    private IGameFactory _gameFactory;
    
    private void Awake()
    {
        _nextButton.onClick.AddListener(() => _stateMachine.Enter(GameStates.GamePlay));
    }   

    public void Construct(GameStateMachine stateMachine, IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _stateMachine = stateMachine;
    }

    public void SuccessCompleteCheckout()
    {
        _gameFactory
            .GetContext()
            .Get<LevelPresenter>()
            .SetNextData();
        
        gameObject.SetActive(true);
    }
}