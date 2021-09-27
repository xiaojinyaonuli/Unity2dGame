using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
  

    public GameObject plane;

    private void OnTriggerEnter2D(Collider2D other)
    {
        plane.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        plane.SetActive(false);
    }

    
}
