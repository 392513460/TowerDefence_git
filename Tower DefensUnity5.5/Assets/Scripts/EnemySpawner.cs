using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour {
    public Wave[] waves;
    public Transform START;
    public  float waveRate=0.25f;
    public static int CountEnemyAlive = 0;
    private Coroutine coroutine;
  public void Stop()
    {
        StopCoroutine(coroutine);
    }
    void Start()
    {
        coroutine = StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnEnemy());
    }
    Enemy e=new Enemy();
     IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i <wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                CountEnemyAlive++;
                //print(CountEnemyAlive);
                if (i != wave.count - 1)
                {
                    yield return new WaitForSeconds(wave.rate);
                    //CountEnemyAlive++;
                   
                }
                //CountEnemyAlive++;
               
            }
            while (CountEnemyAlive > 0)
            {
                yield return 0;
            }
              
           
            yield return new WaitForSeconds(waveRate);
        }
        GameManager.instance.Win();
    }
        
  
    }
	// Use this for initialization
	// Update is called once per frame
	

