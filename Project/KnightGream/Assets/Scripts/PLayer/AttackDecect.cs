using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDecect : MonoBehaviour
{
    public Transform explositionprefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("碰撞");
         ContactPoint contact=other.contacts[0];

         Quaternion rotation=Quaternion.FromToRotation(Vector3.up, contact.normal);

         Vector3 position=contact.point;

         Instantiate(explositionprefab,position,rotation);


    }
        
    
}
