using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBallon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime*10.0f);
    }
}
