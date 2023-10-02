using System;

public class KeyModel
{
    public event Action<char> LetterChanged;
    
    private char _letter = ' ';

    public char Letter
    {
        get => _letter;
        private set
        {
            _letter = value;
            LetterChanged?.Invoke(_letter);
        }
    }

    public void SetLetter(char letter)
    {
        Letter = letter;
    }
}