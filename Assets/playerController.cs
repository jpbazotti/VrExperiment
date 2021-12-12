using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class playerController : MonoBehaviour
{
    public float speed = 1.0f;

    public Transform head;
    public Rigidbody rb;
    List<InputDevice> devices = new List<InputDevice>();
    // Start is called before the first frame update
    void Start()
    {
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(InputDevice device in devices)
        {
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position);
            PlayerMove(new Vector3(position.x,0,position.y));
        }
    }

    private void PlayerMove(Vector3 direction)
    {
        Vector3 forward = head.forward;
        Vector3 right = head.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        //Vector3 headRotation = Vector3.ProjectOnPlane(head.forward + (head.up * 1.5f), Vector3.up).normalized;
        //transform.forward=(headRotation);
        Vector3 mov= (forward * direction.z + right * direction.x);
        rb.velocity = mov;
        Debug.DrawLine(transform.position,transform.position + forward * direction.x + right * direction.z, Color.blue);
        
    }
}
