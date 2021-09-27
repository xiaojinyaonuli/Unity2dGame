using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion_and_enemy : MonoBehaviour
{
    public float Explosioncool;

    private float time;

    private void OnEnable()
    {
        time=Time.time;
    }
    private void Update()
    {
        if((time+Explosioncool)<Time.time)
        {
            Enemy_Pool.instance.push_backExplosion_pool(this.gameObject);
        }
    }


}
