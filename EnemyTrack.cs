using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrack : MonoBehaviour
{
  public int attackFlag = 0;
  public float forwardSpeed = 0.13f;

   void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.tag == "Player" && attackFlag == 0)
       {
          attackFlag = 1;
       }
   }

   void Update()
   {
       if(attackFlag == 1)
       {
           this.GetComponent<Animator>().enabled = true;
           this.gameObject.GetComponent<Animator>().SetBool("isCharging",true);
           transform.Translate(new Vector3(0f,0f,forwardSpeed * Time.timeScale));
       }
   }
}
