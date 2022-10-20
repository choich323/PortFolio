using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Press")
        {
            anim.SetBool("ispressed", true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Press")
        {
            anim.SetBool("ispressed", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Press")
        {
            anim.SetBool("ispressed", false);
        }
    }
}
