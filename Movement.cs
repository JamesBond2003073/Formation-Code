using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class Movement : MonoBehaviour
{ public FightManager fightScript;
  public float cubeZ = -12.04f;
  public float narrowLeftTargetX = -2.86f;
  public float narrowRightTargetX = -1.49f;
   public float narrowLeftTargetX2 = -4.2f;
  public float narrowRightTargetX2 = -0.18f;
  public float wideLeftTargetX = -4.2f;
  public float wideRightTargetX = -0.18f;
  public float wideLeftTargetX2 = -5.49f;
  public float wideRightTargetX2 = 1.16f;
  public int flagW = 0;
  public int flagS = 0;
  public int flagLeft = 0;
  public int flagRight = 0;
  public int state = 0;
  public int midForwardFlag = 0;
  public int narrowForwardFlag = 0;
  public int wideBackFlag = 0;
  public int midBackFlag = 0;
  public int midForwardFlagStart = 0;
  public int narrowForwardFlagStart = 0;
  public int wideBackFlagStart = 0;
  public int midBackFlagStart = 0;
  public float introTimer = 5f;
  public float transitionSpeed = 0.05f;
  public int completeCount = 0;
  public int assignFlag = 0;
  public int posAssignFlag = 0;
  public int swipeDir = 0;
  public int fps;
  public int pauseFlag = 0;
public Animator fadeAnim;

public TextMeshProUGUI text;

  public int dir = 0;
  public int swipeFlag = 0;
  public float swipeLength = 0f;
  
  public GameObject dummy;
  public GameObject[] soldiersLeft  = new GameObject[3];
  public GameObject[] soldiersRight  = new GameObject[3];
  public GameObject[] soldiersMidLeft = new GameObject[3];
  public GameObject[] soldiersMidRight = new GameObject[3];
  public List<GameObject> transformList;
  public List<Vector3> pos;

  public float forwardSpeed = 0.2f;
  public Drag drag1;
 public bool IsMouseOverUI()
 {
    return EventSystem.current.IsPointerOverGameObject();
 }
    // Start is called before the first frame update
    void Start()
    {  Application.targetFrameRate = 60;
      Time.timeScale = 1;
       dummy = GameObject.Find("dummy");
       drag1 = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Drag>();
       fadeAnim = GameObject.Find("Canvas").transform.GetChild(1).gameObject.GetComponent<Animator>();
       fightScript = GameObject.Find("arena1").transform.GetChild(0).transform.gameObject.GetComponent<FightManager>();
    }

    // Update is called once per frame
    void Update()
    {  
      for(int i=0;i<3;i++)
      {
        if(soldiersLeft[i].GetComponent<Individual>().deadFlag == 1)
        soldiersLeft[i] = dummy;
        if(soldiersMidLeft[i].GetComponent<Individual>().deadFlag == 1)
        soldiersMidLeft[i] = dummy;
        if(soldiersRight[i].GetComponent<Individual>().deadFlag == 1)
        soldiersRight[i] = dummy;
        if(soldiersMidRight[i].GetComponent<Individual>().deadFlag == 1)
        soldiersMidRight[i] = dummy;
      }
      
       if(fightScript.attackFlag == 0)
       {
       fps = (int)(1f / Time.unscaledDeltaTime);
      text.text = fps.ToString();
      
      introTimer -= Time.deltaTime;
      if(introTimer <= 0f)
      fadeAnim.SetBool("end",true);
       

      if(Input.GetKey(KeyCode.W))
      flagW=1;

      /*

      if(SceneManager.GetActiveScene().buildIndex <= 3)
      {

  if(Input.GetMouseButton(0))
   {
     midForwardFlagStart = 1;
    // Debug.Log("lol");
   }

  if(!Input.GetMouseButton(0))
   {
     wideBackFlagStart = 1;
   }
      }

      if(SceneManager.GetActiveScene().buildIndex > 3 && SceneManager.GetActiveScene().buildIndex <= 6)
      {

  if(Input.GetMouseButton(0))
   {
     narrowForwardFlagStart = 1;
    // Debug.Log("lol");
   }
 
  if(!Input.GetMouseButton(0))
   {
     midBackFlagStart = 1;
   }
   
      }

*/
       if(SceneManager.GetActiveScene().buildIndex <= 3 || SceneManager.GetActiveScene().buildIndex == 6)
      {

  if(Input.GetMouseButton(0) && !IsMouseOverUI())
   {
  MidForward();
   }

  if(!Input.GetMouseButton(0) && !IsMouseOverUI())
   {
   WideBack();
   }
      }

      if(SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 7 || SceneManager.GetActiveScene().buildIndex == 8)
      {

  if(Input.GetMouseButton(0) && !IsMouseOverUI())
   {
    NarrowForward();
   }
 
  if(!Input.GetMouseButton(0) && !IsMouseOverUI())
   {
    MidBack();
   }
   
      }

      

    /*

        //formation part 1
        if(swipeFlag == 1 && drag1.initState == 0)
        { 
           if(drag1.dragVectorDirection.y > 0f)
         {
            if(swipeLength > 80f && swipeLength < 210f && midBackFlagStart == 0 && narrowForwardFlagStart == 0 && wideBackFlagStart == 0 && state == 0)
          midForwardFlagStart = 1;
         
          else if(swipeLength > 210f && midBackFlagStart == 0 && midForwardFlagStart == 0 && wideBackFlagStart== 0 && state == 1)
          narrowForwardFlagStart = 1;
        
         }
         else if(drag1.dragVectorDirection.y < 0f)
          { 
            if(swipeLength < 170f && swipeLength > 20f  && midForwardFlagStart == 0 && narrowForwardFlagStart== 0 && wideBackFlagStart == 0 && state == 2)
        {
          midBackFlagStart = 1;
          

         }
          else if(swipeLength < 20f && midBackFlagStart == 0 && narrowForwardFlagStart == 0 && midForwardFlagStart == 0 && state == 1)
          wideBackFlagStart = 1;
         
          }
        }

        //formation part2

          if(swipeFlag == 1 && drag1.initState == 1)
        { 
           if(drag1.dragVectorDirection.y > 0f)
         {
            if(swipeLength > 130f && midBackFlagStart == 0 && midForwardFlagStart == 0 && wideBackFlagStart == 0 && state == 1)
          {
            narrowForwardFlagStart = 1;
          //  Debug.Log("narrow");
          }
          else if(swipeLength > 0f && swipeLength < 130f && midBackFlagStart == 0 && narrowForwardFlagStart == 0 && wideBackFlagStart == 0 && state == 0)
          {midForwardFlagStart = 1;
        //  Debug.Log("mid");
          }
         }
         else if(drag1.dragVectorDirection.y < 0f)
          { 
            if(swipeLength > -60f && swipeLength < 90f  && midForwardFlagStart == 0 && narrowForwardFlagStart== 0 && wideBackFlagStart == 0 && state == 2)
        {
          midBackFlagStart = 1;
         }
          else if(swipeLength < -60f && midBackFlagStart == 0 && narrowForwardFlagStart == 0 && midForwardFlagStart == 0 && state == 1)
          wideBackFlagStart = 1;
          
          }
        }

         //formation part3

          if(swipeFlag == 1 && drag1.initState == 2)
        { 
           if(drag1.dragVectorDirection.y > 0f)
         {
            if(swipeLength > 0f && midBackFlagStart == 0 && midForwardFlagStart == 0 && wideBackFlagStart == 0 && state == 1)
          narrowForwardFlagStart = 1;
          else if(swipeLength > -130f && swipeLength < 0f && midBackFlagStart == 0 && narrowForwardFlagStart == 0 && wideBackFlagStart == 0 && state == 0)
          midForwardFlagStart = 1;
         }
         else if(drag1.dragVectorDirection.y < 0f)
          { 
            if(swipeLength < -40f && swipeLength > -190f  && midForwardFlagStart == 0 && narrowForwardFlagStart== 0 && wideBackFlagStart == 0 && state == 2)
        {
          midBackFlagStart = 1;
         }
          else if(swipeLength < -190f && midBackFlagStart == 0 && narrowForwardFlagStart == 0 && midForwardFlagStart == 0 && state == 1)
          wideBackFlagStart = 1;
          
          }
        }
          

        if(midForwardFlagStart == 1)
        {  

           if(assignFlag == 0)
        {
          Assign();
        }
        MidForward();
        }
       else if(wideBackFlagStart == 1)
        {
           if(assignFlag == 0)
        {
          Assign();
        }
          WideBack();
        }
       else if(midBackFlagStart == 1)
        {
           if(assignFlag == 0)
        {
          Assign();
        }
          MidBack();
        }
       else if(narrowForwardFlagStart == 1)
        {
           if(assignFlag == 0)
        {
          Assign();
        }
          NarrowForward();
        }
*/
      
       }
    }

    void NarrowForward()
    { 
      
        
      
         soldiersLeft[2].transform.localPosition = Vector3.MoveTowards(soldiersLeft[2].transform.localPosition,new Vector3(narrowLeftTargetX,soldiersLeft[2].transform.localPosition.y,cubeZ + 2f),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersLeft[1].transform.localPosition = Vector3.MoveTowards(soldiersLeft[1].transform.localPosition,new Vector3(narrowLeftTargetX,soldiersLeft[1].transform.localPosition.y,cubeZ + 3f),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersLeft[0].transform.localPosition = Vector3.MoveTowards(soldiersLeft[0].transform.localPosition,new Vector3(narrowLeftTargetX,soldiersLeft[0].transform.localPosition.y,cubeZ + 4f),transitionSpeed * Time.timeScale * Time.deltaTime);
            
            if(soldiersLeft[0].transform.localPosition == new Vector3(narrowLeftTargetX,soldiersLeft[0].transform.localPosition.y,cubeZ + 4f))
           {
             // flagLeft = 1;
           }
            
            soldiersRight[2].transform.localPosition = Vector3.MoveTowards(soldiersRight[2].transform.localPosition,new Vector3(narrowRightTargetX,soldiersLeft[2].transform.localPosition.y,cubeZ + 2f),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersRight[1].transform.localPosition = Vector3.MoveTowards(soldiersRight[1].transform.localPosition,new Vector3(narrowRightTargetX,soldiersLeft[1].transform.localPosition.y,cubeZ + 3f),transitionSpeed * Time.timeScale * Time.deltaTime);
             soldiersRight[0].transform.localPosition = Vector3.MoveTowards(soldiersRight[0].transform.localPosition,new Vector3(narrowRightTargetX,soldiersLeft[0].transform.localPosition.y,cubeZ + 4f),transitionSpeed * Time.timeScale * Time.deltaTime);
           
              soldiersMidLeft[0].transform.localPosition = Vector3.MoveTowards(soldiersMidLeft[0].transform.localPosition,new Vector3(narrowLeftTargetX,soldiersMidLeft[0].transform.localPosition.y,soldiersMidLeft[0].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidLeft[1].transform.localPosition = Vector3.MoveTowards(soldiersMidLeft[1].transform.localPosition,new Vector3(narrowLeftTargetX,soldiersMidLeft[1].transform.localPosition.y,soldiersMidLeft[1].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidLeft[2].transform.localPosition = Vector3.MoveTowards(soldiersMidLeft[2].transform.localPosition,new Vector3(narrowLeftTargetX,soldiersMidLeft[2].transform.localPosition.y,soldiersMidLeft[2].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);

             soldiersMidRight[0].transform.localPosition = Vector3.MoveTowards(soldiersMidRight[0].transform.localPosition,new Vector3(narrowRightTargetX,soldiersMidRight[0].transform.localPosition.y,soldiersMidRight[0].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidRight[1].transform.localPosition = Vector3.MoveTowards(soldiersMidRight[1].transform.localPosition,new Vector3(narrowRightTargetX,soldiersMidRight[1].transform.localPosition.y,soldiersMidRight[1].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidRight[2].transform.localPosition = Vector3.MoveTowards(soldiersMidRight[2].transform.localPosition,new Vector3(narrowRightTargetX,soldiersMidRight[2].transform.localPosition.y,soldiersMidRight[2].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);

            if(soldiersRight[0].transform.localPosition == new Vector3(narrowRightTargetX,soldiersLeft[0].transform.localPosition.y,cubeZ + 4f))
            {
            //flagRight = 1;
            }

            if(flagLeft == 1 && flagRight == 1)
            {
              flagW = 0;
              flagLeft = 0;
              flagRight = 0;
              state = 2;
              narrowForwardFlag = 0;
               narrowForwardFlagStart = 0;
             // Debug.Log("narrow");
            }

         
      
       

    }
    void MidForward()
    { 
       
        soldiersLeft[0].transform.localPosition = Vector3.MoveTowards(soldiersLeft[0].transform.localPosition,new Vector3(wideLeftTargetX2,soldiersLeft[0].transform.localPosition.y,soldiersLeft[0].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersLeft[1].transform.localPosition = Vector3.MoveTowards(soldiersLeft[1].transform.localPosition,new Vector3(wideLeftTargetX2,soldiersLeft[1].transform.localPosition.y,soldiersLeft[1].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersLeft[2].transform.localPosition = Vector3.MoveTowards(soldiersLeft[2].transform.localPosition,new Vector3(wideLeftTargetX2,soldiersLeft[2].transform.localPosition.y,soldiersLeft[2].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);

             soldiersMidLeft[0].transform.localPosition = Vector3.MoveTowards(soldiersMidLeft[0].transform.localPosition,new Vector3(wideLeftTargetX,soldiersMidLeft[0].transform.localPosition.y,soldiersMidLeft[0].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidLeft[1].transform.localPosition = Vector3.MoveTowards(soldiersMidLeft[1].transform.localPosition,new Vector3(wideLeftTargetX,soldiersMidLeft[1].transform.localPosition.y,soldiersMidLeft[1].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidLeft[2].transform.localPosition = Vector3.MoveTowards(soldiersMidLeft[2].transform.localPosition,new Vector3(wideLeftTargetX,soldiersMidLeft[2].transform.localPosition.y,soldiersMidLeft[2].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            if(soldiersLeft[1].transform.localPosition == new Vector3(wideLeftTargetX2,soldiersLeft[1].transform.localPosition.y,soldiersLeft[1].transform.localPosition.z))
          { 
            // flagLeft = 1;
          }
            
             soldiersRight[0].transform.localPosition = Vector3.MoveTowards(soldiersRight[0].transform.localPosition,new Vector3(wideRightTargetX2,soldiersRight[0].transform.localPosition.y,soldiersRight[0].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersRight[1].transform.localPosition = Vector3.MoveTowards(soldiersRight[1].transform.localPosition,new Vector3(wideRightTargetX2,soldiersRight[1].transform.localPosition.y,soldiersRight[1].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime );
            soldiersRight[2].transform.localPosition = Vector3.MoveTowards(soldiersRight[2].transform.localPosition,new Vector3(wideRightTargetX2,soldiersRight[2].transform.localPosition.y,soldiersRight[2].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);

            soldiersMidRight[0].transform.localPosition = Vector3.MoveTowards(soldiersMidRight[0].transform.localPosition,new Vector3(wideRightTargetX,soldiersMidRight[0].transform.localPosition.y,soldiersMidRight[0].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidRight[1].transform.localPosition = Vector3.MoveTowards(soldiersMidRight[1].transform.localPosition,new Vector3(wideRightTargetX,soldiersMidRight[1].transform.localPosition.y,soldiersMidRight[1].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidRight[2].transform.localPosition = Vector3.MoveTowards(soldiersMidRight[2].transform.localPosition,new Vector3(wideRightTargetX,soldiersMidRight[2].transform.localPosition.y,soldiersMidRight[2].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            if(soldiersRight[1].transform.localPosition == new Vector3(wideRightTargetX2,soldiersRight[1].transform.localPosition.y,soldiersRight[1].transform.localPosition.z))
           {
             // flagRight = 1;
           }

            if(flagLeft == 1 && flagRight == 1)
            { // Debug.Log("done");
              flagW = 0;
              flagLeft = 0;
              flagRight = 0;
              state = 1;
             midForwardFlag = 0;
             midForwardFlagStart = 0;
            }

            

    }

    void MidBack()
    {

     

            soldiersLeft[0].transform.localPosition = Vector3.MoveTowards(soldiersLeft[0].transform.localPosition,new Vector3(wideLeftTargetX2,soldiersLeft[0].transform.localPosition.y,cubeZ - 1f),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersLeft[1].transform.localPosition = Vector3.MoveTowards(soldiersLeft[1].transform.localPosition,new Vector3(wideLeftTargetX2,soldiersLeft[1].transform.localPosition.y,cubeZ ),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersLeft[2].transform.localPosition = Vector3.MoveTowards(soldiersLeft[2].transform.localPosition,new Vector3(wideLeftTargetX2,soldiersLeft[2].transform.localPosition.y,cubeZ +1f ),transitionSpeed * Time.timeScale * Time.deltaTime);
            
            if(soldiersLeft[0].transform.localPosition == new Vector3(wideLeftTargetX2,soldiersLeft[0].transform.localPosition.y,cubeZ - 1f))
           { //flagLeft = 1;
           }
            soldiersRight[0].transform.localPosition = Vector3.MoveTowards(soldiersRight[0].transform.localPosition,new Vector3(wideRightTargetX2,soldiersRight[0].transform.localPosition.y,cubeZ - 1f),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersRight[1].transform.localPosition = Vector3.MoveTowards(soldiersRight[1].transform.localPosition,new Vector3(wideRightTargetX2,soldiersRight[1].transform.localPosition.y,cubeZ ),transitionSpeed * Time.timeScale * Time.deltaTime);
             soldiersRight[2].transform.localPosition = Vector3.MoveTowards(soldiersRight[2].transform.localPosition,new Vector3(wideRightTargetX2,soldiersRight[2].transform.localPosition.y,cubeZ +1f ),transitionSpeed * Time.timeScale * Time.deltaTime);
           
            soldiersMidLeft[0].transform.localPosition = Vector3.MoveTowards(soldiersMidLeft[0].transform.localPosition,new Vector3(wideLeftTargetX,soldiersMidLeft[0].transform.localPosition.y,soldiersMidLeft[0].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidLeft[1].transform.localPosition = Vector3.MoveTowards(soldiersMidLeft[1].transform.localPosition,new Vector3(wideLeftTargetX,soldiersMidLeft[1].transform.localPosition.y,soldiersMidLeft[1].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidLeft[2].transform.localPosition = Vector3.MoveTowards(soldiersMidLeft[2].transform.localPosition,new Vector3(wideLeftTargetX,soldiersMidLeft[2].transform.localPosition.y,soldiersMidLeft[2].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);

             soldiersMidRight[0].transform.localPosition = Vector3.MoveTowards(soldiersMidRight[0].transform.localPosition,new Vector3(wideRightTargetX,soldiersMidRight[0].transform.localPosition.y,soldiersMidRight[0].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidRight[1].transform.localPosition = Vector3.MoveTowards(soldiersMidRight[1].transform.localPosition,new Vector3(wideRightTargetX,soldiersMidRight[1].transform.localPosition.y,soldiersMidRight[1].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidRight[2].transform.localPosition = Vector3.MoveTowards(soldiersMidRight[2].transform.localPosition,new Vector3(wideRightTargetX,soldiersMidRight[2].transform.localPosition.y,soldiersMidRight[2].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);

            if(soldiersRight[0].transform.localPosition == new Vector3(wideRightTargetX2,soldiersLeft[0].transform.localPosition.y,cubeZ - 1f))
           { //flagRight = 1;
           }
            if(flagLeft == 1 && flagRight == 1)
            {
              flagS = 0;
              flagLeft = 0;
              flagRight = 0;
              state = 1;
              midBackFlag = 0;
               midBackFlagStart = 0;
              
            }

        
    }

    void WideBack()
    {  
     
       soldiersLeft[0].transform.localPosition = Vector3.MoveTowards(soldiersLeft[0].transform.localPosition,new Vector3(wideLeftTargetX,soldiersLeft[0].transform.localPosition.y,soldiersLeft[0].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersLeft[1].transform.localPosition = Vector3.MoveTowards(soldiersLeft[1].transform.localPosition,new Vector3(wideLeftTargetX,soldiersLeft[1].transform.localPosition.y,soldiersLeft[1].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersLeft[2].transform.localPosition = Vector3.MoveTowards(soldiersLeft[2].transform.localPosition,new Vector3(wideLeftTargetX,soldiersLeft[2].transform.localPosition.y,soldiersLeft[2].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);

             soldiersMidLeft[0].transform.localPosition = Vector3.MoveTowards(soldiersMidLeft[0].transform.localPosition,new Vector3(narrowLeftTargetX,soldiersMidLeft[0].transform.localPosition.y,soldiersMidLeft[0].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidLeft[1].transform.localPosition = Vector3.MoveTowards(soldiersMidLeft[1].transform.localPosition,new Vector3(narrowLeftTargetX,soldiersMidLeft[1].transform.localPosition.y,soldiersMidLeft[1].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidLeft[2].transform.localPosition = Vector3.MoveTowards(soldiersMidLeft[2].transform.localPosition,new Vector3(narrowLeftTargetX,soldiersMidLeft[2].transform.localPosition.y,soldiersMidLeft[2].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            if(soldiersLeft[1].transform.localPosition == new Vector3(wideLeftTargetX,soldiersLeft[1].transform.localPosition.y,soldiersLeft[1].transform.localPosition.z))
           { //flagLeft = 1;
           }
             soldiersRight[0].transform.localPosition = Vector3.MoveTowards(soldiersRight[0].transform.localPosition,new Vector3(wideRightTargetX,soldiersRight[0].transform.localPosition.y,soldiersRight[0].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersRight[1].transform.localPosition = Vector3.MoveTowards(soldiersRight[1].transform.localPosition,new Vector3(wideRightTargetX,soldiersRight[1].transform.localPosition.y,soldiersRight[1].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersRight[2].transform.localPosition = Vector3.MoveTowards(soldiersRight[2].transform.localPosition,new Vector3(wideRightTargetX,soldiersRight[2].transform.localPosition.y,soldiersRight[2].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);

            soldiersMidRight[0].transform.localPosition = Vector3.MoveTowards(soldiersMidRight[0].transform.localPosition,new Vector3(narrowRightTargetX,soldiersMidRight[0].transform.localPosition.y,soldiersMidRight[0].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidRight[1].transform.localPosition = Vector3.MoveTowards(soldiersMidRight[1].transform.localPosition,new Vector3(narrowRightTargetX,soldiersMidRight[1].transform.localPosition.y,soldiersMidRight[1].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            soldiersMidRight[2].transform.localPosition = Vector3.MoveTowards(soldiersMidRight[2].transform.localPosition,new Vector3(narrowRightTargetX,soldiersMidRight[2].transform.localPosition.y,soldiersMidRight[2].transform.localPosition.z),transitionSpeed * Time.timeScale * Time.deltaTime);
            if(soldiersRight[1].transform.localPosition == new Vector3(wideRightTargetX,soldiersRight[1].transform.localPosition.y,soldiersRight[1].transform.localPosition.z))
            {//flagRight = 1;
            }
            if(flagLeft == 1 && flagRight == 1)
            { // Debug.Log("done");
              flagW = 0;
              flagLeft = 0;
              flagRight = 0;
              state = 1;
             wideBackFlag = 0;
             wideBackFlagStart = 0;
            }  

               
       
    }
    void Assign()
    {
      transformList.Clear();
           pos.Clear();

          transformList.Add(soldiersLeft[0]);
        transformList.Add(soldiersLeft[1]);
        transformList.Add(soldiersLeft[2]);
        transformList.Add(soldiersRight[0]);
       transformList.Add(soldiersRight[1]);
        transformList.Add(soldiersRight[2]);
        transformList.Add(soldiersMidLeft[0]);
        transformList.Add(soldiersMidLeft[1]);
        transformList.Add(soldiersMidLeft[2]);
        transformList.Add(soldiersMidRight[0]);
        transformList.Add(soldiersMidRight[1]);
        transformList.Add(soldiersMidRight[2]);

        for(int i=0;i<12;i++)
        {
           pos.Add(transformList[i].transform.localPosition);
        }

        assignFlag = 1;
    }
    
}
