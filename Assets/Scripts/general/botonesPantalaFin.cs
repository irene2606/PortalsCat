using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class botonesPantalaFin : MonoBehaviour
{
    public void Exit() 
    {
        Application.Quit();
    }

    public void ChangeScene(string sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
