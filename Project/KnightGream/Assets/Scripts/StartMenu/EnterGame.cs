using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnterGame : MonoBehaviour
{
    public void Entergame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+4);
    }

    public void Exitgame()
    {
       Application.Quit();
    }
    
    public void StartMenuButton()
    {
        GameObject.Find("Canvas/Panel/menu").SetActive(true);
    }
}
