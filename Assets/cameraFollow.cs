using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform camPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(camPos.rotation.x);
        Vector3 newVec = new Vector3(camPos.position.x, camPos.position.y - 0.75f, camPos.position.z);
        Vector3 newRotation = new Vector3(0, camPos.eulerAngles.y, 0);
        transform.eulerAngles = newRotation;
        Vector3 modifier = transform.forward.normalized;
        modifier.y = 0;
        Debug.DrawLine(transform.position, transform.position + modifier);
        modifier = modifier.normalized;
        if (camPos.rotation.x <= 0.75)
        {
            transform.position = newVec + (modifier * -0.2f);
        }
        else
        {
            transform.position = newVec + (modifier * 0.2f);
        }
    }
}
