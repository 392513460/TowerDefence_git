using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public GameObject endUi;
    public static GameManager instance;
    private EnemySpawner enemySpawner;
    void Awake()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        instance = this;
    }
    public  void Win()
    {
        endMessage.text = "胜利";
        endUi.SetActive(true);
    }
    public Text endMessage;
    public void Failed()
    {
        enemySpawner.Stop();
        endMessage.text = "失败";
        endUi.SetActive(true);
    }
   public void OnButtonRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void OnButtonMenue()
    {
        SceneManager.LoadScene(0);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
