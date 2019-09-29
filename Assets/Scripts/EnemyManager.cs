using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public Transform player;
    public TextMeshProUGUI scoreText;
    public GameObject enemyPrefab;
    private float timeModder = 0;
    private float timeChange = 5;

    public float spawnChance;
    public float spawnRange = 50;

    [HideInInspector]
    public float speedMod = 0;

    [HideInInspector]
    public bool collided = false;

    public GameObject GameOverPanel;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        GameOverPanel.SetActive(false);
        GameManager.instance.SetScore(0);
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        StartCoroutine(SpawnEnemy(2));
    }

    private void Update()
    {
        timeModder += Time.deltaTime;
        if(timeModder > timeChange)
        {
            timeModder = 0;
            speedMod += .2f;
        }


        
    }

    public void CollidedWithPlayer()
    {
        collided = true;
        GameOverPanel.SetActive(true);
        scoreText.text = "Score: " + GameManager.instance.GetScore();
        GameManager.instance.SaveScore();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.instance.SetScore(0);
    }

    IEnumerator SpawnEnemy(int time)
    {
        yield return new WaitForSeconds(time);
        int rand = UnityEngine.Random.Range(0, 100);
        int randEnemySize = UnityEngine.Random.Range(3, 8);
        if (rand < spawnChance)
        {
            float randX = UnityEngine.Random.Range(-spawnRange, spawnRange);
            float randZ = UnityEngine.Random.Range(-spawnRange, spawnRange);
            for (int i = 0; i < randEnemySize; i++)
            {
                EnemyBehaviour eBehav = EnemyPool.Instance.Get();
                eBehav.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(new Vector3(randX + i,1.5f,randZ - i));
                eBehav.gameObject.SetActive(true);
            }

        }
        StartCoroutine(SpawnEnemy(time));
    }
}
