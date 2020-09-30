using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
   void Awake()
   {  if(!PlayerPrefs.HasKey("LevelNumber"))
      PlayerPrefs.SetInt("LevelNumber",1);
       
       if(PlayerPrefs.GetInt("ExitLevel") >= 1 && PlayerPrefs.GetInt("ExitLevel") <= 8)
       SceneManager.LoadScene(PlayerPrefs.GetInt("ExitLevel"));
       else
       SceneManager.LoadScene(1);
   }

    
}
