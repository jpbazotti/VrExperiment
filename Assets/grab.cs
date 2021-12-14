using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class grab : MonoBehaviour
{
    private Transform grabPoint;
    public bool direito;
    List<InputDevice> devices = new List<InputDevice>();
    private GameObject obj;
    private bool holding;
    private Vector3[] pos=new Vector3[15]; 
    private int frameCounter;
    // Start is called before the first frame update
    void Start()
    {
        if (direito)
        {
            InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        }
        else
        {
            InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);
        }
        grabPoint = GetComponentInChildren<Transform>();
        obj = null;
        holding = false;
        for(int i = 0; i < pos.Length; i++)
        {
            pos[i] = Vector3.zero;
        }
    }

    private void Update()
    {
        bool gripping = false;
        foreach (InputDevice device in devices)
        {
            device.TryGetFeatureValue(CommonUsages.gripButton, out gripping);        
        }
        if (holding)
        {
            for (int i = 0; i < pos.Length-1; i++)
            {
                pos[i] = pos[i+1];
            }
            pos[pos.Length - 1] = grabPoint.transform.position;
            for (int i = 0; i < pos.Length; i++)
            {
                Debug.Log(i + ":" + pos[i]);
            }
            if (!gripping)
            {

                obj.GetComponent<Rigidbody>().isKinematic = false;
                obj.GetComponent<Collider>().enabled = true;
                obj.transform.parent = null;
                Vector3 vel = (pos[pos.Length - 1] - pos[0]);
                Debug.Log("first x:" + pos[0].x + " Y:" + pos[0].y + " Z:" + pos[0].z);
                Debug.Log("last x:" + pos[pos.Length - 1].x + " Y:" + pos[pos.Length - 1].y + " Z:" + pos[pos.Length - 1].z);
                Debug.Log("end x:" + vel.x + " Y:" + vel.y + " Z:"+vel.z);
                obj.GetComponent<Rigidbody>().AddForce(10*vel / (Time.deltaTime * 15),ForceMode.Impulse);
                obj = null;
                holding = false;
            }
        }
        else
        {
            if (obj != null && gripping)
            {
                obj.transform.position = grabPoint.transform.position;
                obj.transform.parent = grabPoint;
                obj.GetComponent<Rigidbody>().isKinematic = true;
                obj.GetComponent<Collider>().enabled = false;
                holding = true;
            }
        }
        frameCounter++;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!holding)
        {
            obj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        obj = null;
    }
}
