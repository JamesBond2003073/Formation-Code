using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpen : MonoBehaviour
{
   public int openFlag = 0;
   public float openSpeed = 0.1f;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && openFlag == 0)
        {
            openFlag = 1;
        }
    }

    void Update()
    {
        if(openFlag == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x,transform.position.y + 10f,transform.position.z),openSpeed * Time.timeScale);
        }
    }
}
