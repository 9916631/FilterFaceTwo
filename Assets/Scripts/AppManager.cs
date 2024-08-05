using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    // Method to change the scene based on the provided scene name
    public void ChangeScene(string sceneName)
    {
        // Load the scene with the specified name
        SceneManager.LoadScene(sceneName);
    }

    // Method to exit the application
    public void ExitApplication()
    {
        // Quit the application
        Application.Quit();
    }

}
