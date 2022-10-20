using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isTouch : MonoBehaviour
{
    public bool istouch;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Frame")
            istouch = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Frame")
            istouch = false;
    }
}
