using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    public Vector2 upperBoundary,lowerBoundary;
    public GameObject targetPrefab;
    private bool targetOn;

    // Start is called before the first frame update
    void Start()
    {
        targetOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetOn) 
        {
            float posx = Random.Range(lowerBoundary.x,upperBoundary.x);
            float posy = Random.Range(lowerBoundary.y,upperBoundary.y);
            GameObject newTarget = Instantiate(targetPrefab, new Vector3(posx, posy, 0), Quaternion.identity);
            newTarget.transform.parent = transform;
            targetOn= true;
        }
    }

    public void TargetGotShot() { targetOn = false; }
}
