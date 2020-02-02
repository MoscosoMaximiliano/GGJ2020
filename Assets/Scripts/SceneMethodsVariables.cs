using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMethodsVariables : MonoBehaviour
{
    public enum action
    {
        loadlevel,
        quit,
        restart
    }

    public action actionMethod;
    public void ActionSceneChanger()
    {
        switch (actionMethod)
        {
            case action.loadlevel:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case action.quit:
                Application.Quit();
                break;
            case action.restart:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
        
        
        
    }

}
