using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    public List<GameObject> mapTilesets;
    GameObject[,] currentGrid = new GameObject[5,5];

    public GameObject player;

    void Start()
    {
        for (int row = 0; row < currentGrid.GetLength(0); row++)
        {
            for (int col = 0; col < currentGrid.GetLength(1); col++)
            {
                currentGrid[row, col] = Instantiate(mapTilesets[Random.Range(0, mapTilesets.Count - 1)]);
                currentGrid[row, col].transform.position = new Vector3(col * currentGrid[row, col].transform.localScale.x, 0, row * currentGrid[row, col].transform.localScale.z);

                //If this is the center tile, move the player there
                if (row == (Mathf.Floor(currentGrid.GetLength(0) / 2f)) && col == (Mathf.Floor(currentGrid.GetLength(1) / 2f)))
                {
                    player.transform.position = new Vector3(currentGrid[row, col].transform.position.x, player.transform.position.y, currentGrid[row, col].transform.position.z);
                }
            }
        }
    }

    void Update()
    {
        
    }
}