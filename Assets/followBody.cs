using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followBody : MonoBehaviour
{
    public Rigidbody body;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity=body.velocity;
    }
}
