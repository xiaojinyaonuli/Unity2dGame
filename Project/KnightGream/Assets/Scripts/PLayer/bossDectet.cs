using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDectet : MonoBehaviour
{

    public GameObject boss,wall1,wall2,BossWarn,BossHealth;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
             boss.SetActive(true);
             wall1.SetActive(true);
             wall2.SetActive(true);
             BossWarn.SetActive(true);
             BossHealth.SetActive(true);
        }
    }
}
