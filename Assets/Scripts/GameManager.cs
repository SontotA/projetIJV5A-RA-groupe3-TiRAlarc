using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public TargetGenerator TargetGenerator;
    public TextMeshProUGUI timer;
    public int nbMaxTarget;
    public int nbTarget;
    public int score;
    public DateTime start;
    public bool run = false;
    public void Awake()
    {
        instance = this;
    }
    public void StartBallon()
    {
        score = 0;
        nbTarget = 0;
        TargetGenerator.StartBallon();
        start = DateTime.Now;
        run = true;
    }

    public void Update()
    {
        if(run)
            timer.text = (DateTime.Now - start).ToString("mm")+":"+(DateTime.Now - start).ToString("ss") ;
        if (score >= 10)
        {
            TargetGenerator.StopBallon();
            GameObject[] go = GameObject.FindGameObjectsWithTag("Target");
            for (int i = 0; i < go.Length; i++)
            {
                Destroy(go[i]);
            }

            run = false;
        }
    }
}
