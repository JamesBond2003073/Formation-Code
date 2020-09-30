using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmoothFollow : MonoBehaviour
{  public Transform target;
   public Vector3 targetPosition;
   public float smoothSpeed = 0.3f;
   public Vector3 offset;
   private Vector3 refvel;
   public FightManager fightScript;
   public Vector3 avg = Vector3.zero;
   public int aliveCount = 0;
   public int confettiFlag = 0;
   public ParticleSystem confetti;

void Start()
{
    fightScript = GameObject.Find("arena1").transform.GetChild(0).transform.gameObject.GetComponent<FightManager>();
    
}
   void Update()
   { 
      

       if(fightScript.attackFlag == 0)
         targetPosition = target.position;

       Vector3 desiredPosition = targetPosition + offset;
       Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position,desiredPosition,ref refvel,smoothSpeed);
       transform.position = smoothedPosition;

      
   
       
   }

   void LateUpdate()
   {
        if(fightScript.attackFlag == 1 && fightScript.playersRemaining != 0)
       {  
    
     for(int i=0;i<fightScript.playersHealth.Count;i++)
     {
         if(fightScript.playersHealth[i] > 0f)
         {
             avg += fightScript.players[i].transform.position;
             aliveCount ++;
         }    
     }

       avg = avg/aliveCount;
       targetPosition = avg;
           avg = Vector3.zero;
           aliveCount = 0;
          
   

           //target = GameObject.Find("arena1").transform.Find("Cube.047 (3)").transform;
           if(SceneManager.GetActiveScene().buildIndex == 5)
           offset = new Vector3(0f,17f,-12f);
           else if(SceneManager.GetActiveScene().buildIndex == 8)
           offset = new Vector3(0f,10.5f,-9f);
           else
           offset = new Vector3(0f,7.5f,-7f);

           smoothSpeed = 0.3f;
       }
   }
  
}
