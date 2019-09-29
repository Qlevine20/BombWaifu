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
        scores = new List<GameManager.ScoreValue>();
        LoadScores();
        foreach(GameManager.ScoreValue score in scores)
        {
            GameObject scoreText = Instantiate(highScoreTextPrefab);
            scoreText.transform.SetParent(content);
            scoreText.transform.SetAsLastSibling();
            scoreText.GetComponent<Text>().text = "Score: " + score.scoreValue;
        }
    }

    void LoadScores()
    {
        string text = File.ReadAllText(GameManager.instance.filePath);
        string[] scoreVals = text.Split('\n');
        foreach(string score in scoreVals)
        {
            if(score != string.Empty)
            {
                Debug.Log(score);
                GameManager.ScoreValue val = new GameManager.ScoreValue();
                val.scoreValue = JsonUtility.FromJson<GameManager.ScoreValue>(score).scoreValue;
                scores.Add(val);
            }

        }
    }
}
