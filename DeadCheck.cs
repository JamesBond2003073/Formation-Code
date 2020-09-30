using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCheck : MonoBehaviour
{ 
    public static int remainCount;
    public int playerCount;
    public int overFlag = 0;
    public float timerOver = 0.5f;
    public FightManager fightScript;
    public GameObject gameOverDisplay;
    // Start is called before the first frame update
    void Start()
    {
        fightScript = GameObject.Find("arena1").transform.GetChild(0).transform.gameObject.GetComponent<FightManager>();
      playerCount = GameObject.Find("Unit").transform.childCount;
      remainCount = playerCount;

    }

    // Update is called once per frame
    void Update()
    {
         if(remainCount == 0 && fightScript.attackFlag == 0 )
    {  timerOver -= Time.deltaTime;
    if(timerOver <= 0f && overFlag == 0)
        {
        gameOverDisplay.SetActive(true);
        Debug.Log("Level " + "Failed , " + PlayerPrefs.GetInt("LevelNumber"));
        Time.timeScale = 0.3f;
        overFlag = 1;
        }
        
    }
    }
}
