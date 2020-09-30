using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag1 : MonoBehaviour,IEndDragHandler,IDragHandler,IBeginDragHandler
{
public Vector2 initialPosition;
public Movement mov;
public int initState;
public Vector3 dragVectorDirection;
public Vector3 swipeLength;
public int swipeDir;
public int swipeThresh = 20;
void Start()
{
  mov = GameObject.Find("Unit").GetComponent<Movement>();
}  
     public void OnBeginDrag(PointerEventData eventData)
    { 
       mov.swipeFlag = 1;
      initialPosition = eventData.pressPosition;
      initState = mov.state;
      
    }

    public void OnDrag(PointerEventData eventData)
    {
     swipeLength = (eventData.position - initialPosition);
      dragVectorDirection = eventData.delta;
      if(swipeLength.y > 0f)
      {
    if(swipeLength.y > swipeThresh)
    {
      swipeDir = 1;
      //Debug.Log("up");
    }
      }
    else if(swipeLength.y < 0f)
    { if(swipeLength.y < -swipeThresh)
      swipeDir = -1;
      //Debug.Log("down");
    }
     mov.swipeDir = swipeDir;
     mov.swipeLength = swipeLength.y; 
      
    }
    public void OnEndDrag(PointerEventData eventData)
{
mov.swipeLength = 0f;
mov.swipeDir = 0;
}


}
