using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BlueBallon : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity += Vector3.up * 0.5f*Time.deltaTime;
        
        if (Vector3.Distance(this.transform.position, Camera.main.transform.position) > 30)
        {
            GameManager.instance.nbTarget--;
            Destroy(this.gameObject);
        }
    }
}
