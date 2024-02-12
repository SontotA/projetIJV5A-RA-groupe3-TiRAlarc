using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BowShooting : MonoBehaviour
{
    public GameObject bow;
    public GameObject arrow;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
        print(Vector3.Distance(bow.transform.position, camera.transform.position));
        if(Input.GetMouseButtonDown(0))
        {
            print("chargement");
        }
        if (Input.GetMouseButtonUp(0))
        {
            GameObject _Arrow = Instantiate(arrow,camera.transform.position,Quaternion.identity);
            Rigidbody rb = _Arrow.GetComponent<Rigidbody>();
            _Arrow.transform.rotation = Quaternion.LookRotation((bow.transform.position - _Arrow.transform.position).normalized,
                                                                Vector3.up);
            rb.AddForce(_Arrow.transform.forward * 100,
                        ForceMode.VelocityChange);
            }
    }
}
