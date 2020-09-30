using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothf : MonoBehaviour
{  public Transform target;
   public float smoothSpeed = 0.3f;
   public Vector3 offset;
   private Vector3 refvel;
   public FightManager fightScript;


void Start()
{
    fightScript = GameObject.Find("arena1").transform.GetChild(0).transform.gameObject.GetComponent<FightManager>();
}
   void Update()
   {
       Vector3 desiredPosition = target.position + offset;
       Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position,desiredPosition,ref refvel,smoothSpeed);
       transform.position = smoothedPosition;

      
   }
}
