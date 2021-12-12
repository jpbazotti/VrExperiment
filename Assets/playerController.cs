using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class playerController : MonoBehaviour
{
    public float speed = 1.0f;

    public Transform head;
    public CharacterController CC;
    List<UnityEngine.XR.InputDevice> devices = new List<InputDevice>();
    // Start is called before the first frame update
    void Start()
    {
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(InputDevice device in devices)
        {
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position);
            PlayerMove(position);
        }
    }

    private void PlayerMove(Vector2 direction)
    {
        Quaternion headRotation = Quaternion.Euler(0, head.eulerAngles.y, 0);
        Debug.Log("head = "+ head.eulerAngles.y);

        direction = headRotation * direction;
        CC.Move(speed * (new Vector3(direction.x, 0, direction.y)) * Time.fixedDeltaTime);
        Debug.Log(speed * (new Vector3(direction.x, 0, direction.y)) * Time.fixedDeltaTime);
    }
}
