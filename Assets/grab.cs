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
    private Vector3 SPos;
    private Vector3 EPos;
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
        EPos = grabPoint.transform.position;
    }

    private void Update()
    {
        bool gripping = false;
        foreach (InputDevice device in devices)
        {
            device.TryGetFeatureValue(CommonUsages.gripButton, out gripping);
            Debug.Log(gripping);
        
        }
        if (holding)
        {
            if (frameCounter == 10) { 
                SPos = EPos;
                frameCounter = 0;
            }
            EPos = grabPoint.transform.position;
            if (!gripping)
            {
                obj.GetComponent<Rigidbody>().isKinematic = false;
                obj.GetComponent<Collider>().enabled = true;
                obj.transform.parent = null;
                obj.GetComponent<Rigidbody>().AddForce(((EPos - SPos)), ForceMode.Impulse);
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
