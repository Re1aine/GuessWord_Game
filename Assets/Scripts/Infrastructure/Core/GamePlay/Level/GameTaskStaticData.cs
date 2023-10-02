using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameTaskStaticData : ScriptableObject
{
    public List<Sprite> Pictures = new List<Sprite>(4);
    public string Word;
    public bool _IsCompleted;
}

