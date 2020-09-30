using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCheck : MonoBehaviour
{
   public int closeFlag = 0;
public GameObject target;
   void OnTriggerEnter(Collider other)
   {
       if(this.gameObject.tag == "Player" && other.gameObject.tag == "Enemy" && other.gameObject.name == target.name)
       {
           this.gameObject.GetComponent<Animator>().SetBool("isRunning",false);
           other.gameObject.GetComponent<Animator>().SetBool("isRunning",false);
           closeFlag = 1;
       }
   }
}
