using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleBoundary : MonoBehaviour
{
    Renderer render;
    public Dalgona dalgona;

    int stack = 0;

    void Awake()
    {
        render = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Needle")
        {
            stack++;
            if (stack == 8)
            {
                render.enabled = true;
                dalgona.black++;
            }
        }
    }
}
