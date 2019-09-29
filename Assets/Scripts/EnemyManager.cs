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

    public LayerMask spawnMask;

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
        GetComponent<AudioSource>().Play();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.instance.SetScore(0);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
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
                Vector3 loc = FindPlaceToSpawnEnemy(8,10);
                if(loc != Vector3.zero)
                {
                    EnemyBehaviour eBehav = EnemyPool.Instance.Get();
                    eBehav.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(loc);
                    eBehav.gameObject.SetActive(true);
                }

            }

        }
        StartCoroutine(SpawnEnemy(time));
    }


    Vector3 FindPlaceToSpawnEnemy(int spawnLocationChecks, float SpawnDist)
    {

        for (int i = 0; i < spawnLocationChecks; i++)
        {
            float randLocationX = UnityEngine.Random.Range(player.position.x + (-SpawnDist), player.position.x + SpawnDist);
            float randLocationZ = UnityEngine.Random.Range(player.position.z + (-SpawnDist), player.position.z + SpawnDist);
            Vector3 newPos = new Vector3(randLocationX, 1.5f, randLocationZ);
            RaycastHit hit;
            if(Physics.Raycast(newPos,Vector3.down, out hit, 10f, spawnMask))
            {
                return newPos;
            }
            else
            {
                Debug.Log("coords: " + newPos);
            }
        }
        return Vector3.zero;
    }
}
