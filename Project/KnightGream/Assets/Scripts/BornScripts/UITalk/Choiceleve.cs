using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Choiceleve : MonoBehaviour
{
    public void RetreatAddressborn()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
