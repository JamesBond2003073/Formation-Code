using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualEnemy : MonoBehaviour
{
    
    public int playerAssignFlag1 = 0;
    public GameObject enemyTarget;
    public FightManager fightScript;
    public int playerTempIndex;
    public int enemyAssignFlag = 0;
    public int collideFlag = 0;
    public float forwardSpeed = 0.05f;
    public int closeFlag = 0;
    public int disableCapsuleFlag = 0;
    //public int enemiesHealth = 100;
    public float timer = 0.45f;
    public int playerIndex;
     public Rigidbody rb;
     public RigidbodyConstraints rbAttackConstraints;
      public RigidbodyConstraints rbHitConstraints;
       public Color playerColor;
    public float timerMatReset = 0.1f;
    public int matReturnFlag = 0;
    public GameObject trail;

    // Start is called before the first frame update
    void Start()
    {  rb = this.GetComponent<Rigidbody>();
       fightScript = GameObject.Find("arena1").transform.GetChild(0).transform.gameObject.GetComponent<FightManager>();
       rbAttackConstraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
       rbHitConstraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }
     void OnTriggerStay(Collider other)
   {
       if(other.gameObject.tag == "Player" && other.gameObject == enemyTarget)
       {   if(closeFlag == 0)
           {
            this.gameObject.GetComponent<Animator>().SetBool("isRunning",false);
            this.gameObject.GetComponent<Animator>().SetBool("isHitting",true);
          // other.gameObject.GetComponent<Animator>().SetBool("isRunning",false);
           closeFlag = 1;
           }
       }
   }

   void OnTriggerExit(Collider other)
   {
       if(other.gameObject.tag == "Player" && other.gameObject == enemyTarget)
       {
           this.gameObject.GetComponent<Animator>().SetBool("isRunning",true);
           this.gameObject.GetComponent<Animator>().SetBool("isHitting",false);
          // other.gameObject.GetComponent<Animator>().SetBool("isRunning",false);
           closeFlag = 0;
       }
   }


    // Update is called once per frame
    void FixedUpdate()
    {  
      
      if(matReturnFlag == 1)
     {
         timerMatReset -= Time.deltaTime;
         if(timerMatReset <= 0f)
        { 
            fightScript.players[playerTempIndex].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor",playerColor);
            matReturnFlag = 0;
        }
     }
      
      if(fightScript.attackFlag == 1 && fightScript.gameOver == 0)
         {
       if(fightScript.playersHealth[playerIndex] <= 0)
             {      if(fightScript.playersRemaining == 1)
             {     foreach(GameObject player in fightScript.players)
                 {
                   if(player.activeInHierarchy == true && enemyTarget == player)
                    fightScript.gameOver = 1;
                   // Debug.Log("over" + this.name);
                 }
             }
                 
                 enemyTarget.SetActive(false);
                 enemyTarget.GetComponent<CapsuleCollider>().enabled = false;
                 enemyAssignFlag = 0;
                 closeFlag = 0;
                 //enemiesHealth = 100;
                 this.GetComponent<Animator>().SetBool("isHitting",false);
                 this.GetComponent<Animator>().SetBool("isRunning",true);
                
                
             }   
         }

        if(enemyAssignFlag == 0 && fightScript.gameOver == 0 && fightScript.attackFlag == 1)
      { 
           if(playerAssignFlag1 == 0 )
      {
          playerTempIndex = Random.Range(0,fightScript.players.Count);
          playerAssignFlag1 = 1;
          enemyTarget = fightScript.players[playerTempIndex];
      }
      else if(playerAssignFlag1 == 1)
      {
           while(fightScript.playersHealth[playerTempIndex] <= 0 && fightScript.gameOver == 0)
        {
            playerTempIndex = Random.Range(0,fightScript.players.Count);
        }
       enemyTarget = fightScript.players[playerTempIndex];
      }  
         
        
       for(int i=0;i<fightScript.players.Count;i++)
       {
           if(fightScript.players[i] == enemyTarget)
           {
               playerIndex = i;
               break;
           }
       }

     // this.GetComponent<CloseCheck>().enabled = true;
     // this.GetComponent<CloseCheck>().target = enemyTarget;

      
         enemyAssignFlag = 1;
             
      }
     if(fightScript.attackFlag == 1 && fightScript.gameOver == 0)
     {   
        if(disableCapsuleFlag == 0)
         {
          foreach(Collider c in GetComponents<Collider> ())
          {
            if(c.isTrigger == false)
            c.enabled = false;
            rb.constraints = rbAttackConstraints;
            disableCapsuleFlag = 1;
          }
          //rb.isKinematic = true;
         }

         if(collideFlag == 0)
        {    rb.constraints = rbAttackConstraints;
            this.transform.GetChild(1).gameObject.SetActive(true);
             collideFlag = 1;
        } 

      if(closeFlag == 0)
            { rb.constraints = rbAttackConstraints;
              
              rb.angularVelocity = Vector3.zero;
              //transform.Translate(Vector3.forward * forwardSpeed);
                rb.velocity = transform.forward * forwardSpeed;
              transform.LookAt(enemyTarget.transform);
            }
            if(closeFlag == 1 )
            { rb.constraints = rbHitConstraints;
                EnemyHit();  
            }
     }      
    }

    IEnumerator PlayerHit()
    {   
         this.GetComponent<Animator>().SetBool("isHitting",true);
         this.GetComponent<Animator>().SetBool("isRunning",false);
          yield return new WaitForSeconds(0.7f);
  
                fightScript.playersHealth[playerIndex] -= 20;
             
    }
    void EnemyHit()
    {   
        
          timer -= Time.deltaTime;
          
             if(timer <= 0f)
               { //fightScript.players[playerTempIndex].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor",Color.white);
                 //  matReturnFlag = 1;

                  fightScript.playersHealth[playerIndex] -= 15;
                   timer = 0.45f;
               }

    }
}
