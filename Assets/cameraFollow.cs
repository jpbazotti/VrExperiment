using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform camPos;
    public float offsetxy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(camPos.rotation.x);
        Vector3 newVec = new Vector3(camPos.position.x, camPos.position.y - 0.65f, camPos.position.z);
        transform.LookAt(newVec);
        Vector3 modifier = Vector3.ProjectOnPlane(camPos.forward+(camPos.up*1.5f), Vector3.up).normalized;
        transform.position = newVec-(modifier*0.1f);
        Debug.DrawLine(camPos.position,camPos.position + Vector3.ProjectOnPlane(camPos.forward, Vector3.up).normalized, Color.blue);
       
    }
}
