using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafatherwith : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform objectcamera;


    void Start()
    {
        objectcamera=GameObject.Find("CameraWith").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=Vector3.Lerp(transform.position,objectcamera.position,1f);
    }
}
