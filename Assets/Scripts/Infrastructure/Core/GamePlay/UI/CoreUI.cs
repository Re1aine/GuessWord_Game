using System;
using TMPro;
using UnityEngine;

public class CoreUI : UIBase
{
    public PictureFieldView PictureFieldView => _pictureFieldView;
    public WorldFieldView WorldFieldView => _worldFieldView;
    public KeyboardView KeyboardView => _keyboardView;

    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private PictureFieldView _pictureFieldView;
    [SerializeField] private WorldFieldView _worldFieldView;
    [SerializeField] private KeyboardView _keyboardView;
    [SerializeField] private ResetWordFieldButton _resetWordFieldButton;
    [SerializeField] private BackToMenuButton _backToMenuButton;
    [SerializeField] private RewardedAdsButton _rewardedAdsButton;
    [SerializeField] private VictoryWindow _victoryWindow;
    
    private IAudioService _audioService;
    private GameStateMachine _stateMachine;
    private IGameFactory _gameFactory;

    public CoreUI Construct(GameStateMachine stateMachine, IGameFactory gameFactory, IAudioService audioService)
    {
        _stateMachine = stateMachine;
        _audioService = audioService;
        _gameFactory = gameFactory;

        return this;
    }

    protected override void Init()
    {
        _resetWordFieldButton.Construct(_worldFieldView, _audioService);
        _backToMenuButton.Construct(_stateMachine, _audioService);
        _victoryWindow.Construct(_stateMachine, _gameFactory);
        
        Initialized += _gameFactory
            .GetContext()
            .Get<LevelPresenter>()
            .Initialize;
        
        Initialized += _gameFactory
            .GetContext()
            .Get<PlayerPresenter>()
            .Initialize;
    }

    public void SetLevel(int count)
    {
        _level.text = count.ToString();
    }

    private void OnEnable()
    {
        _keyboardView.KeyClicked += _worldFieldView.Fill;
        _worldFieldView.SlotClicked += _keyboardView.SetKeyState;
        _rewardedAdsButton.RewardAdsComplete += _worldFieldView.RemoveUnnecessaryLetters;
        _rewardedAdsButton.RewardAdsComplete += _keyboardView.RemoveUnnecessaryLetters;
        _worldFieldView.StartCheckoutWord += _keyboardView.OnWordStartCheckout;
        _worldFieldView.CompleteCheckoutWord += _keyboardView.OnWordCompleteCheckout;
        _worldFieldView.StartCheckoutWord += _resetWordFieldButton.OnWordStartCheckout;
        _worldFieldView.CompleteCheckoutWord += _resetWordFieldButton.OnWordCompleteCheckout;
        _worldFieldView.StartCheckoutWord += _backToMenuButton.OnWordStartCheckout;
        _worldFieldView.CompleteCheckoutWord += _backToMenuButton.OnWordCompleteCheckout;
        _worldFieldView.SuccessCompleteCheckout += _victoryWindow.SuccessCompleteCheckout;
        _worldFieldView.StartCheckoutWord += _rewardedAdsButton.StartCheckoutWord;
        _worldFieldView.CompleteCheckoutWord += _rewardedAdsButton.CompleteCheckoutWord;
    }

    private void OnDisable()
    {
        _keyboardView.KeyClicked -= _worldFieldView.Fill;
        _worldFieldView.SlotClicked -= _keyboardView.SetKeyState;
        _rewardedAdsButton.RewardAdsComplete -= _keyboardView.RemoveUnnecessaryLetters;
        _worldFieldView.StartCheckoutWord -= _keyboardView.OnWordStartCheckout;
        _worldFieldView.CompleteCheckoutWord -= _keyboardView.OnWordCompleteCheckout;
        _worldFieldView.StartCheckoutWord -= _resetWordFieldButton.OnWordStartCheckout;
        _worldFieldView.CompleteCheckoutWord -= _resetWordFieldButton.OnWordCompleteCheckout;
        _worldFieldView.StartCheckoutWord -= _backToMenuButton.OnWordStartCheckout;
        _worldFieldView.CompleteCheckoutWord -= _backToMenuButton.OnWordCompleteCheckout;
        _worldFieldView.SuccessCompleteCheckout -= _victoryWindow.SuccessCompleteCheckout;
        _worldFieldView.StartCheckoutWord -= _rewardedAdsButton.StartCheckoutWord;
        _worldFieldView.CompleteCheckoutWord -= _rewardedAdsButton.CompleteCheckoutWord;
    }
}