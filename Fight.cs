using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public List<GameObject> players;
    public List<GameObject> enemies;
    public List<GameObject> playersTarget;
  //  public List<GameObject> enemiesTarget = new List<GameObject>(6);
     public GameObject[] enemiesTarget  = new GameObject[6];
    public List<int> playersFlag;
    public List<int> enemiesFlag = new List<int>(6);
    public List<int> playersHealth;
    public List<int> enemiesHealth = new List<int>(6);
    public int playersAssignFlag = 0;
    public float forwardSpeed = 0.05f;
    public float unitForwardSpeed = 0.13f;
    public int runningEnemyFlag = 0;
    public int collideFlag = 0;
    public List<int> playersCoroutineFlag;

    public GameObject unit;

    public FallCheck checkScript;
    // Start is called before the first frame update
    void Start()
    {  
        
        for(int i=0;i<6;i++)
        {
            enemiesFlag.Add(0);
        
            enemiesHealth.Add(100);
        }
        checkScript = this.gameObject.transform.parent.GetComponent<FallCheck>();
        unit = GameObject.Find("Unit");

            foreach(Transform child in unit.transform)
            {
                child.gameObject.GetComponent<Animator>().SetBool("isRunning",true);
            }
    
    }
    
      void OnTriggerEnter(Collider other)
    {
        
        
            if(other.gameObject.tag == "Player" && other.isTrigger == false)
            {
                players.Add(other.gameObject);
                playersFlag.Add(0);
                playersHealth.Add(100);
                if(players.Count == checkScript.remainCount)
                {
                    playersAssignFlag = 1;
                    playersTarget = new List<GameObject>(players.Count);
                    playersCoroutineFlag = new List<int>(players.Count);
                    for(int i=0;i<players.Count;i++)
                    {
                        playersCoroutineFlag.Add(0);
                    }
                }
            }
        
    }
    
    void Update()
    { 
        //unit movement
         if(playersAssignFlag == 0)
          {  
        unit.transform.Translate(0f,0f,unitForwardSpeed * Time.timeScale);
          }

        if(playersAssignFlag == 1)
        {   
            for(int i = 0; i < players.Count; i++)
            {  
                  
                 if(playersFlag[i] == 0)
              { 
                  playersTarget.Insert(i,enemies[Random.Range(0,enemies.Count)]);
                 //playersTarget.Add(enemies[Random.Range(0,enemies.Count)]);
                 if(collideFlag == 0)
             { 
                players[i].transform.GetChild(0).gameObject.SetActive(true);
                playersTarget[i].transform.GetChild(0).gameObject.SetActive(true);
                if(i == players.Count - 1)
                collideFlag = 1;
             }
                 
                  players[i].GetComponent<CloseCheck>().enabled = true;
                  players[i].GetComponent<CloseCheck>().target = playersTarget[i];
                  for(int m=0;m<enemies.Count;m++)
                  {   
                     // Debug.Log(enemies.Count);
                      if(playersTarget[i].name == enemies[m].name)
                     { 
                       enemiesTarget[m] = players[i];
                       enemiesFlag[m] = 1;
                     }
                  }
                   playersFlag[i] = 1;
              }
            } 
                if(runningEnemyFlag == 0)
                {
            for(int j=0;j<enemies.Count;j++)
            {   
                 enemies[j].GetComponent<Animator>().SetBool("isRunning",true);
                 if(j == enemies.Count - 1)
                 runningEnemyFlag = 1;
               
            }
                }
        
        for(int i = 0 ; i < players.Count ; i++)
        {  // players[i].transform.position = Vector3.MoveTowards( players[i].transform.position,playersTarget[i].transform.position,forwardSpeed * Time.timeScale);
             if(players[i].GetComponent<CloseCheck>().closeFlag == 0)
            {
              players[i].transform.Translate(players[i].transform.forward * forwardSpeed);
              players[i].transform.LookAt(playersTarget[i].transform);
            }
            if(players[i].GetComponent<CloseCheck>().closeFlag == 1)
            { 
                playersCoroutineFlag[i] = 1;
               
             
            }
        }
         for(int i = 0 ; i < enemies.Count ; i++)
        { //  enemies[i].transform.position = Vector3.MoveTowards( enemies[i].transform.position,enemiesTarget[i].transform.position,forwardSpeed * Time.timeScale);
           if(enemiesTarget[i] != null )
          { 
              if(enemiesTarget[i].GetComponent<CloseCheck>().closeFlag == 0)
         { 
          enemies[i].transform.Translate(enemies[i].transform.forward * -forwardSpeed);
          enemies[i].transform.LookAt(enemiesTarget[i].transform);
         }

          }
         
        }
        }
    }

   
}
