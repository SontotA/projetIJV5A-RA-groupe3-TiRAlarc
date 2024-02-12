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
        StartCoroutine(SpawnBallon());
    }

    IEnumerator SpawnBallon()
    {
        while (true)
        {
            Vector3 vec = new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f));
            vec = vec.normalized;
            GameObject newTarget = Instantiate(targetPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newTarget.transform.parent = transform;
            newTarget.transform.localPosition = vec*upperBoundary.x;
            targetOn= true;
            yield return new WaitForSeconds(1);
        }

    }
    
    // Update is called once per frame
    void Update()
    {
   
     
    }

    public void TargetGotShot() { targetOn = false; }
}
