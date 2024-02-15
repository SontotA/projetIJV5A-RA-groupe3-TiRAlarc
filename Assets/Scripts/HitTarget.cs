using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTarget : MonoBehaviour
{
    public bool destoyed;
    private  AudioSource AS;
    // Start is called before the first frame update
    void Start()
    {
        destoyed = false;
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (destoyed && !AS.isPlaying)
        {
            Destroy(this.gameObject);        
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            Destroy(this.transform.GetChild(0).gameObject);
            this.GetComponent<SphereCollider>().enabled = false;
            AS.Play();
            destoyed = true;
            GameManager.instance.nbTarget--;
            GameManager.instance.score++;
        }
    }
}
