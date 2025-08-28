using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomescreenButtons : MonoBehaviour
{
    public void OnclickStart()
    {
        SceneManager.LoadScene("API-testing");
    }

    public void OnclickQuit()
    {
        Application.Quit();
    }
}
