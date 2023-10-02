using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _levelId;

    private LevelStaticData _levelStaticData;
    private GameStateMachine _stateMachine;
    private IGameFactory _gameFactory;

    public void Construct(GameStateMachine stateMachine, IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _stateMachine = stateMachine;
    }
    
    private void SetId(int id)
    {
        _levelId.text = id.ToString();
    }

    private void SetLevelData(LevelStaticData data)
    {
        _levelStaticData = data;
    }

    public void SetData(LevelStaticData data)
    {
        SetLevelData(data);
        SetId(_levelStaticData.Id);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _gameFactory
            .GetContext()
            .Get<LevelPresenter>()
            .SetLevelData(_levelStaticData);
        
        _stateMachine.Enter(GameStates.GamePlay);
    }
}
