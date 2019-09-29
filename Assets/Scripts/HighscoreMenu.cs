using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

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
            scoreText.GetComponent<Text>().text = "'" + score.name + "'" + " Score: " + score.scoreValue;
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
                GameManager.ScoreValue val = new GameManager.ScoreValue();
                GameManager.ScoreValue scoreInfo = JsonUtility.FromJson<GameManager.ScoreValue>(score);
                scores.Add(scoreInfo);
            }

        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
