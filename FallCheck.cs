using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallCheck : MonoBehaviour
{    
    public List<GameObject> playersDead;
    public List<float> playersDeadTimer;
    public GameObject gameOverDisplay;
    public float fallSpeed = 0.25f;
    public float timerOver = 0.5f;
   public  int remainCount;
    public int playerCount;
    public int overFlag = 0;
    public FightManager fightScript;



    void Start()
    {
      fightScript = GameObject.Find("arena1").transform.GetChild(0).transform.gameObject.GetComponent<FightManager>();
      playerCount = GameObject.Find("Unit").transform.childCount;
      remainCount = playerCount;

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && other.isTrigger == false)
       { 
           playersDead.Add(other.gameObject);
           playersDeadTimer.Add(3f);
            remainCount --;
           
       }
    }
    void Update()
    {

        for(int i = 0 ; i<playersDead.Count ; i++)
        {   if(playersDeadTimer[i] > 0f)
            {
            playersDead[i].transform.Translate(0f,-fallSpeed,0f);
            playersDeadTimer[i] -= Time.deltaTime;
            }
            if(playersDeadTimer[i] <= 0f)
            {
                playersDead[i].SetActive(false);
            }
           
        }
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
