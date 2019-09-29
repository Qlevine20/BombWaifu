using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HighscoreMenu : MonoBehaviour
{
    public GameObject highScoreTextPrefab;

    public Transform content;

    List<GameManager.ScoreValue> scores;

    private void Start()
    {
        
    }

    void LoadScores()
    {
        string text = File.ReadAllText(GameManager.instance.filePath);
        string[] scoreVals = text.Split('\n');
        foreach(string score in scoreVals)
        {
            
        }
    }
}
