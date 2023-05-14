using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] spawnPoints = new GameObject[32];
    public float spawnTime;
    public Player playerSizeScript;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawner");
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        // trash
        // worm
        // jelly fish
        // 
        while (true)
        {
            
            float size = playerSizeScript.size;
            // decide limit
            int limit = DecideLimit(size);

            int rndIndex = Random.Range(0, spawnPoints.Length-1);
            int rndPrefab = Random.Range(0, limit);

            GameObject enemy = Instantiate(enemyPrefabs[rndPrefab], spawnPoints[rndIndex].transform.position, Quaternion.identity);
            Size enemySize = enemy.GetComponent<Size>();
            if (enemySize != null)
            {
                enemySize.size += size / 6;
            }
            
            // direction they go is randomized
            enemy.transform.right = Random.insideUnitCircle.normalized;

            yield return new WaitForSeconds(spawnTime);
        }

        
    }

    int DecideLimit(float size)
    {
        if (size <= 6)
        {
            return 4;
        }
        else if (size <= 19)
        {
            return 5;
        }
        else if (size <= 36)
        {
            return 6;
        }
        else if (size <= 99)
        {
            return 8;
        }
        else if(size <= 175)
        {
            return 10;
        }
        else if(size <= 200)
        {
            return 11;
        }

        return 13;
    }

}
