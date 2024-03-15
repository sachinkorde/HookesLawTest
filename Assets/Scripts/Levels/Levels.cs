using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "level", menuName = "ScriptableObjects/Level")]
public class Levels : ScriptableObject
{
    public List<LevelData> LevelData = new();
}

[System.Serializable]
public class LevelData
{
    public int levelNum;
    public GameObject levelprefab;
    public int coinsEarned;
}