using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterSlider : MonoBehaviour
{

    public  GameObject Plane;
    public void EnterSence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void Cancelplane()
    {
        Plane.SetActive(false);
    }

    

    


}
