using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelStaticData : ScriptableObject
{
    public int Id;
    public List<GameTaskStaticData> Tasks;
    public bool IsCompleted;
}