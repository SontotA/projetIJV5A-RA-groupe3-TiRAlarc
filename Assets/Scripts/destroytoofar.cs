using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroytoofar : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, Camera.main.transform.position) > 30)
        {
            Destroy(this.gameObject);
        }
    }
}
