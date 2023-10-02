using System.Collections.Generic;
using System.Linq;

public class KeyboardPresenter
{
    private readonly KeyboardModel _model;
    private readonly KeyboardView _view;
    private readonly GameFactory _gameFactory;

    public KeyboardPresenter(KeyboardModel model, KeyboardView view, GameFactory gameFactory)
    {
        _model = model;
        _view = view;
        _gameFactory = gameFactory;

        CreateKeys();
    }

    private void CreateKeys()
    {
        List<KeyPresenter> keys = new List<KeyPresenter>(_view.GetKeys().Count);
        
        for (int i = 0; i < _view.GetKeys().Count; i++) 
            keys.Add(_gameFactory.CreateKey(_view.GetKeys()[i]));

        _model.SetKeys(keys);
    }
    
    public void LoadData(GameTaskStaticData data)
    {
        SetKeysAvailableState();   
        FillKeyBoard(data.Word);
        _model.TaskStaticData = data;
    }

    private void FillKeyBoard(string word)
    {
        FillTaskWord(word);
        FillNonTaskLetter();
    }

    private void FillTaskWord(string word)
    {
        var chars = new List<char>(word.ToCharArray());
        _model.Keys.Shuffle();
        chars.Shuffle();

        for (int i = 0; i < chars.Count; i++) 
            _model.Keys[i].PutLetter(chars[i]);
    }
    
    public void RemoveUnnecessaryLetters()
    {
        string word = _model.TaskStaticData.Word;

        foreach (var key in _model.Keys.Where(key => !word.Contains(key.GetLetter()))) 
            key.SetNonAvailableState();
    }

    private void FillNonTaskLetter()
    {
        foreach (var key in _model.Keys.Where(x => x.IsEmpty())) 
            key.PutLetter(Extentions.GetRandomAlphabetLetter());
    }
    
    private void SetKeysAvailableState()
    {
        _model.Keys.ForEach(x => x.SetAvailableState());
    }
}