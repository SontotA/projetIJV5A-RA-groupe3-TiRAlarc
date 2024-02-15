using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetGenerator : MonoBehaviour
{
    public Vector2 upperBoundary,lowerBoundary;


    public GameObject[] targetPrefab;
    public GameObject Button;
    private bool targetOn;
    private Coroutine Coroutine;
    

    public void StartBallon()
    {
        Coroutine = StartCoroutine(SpawnBallon());
    }

    public void StopBallon()
    {
       StopCoroutine(Coroutine);
       Button.SetActive(true);
    }

    public IEnumerator SpawnBallon()
    {
        while (true)
        {
            if (GameManager.instance.nbTarget == GameManager.instance.nbMaxTarget)
            {
                yield return new WaitForSeconds(1); 
                continue;
            }
            Vector3 vec = new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f));
            vec = vec.normalized;
            GameObject newTarget = Instantiate(targetPrefab[Random.Range(0,targetPrefab.Length)], new Vector3(0, 0, 0), Quaternion.identity);
            newTarget.transform.parent = transform;
            newTarget.transform.localPosition = vec* Random.Range(upperBoundary.x,upperBoundary.y);
            targetOn= true;
            GameManager.instance.nbTarget++;
            yield return new WaitForSeconds(1);
        }

    }

    public void TargetGotShot() { targetOn = false; }
}
