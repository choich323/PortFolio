using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InductionSwitch : MonoBehaviour
{
    public GameManger manager;

    public GameObject swi;
    public GameObject burned;
    public GameObject white;
    public GameObject white_mole;
    public GameObject brown_mole;
    public GameObject danger;
    public GameObject failed;
    public GameObject complete;

    public bool dummyswtch; // switch4만 사용되도록
    bool isOn = false;
    bool swtchReady = true;
    bool onSugar = false;
    float burningCount; // 실패 판단
    float swtchDelay;

    public float rate;
    
    void Update()
    {
        if (white.activeInHierarchy)
            onSugar = true;
        if (isOn && onSugar && !complete.activeInHierarchy && !dummyswtch)
        {
            burningCount += Time.deltaTime;
            manager.Burning((int)burningCount);
        }
        Burning();
        Burned();
        swtchDelay += Time.deltaTime;
        swtchReady = rate < swtchDelay;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Finger" && swtchReady)
        {
            if (!isOn)
            {
                swi.SetActive(true);
                isOn = true;
                swtchDelay = 0;
            }
            else
            {
                swi.SetActive(false);
                isOn = false;
                swtchDelay = 0;
            }
        }
    }

    void Burning()
    {
        if (burningCount > 20 && burningCount <= 45 && isOn)
            danger.SetActive(true);

        if (burningCount > 20 && !isOn)
            danger.SetActive(false);

    }

    void Burned()
    {
        if(burningCount > 45 && !complete.activeInHierarchy)
        {
            white.SetActive(false);
            white_mole.SetActive(false);
            brown_mole.SetActive(false);
            burned.SetActive(true);
            danger.SetActive(false);
            failed.SetActive(true);
        }
    }
}
