using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Individual : MonoBehaviour
{
    
    public int enemyAssignFlag = 0;
    public GameObject playerTarget;
    public FightManager fightScript;
    public int enemyTempIndex;
    public int playerAssignFlag = 0;
    public int collideFlag = 0;
    public float forwardSpeed = 0.05f;
    public int closeFlag = 0;
    public int disableCapsuleFlag = 0;
    public int rbFlag = 0;
    public float timer = 0.45f;
    public int enemyIndex;
    public Rigidbody rb;
    public RigidbodyConstraints rbAttackConstraints;
    public RigidbodyConstraints rbHitConstraints;
    public GameObject deathPrefab;
    public Color enemyColor;
    public float timerMatReset = 0.1f;
    public int matReturnFlag = 0;
    public GameObject trail;
    public int deadFlag = 0;

    // Start is called before the first frame update
    void Start()
    {  rb = this.GetComponent<Rigidbody>();
       fightScript = GameObject.Find("arena1").transform.GetChild(0).transform.gameObject.GetComponent<FightManager>();
       rbAttackConstraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
       rbHitConstraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }
    
 void OnTriggerEnter(Collider other)
   {
     if(other.gameObject.tag == "EnemyTrack" && deadFlag == 0)
     {  deadFlag = 1;
       DeadCheck.remainCount -- ;
       Debug.Log(DeadCheck.remainCount);
       this.gameObject.GetComponent<Animator>().SetBool("isDead",true);
       Instantiate(deathPrefab,this.gameObject.transform.position,Quaternion.identity);
       //transform.Translate(Vector3.zero);
       transform.parent = null;
      
     }
   }

     void OnTriggerStay(Collider other)
   { //Debug.Log("lol");
       if(other.gameObject.tag == "Enemy" && other.gameObject == playerTarget)
       {   if(closeFlag == 0)
          { 
           this.gameObject.GetComponent<Animator>().SetBool("isRunning",false);
           this.gameObject.GetComponent<Animator>().SetBool("isHitting",true);
         //  other.gameObject.GetComponent<Animator>().SetBool("isRunning",false);
           closeFlag = 1;
          }
          // Debug.Log("detected");
       }
   }


   void OnTriggerExit(Collider other)
   {
       if(other.gameObject.tag == "Enemy" && other.gameObject == playerTarget)
       {
           this.gameObject.GetComponent<Animator>().SetBool("isRunning",true);
           this.gameObject.GetComponent<Animator>().SetBool("isHitting",false);
         //  other.gameObject.GetComponent<Animator>().SetBool("isRunning",false);
           closeFlag = 0;
          // Debug.Log("detected");
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
            fightScript.enemies[enemyTempIndex].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor",enemyColor);
            matReturnFlag = 0;
        }
     }

      if(fightScript.attackFlag == 1 && fightScript.gameOver == 0 && deadFlag == 0)
      { if(fightScript.enemiesHealth[enemyIndex] <= 0 )
             {   if(fightScript.enemiesRemaining == 1)
             {     foreach(GameObject enemy in fightScript.enemies)
                 {
                   if(enemy.activeInHierarchy == true && playerTarget == enemy)
                    fightScript.gameOver = 1;
                 }
             }
                 playerTarget.SetActive(false);
                 playerTarget.GetComponent<CapsuleCollider>().enabled = false;
                 playerAssignFlag = 0;
                 closeFlag = 0;
                 this.GetComponent<Animator>().SetBool("isHitting",false);
                 this.GetComponent<Animator>().SetBool("isRunning",true);  
                
             }
      }      
      
       if(playerAssignFlag == 0 && fightScript.gameOver == 0)
      {  if(enemyAssignFlag == 0)
      {
          enemyTempIndex = Random.Range(0,fightScript.enemies.Count);
          enemyAssignFlag = 1;
          playerTarget = fightScript.enemies[enemyTempIndex];
         
          
      }
      else if(enemyAssignFlag == 1)
      {
           while(fightScript.enemiesHealth[enemyTempIndex] <= 0 && fightScript.gameOver == 0)
        {
            enemyTempIndex = Random.Range(0,fightScript.enemies.Count);
        }
         playerTarget = fightScript.enemies[enemyTempIndex];
          
         
      }  
       
        
       for(int i=0;i<fightScript.enemies.Count;i++)
       {
           if(fightScript.enemies[i] == playerTarget)
           {
               enemyIndex = i;
               break;
           }
       }

     // this.GetComponent<CloseCheck>().enabled = true;
     // this.GetComponent<CloseCheck>().target = playerTarget;

      
         playerAssignFlag = 1;
             
      }
     if(fightScript.attackFlag == 1 && fightScript.gameOver == 0 && deadFlag == 0)
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
         // rb.isKinematic = true;
         }

         if(collideFlag == 0)
        {   
            this.transform.GetChild(1).gameObject.GetComponent<CapsuleCollider>().enabled = true;
            
             foreach(Collider c in GetComponents<Collider> ())
          {
            if(c.isTrigger == true)
            c.enabled = true;
  
          }
             collideFlag = 1;
        } 

      if(closeFlag == 0)
            { rb.constraints = rbAttackConstraints;
           //   transform.Translate(Vector3.forward * forwardSpeed);
             rb.angularVelocity = Vector3.zero;
             rb.velocity = transform.forward * forwardSpeed;
              transform.LookAt(playerTarget.transform);
            }
            if(closeFlag == 1 )
            {   rb.constraints = rbHitConstraints;
                PlayerHit1();
                
            }
     }      
    }

    IEnumerator PlayerHit()
    {   
         this.GetComponent<Animator>().SetBool("isHitting",true);
         this.GetComponent<Animator>().SetBool("isRunning",false);
          yield return new WaitForSeconds(0.7f);
  
                fightScript.enemiesHealth[enemyIndex] -= 15;
               
    }
    void PlayerHit1()
    {   
         
          timer -= Time.deltaTime;
          
             if(timer <= 0f)
               { // fightScript.enemies[enemyTempIndex].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor",Color.white);
                 //  matReturnFlag = 1;
                 //fightScript.enemies[enemyTempIndex].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = enemyColor;
                  fightScript.enemiesHealth[enemyIndex] -= 15;
                   timer = 0.45f;
               }
            
            
            
         
         
         //Debug.Log("player " + index);
    }
}
