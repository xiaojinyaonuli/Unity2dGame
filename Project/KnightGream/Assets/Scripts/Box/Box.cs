using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update

    private float BoxHp;

    public GameObject explosion;
    void Start()
    {
        BoxHp=50;
    }

    // Update is called once per frame
    void Update()
    {
        Boxdeath();
    }


    public void BoxGetHit()
    {
        BoxHp-=15;
    }

    public void Boxdeath()
    {
        if(BoxHp<=0)
        {
            Instantiate(explosion,transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    
}
