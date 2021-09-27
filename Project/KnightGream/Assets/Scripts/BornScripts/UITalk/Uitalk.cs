using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uitalk : MonoBehaviour
{
    public GameObject ui_r;

    public GameObject plane;




    private void OnTriggerEnter2D(Collider2D other)
    {
       
        ui_r.SetActive(true);
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ui_r.SetActive(false);
        plane.SetActive(false);
        
    }

    private void Update()
    {
        if(ui_r.activeSelf&&Input.GetKeyDown(KeyCode.R))
        {
            plane.SetActive(true);
        }
    }
}
