using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BowShooting : MonoBehaviour
{
    public GameObject bow;
    public GameObject arrow;
    public GameObject camera;
    public Scrollbar scrollBar;
    public AudioSource AudioArrow;
    
    private float distance;
    private Vector3 position;
    private float counter;

    private bool startLoading = false;
    // Start is called before the first frame update
    void Start()
    {
      //  Vibration.Vibrate(500);
      distance = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(bow.transform.position, camera.transform.position)/0.4f;
        //scrollBar.size =  Mathf.Clamp( distance,0,1);
        if(Input.GetMouseButtonDown(0) && distance < 0.25f)
        {
            startLoading = true;
            AudioArrow.Play();
        }
       
        counter -= Time.deltaTime;
        if (startLoading && counter <= 0)
        {
            counter = 1 * (1.0f/(distance*4));
            AndroidVibration.CreateOneShot(200, 150);    
        }
        
        if (Input.GetMouseButtonUp(0) && startLoading )
        {
            startLoading = false;
            GameObject _Arrow = Instantiate(arrow,camera.transform.position,Quaternion.identity);
            Rigidbody rb = _Arrow.GetComponent<Rigidbody>();
            _Arrow.transform.rotation = Quaternion.LookRotation((bow.transform.position - _Arrow.transform.position).normalized,
                                                                Vector3.up);
            rb.AddForce(_Arrow.transform.forward * (40 * distance),
                        ForceMode.VelocityChange);
            }
    }
}
