using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FightManager : MonoBehaviour
{
  public List<GameObject> players;
  public List<GameObject> enemies;
  public int runningEnemyFlag = 0;
  public GameObject unit;
  public float unitForwardSpeed = 0.13f;
  public int attackFlag = 0;
  public int enemiesRemaining;
  public int playersRemaining;
  public List<int> enemiesHealth;
  public List<int> playersHealth;
  public List<int> playersDeathFlag;
  public List<int> enemiesDeathFlag;
  public ParticleSystem DeathBlue;
  public ParticleSystem DeathRed;
  public GameObject gameOverDisplay;
  public GameObject levelCompleteDisplay;
  public int confettiFlag = 0;
  public SmoothFollow smooth;
  public ParticleSystem confetti;
  public TextMeshProUGUI levelText;
  public FallCheck checkScript;

  public int gameOver = 0;
  public int endFlag =  0;

  // Start is called before the first frame update
    void Start()
    {   levelText = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>();
        levelText.text = "LEVEL " + PlayerPrefs.GetInt("LevelNumber").ToString();
        smooth = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
        checkScript = this.gameObject.transform.parent.GetComponent<FallCheck>();

        foreach(Transform child in GameObject.Find("Enemies").transform)
    {
        enemies.Add(child.gameObject);
    }
        
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
                if(SceneManager.GetActiveScene().buildIndex < 6)
                {
                if(players.Count == checkScript.remainCount)
                {  attackFlag = 1;
                   playersHealth = new List<int>(players.Count);
                   enemiesHealth = new List<int>(enemies.Count); 
                   playersDeathFlag = new List<int>(players.Count);
                   enemiesDeathFlag = new List<int>(enemies.Count); 
                    for(int i=0;i<players.Count;i++)
                   {
                        players[i].GetComponent<Individual>().enabled = true;
                        playersDeathFlag.Add(0);
                        playersHealth.Add(100);
                   }
                   for(int i=0;i<enemies.Count;i++)
                   {
                        enemies[i].GetComponent<IndividualEnemy>().enabled = true;
                        enemiesDeathFlag.Add(0);
                        enemiesHealth.Add(100);
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
                }
            }
           else if(SceneManager.GetActiveScene().buildIndex >= 6 )
                {
                if(players.Count == DeadCheck.remainCount)
                {  attackFlag = 1;
                   playersHealth = new List<int>(players.Count);
                   enemiesHealth = new List<int>(enemies.Count); 
                   playersDeathFlag = new List<int>(players.Count);
                   enemiesDeathFlag = new List<int>(enemies.Count); 
                    for(int i=0;i<players.Count;i++)
                   {
                        players[i].GetComponent<Individual>().enabled = true;
                        playersDeathFlag.Add(0);
                        playersHealth.Add(100);
                   }
                   for(int i=0;i<enemies.Count;i++)
                   {
                        enemies[i].GetComponent<IndividualEnemy>().enabled = true;
                        enemiesDeathFlag.Add(0);
                        enemiesHealth.Add(100);
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
                }
            }

            }
        
    }


    // Update is called once per frame
    void Update()
    { 
         if(SceneManager.GetActiveScene().buildIndex < 6)
         {
         if(attackFlag == 0 && checkScript.remainCount != 0)
         unit.transform.Translate(0f,0f,unitForwardSpeed * Time.timeScale * Time.deltaTime);
         }
         else if(SceneManager.GetActiveScene().buildIndex >= 6)
        {
            if(attackFlag == 0 && DeadCheck.remainCount != 0)
        unit.transform.Translate(0f,0f,unitForwardSpeed * Time.timeScale * Time.deltaTime);
        }
        
      if(attackFlag == 1)
      {   enemiesRemaining = enemies.Count;
         playersRemaining = players.Count;

        for(int i=0;i<enemies.Count;i++)
        {
            
            if(enemies[i].activeInHierarchy == false)
           { 
               if(enemiesDeathFlag[i] == 0)
             {
                Instantiate(DeathRed,enemies[i].transform.position + new Vector3(0f,0.4f,0f), Quaternion.identity);
                Taptic.Medium();
                enemiesDeathFlag[i] = 1;
             }
            enemiesRemaining -= 1;
           }
        }
        for(int i=0;i<players.Count;i++)
        {
           
           if(players[i].activeInHierarchy == false)
           {
                if(playersDeathFlag[i] == 0)
             {
                Instantiate(DeathBlue,players[i].transform.position + new Vector3(0f,0.4f,0f), Quaternion.identity);
                Taptic.Medium();
                playersDeathFlag[i] = 1;
             }

               playersRemaining -= 1;
           }    
        }
           
      }

      //GAME OVER
      if(gameOver == 1)
      {
          foreach(GameObject player in players)
          {
           player.GetComponent<Animator>().SetBool("isRunning",false);
           player.GetComponent<Animator>().SetBool("isHitting",false);
         //  player.GetComponent<Individual>().trail.SetActive(false);
         
          }
          foreach(GameObject enemy in enemies)
          {
               enemy.GetComponent<Animator>().SetBool("isRunning",false);
           enemy.GetComponent<Animator>().SetBool("isHitting",false);
        // enemy.GetComponent<IndividualEnemy>().trail.SetActive(false);
          
          }
          if(playersRemaining == 0 && endFlag == 0)
          {
              gameOverDisplay.SetActive(true);
              Debug.Log("Level " + "Failed , " + PlayerPrefs.GetInt("LevelNumber"));
              //Time.timeScale = 0.6f;
              endFlag = 1;
          }
          else if(enemiesRemaining == 0 && endFlag == 0)
          {
              levelCompleteDisplay.SetActive(true);
              Debug.Log("Level " + "Complete , " + PlayerPrefs.GetInt("LevelNumber"));
              //Time.timeScale = 0.6f;
              endFlag = 1;
          }
      }
    }

    void LateUpdate()
    {
        if(gameOver == 1 && confettiFlag == 0 && playersRemaining != 0)
       {
            Instantiate(confetti,smooth.targetPosition + new Vector3(0f,3f,0f), Quaternion.identity);
            //Debug.Log("lol");
            confettiFlag = 1;
       }
    }
}
