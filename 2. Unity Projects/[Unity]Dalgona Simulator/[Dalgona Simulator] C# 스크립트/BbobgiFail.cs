using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BbobgiFail : MonoBehaviour
{
    public GameObject dalgona;
    public GameObject failUI;
    public GameObject caution;

    Renderer render;
    public int failStack;

    void Awake()
    {
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        if(failStack > 180)
        {
            caution.SetActive(true);
        }

        if(failStack > 210)
        {
            dalgona.SetActive(false);
            render.enabled = true;
            failUI.SetActive(true);
            caution.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Needle")
        {
            failStack++;
        }
    }
}
