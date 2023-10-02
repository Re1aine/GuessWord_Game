using System;
using UnityEngine;

public class WordSlotModel
{
    public event Action<char> LetterChanged;
    public event Action<Color> ColorChanged;
    
    private char _letter;
    private Color _colorFill;
    public KeyPresenter KeyPresenter { get; private set; }
    
    public char Letter
    {
        get => _letter;
        set
        {
            _letter = value;
            LetterChanged?.Invoke(_letter);
        }
    }

    public Color ColorFill
    {
        get => _colorFill;
        set
        {
            _colorFill = value;
            ColorChanged?.Invoke(_colorFill);
        }
    }
    
    public WordSlotModel(Color colorFill)
    {
        _colorFill = colorFill;
        _letter = ' ';
    }

    public void SetColor(Color color)
    {
        ColorFill = color;
    }

    public void SetLetter(char letter)
    {
        Letter = letter;
    }

    public void SetInputKey(KeyPresenter presenter)
    {
        KeyPresenter = presenter;
    }
}