using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string filePath;
    private int score = 0;

    private List<ScoreValue> scores;

    public class ScoreValue
    {
        public int scoreValue = 0;

        public static IComparer<ScoreValue> value;
    }

    private class sortScoreDescendingOrder : IComparer<ScoreValue>
    {
        int IComparer<ScoreValue>.Compare(ScoreValue a, ScoreValue b)
        {
            ScoreValue c1 = (ScoreValue)a;
            ScoreValue c2 = (ScoreValue)b;
            if (c1.scoreValue < c2.scoreValue)
                return 1;
            if (c1.scoreValue > c2.scoreValue)
                return -1;
            else
                return 0;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        DontDestroyOnLoad(this);
        scores = new List<ScoreValue>();

        filePath = Application.persistentDataPath + "/scores.txt";
    }

    public void AddToScore(int value)
    {
        score += value;
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public void SaveScore()
    {
        if (File.Exists(filePath))
        {
            string scoreInfo = File.ReadAllText(filePath);
            string[] scoreVals = scoreInfo.Split('\n');
            foreach(string scoreVal in scoreVals)
            {
                if(scoreVal != string.Empty)
                {
                    ScoreValue val = JsonUtility.FromJson<ScoreValue>(scoreVal);
                    scores.Add(val);
                }
                
            }
        }
        ScoreValue newScore = new ScoreValue();
        newScore.scoreValue = score;
        scores.Add(newScore);
        sortScoreDescendingOrder sorter = new sortScoreDescendingOrder();
        scores.Sort(sorter);
        string temp = "";
        foreach(ScoreValue scoreVal in scores)
        {
            temp += JsonUtility.ToJson(scoreVal) + "\n";
        }

        File.WriteAllText(filePath, temp);
    }
}
