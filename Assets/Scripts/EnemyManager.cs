using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public Transform player;

    private float timeModder = 0;
    private float timeChange = 5;

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
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
